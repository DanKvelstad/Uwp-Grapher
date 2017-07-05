using System;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Grapher
{
    public sealed partial class Node : UserControl, INotifyPropertyChanged
    {

        public Node(String Name)
        {

            this.InitializeComponent();

            StateName.Text = Name;
            StateName.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            NodeBorder.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Width   = NodeBorder.DesiredSize.Width;
            Height  = NodeBorder.DesiredSize.Height;

        }

        internal int CompareTo(Node fromState)
        {
            return StateName.Text.CompareTo(fromState.StateName.Text);
        }

        public Point GetFromAnchorRelativeTo(Node Other)
        {
            if (Center.X < Other.Center.X)
            {
                if (Center.Y < Other.Center.Y)
                {
                    return FromAnchorBottomRight;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return FromAnchorTopRight;
                }
                else
                {
                    return FromAnchorRight;
                }
            }
            else if (Center.X > Other.Center.X)
            {
                if (Center.Y < Other.Center.Y)
                {
                    return FromAnchorBottomLeft;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return FromAnchorTopLeft;
                }
                else
                {
                    return FromAnchorLeft;
                }
            }
            else
            {
                if (Center.Y < Other.Center.Y)
                {
                    return FromAnchorBottomCenter;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return FromAnchorTopCenter;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        
        public Point GetToAnchorRelativeTo(Node Other)
        {
            if (Center.X < Other.Center.X)
            {
                if (Center.Y < Other.Center.Y)
                {
                    return ToAnchorBottomRight;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return ToAnchorTopRight;
                }
                else
                {
                    return ToAnchorRight;
                }
            }
            else if (Center.X > Other.Center.X)
            {
                if (Center.Y < Other.Center.Y)
                {
                    return ToAnchorBottomLeft;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return ToAnchorTopLeft;
                }
                else
                {
                    return ToAnchorLeft;
                }
            }
            else
            {
                if (Center.Y < Other.Center.Y)
                {
                    return ToAnchorBottomCenter;
                }
                else if (Center.Y > Other.Center.Y)
                {
                    return ToAnchorTopCenter;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public Point Grid_Point;

        private Point _Center;
        public Point Center
        {
            get
            {
                return this._Center;
            }
            set
            {
                _Center = value;
                if(new Point()!=_FromAnchorLeft)
                {
                    FromAnchorLeft = Center;
                }
                else if(new Point()!=_ToAnchorLeft)
                {
                    ToAnchorLeft = Center;
                }
                if (new Point() != _FromAnchorTopLeft)
                {
                    FromAnchorTopLeft = Center;
                }
                else if (new Point() != _ToAnchorTopLeft)
                {
                    ToAnchorTopLeft = Center;
                }
                if (new Point() != _FromAnchorTopCenter)
                {
                    FromAnchorTopCenter = Center;
                }
                else if (new Point() != _ToAnchorTopCenter)
                {
                    ToAnchorTopCenter = Center;
                }
                if (new Point() != _FromAnchorTopRight)
                {
                    FromAnchorTopRight = Center;
                }
                else if (new Point() != _ToAnchorTopRight)
                {
                    ToAnchorTopRight = Center;
                }
                if (new Point() != _FromAnchorRight)
                {
                    FromAnchorRight = Center;
                }
                else if (new Point() != _ToAnchorRight)
                {
                    ToAnchorRight = Center;
                }
                if (new Point() != _FromAnchorBottomLeft)
                {
                    FromAnchorBottomLeft = Center;
                }
                else if (new Point() != _ToAnchorBottomLeft)
                {
                    ToAnchorBottomLeft = Center;
                }
                if (new Point() != _FromAnchorBottomCenter)
                {
                    FromAnchorBottomCenter = Center;
                }
                else if (new Point() != _ToAnchorBottomCenter)
                {
                    ToAnchorBottomCenter = Center;
                }
                if (new Point() != _FromAnchorBottomRight)
                {
                    FromAnchorBottomRight = Center;
                }
                else if (new Point() != _ToAnchorBottomRight)
                {
                    ToAnchorBottomRight = Center;
                }
            }
        }

        private Point _FromAnchorLeft;
        private Point _ToAnchorLeft;
        private Point FromAnchorLeft
        {
            get
            {
                if (new Point() == _FromAnchorLeft)
                {
                    FromAnchorLeft = Center;
                }
                return _FromAnchorLeft;
            }
            set
            {
                if (new Point()==_ToAnchorLeft)
                {
                    _FromAnchorLeft = new Point(
                        value.X - Width / 2,
                        value.Y
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorLeft"));
                }
                else
                {
                    var _NewFromAnchorLeft = new Point(
                        value.X - Width / 2,
                        value.Y + 5
                    );
                    if (_FromAnchorLeft != _NewFromAnchorLeft)
                    {
                        _FromAnchorLeft = _NewFromAnchorLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorLeft"));
                        ToAnchorLeft = Center;
                    }
                }
            }
        }
        private Point ToAnchorLeft
        {
            get
            {
                if (new Point() == _ToAnchorLeft)
                {
                    ToAnchorLeft = Center;
                }
                return _ToAnchorLeft;
            }
            set
            {
                if (new Point() == _FromAnchorLeft)
                {
                    _ToAnchorLeft = new Point(
                        value.X - Width / 2,
                        value.Y
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorLeft"));
                }
                else
                {
                    var _NewToAnchorLeft = new Point(
                        value.X - Width / 2,
                        value.Y - 5
                    );
                    if( _ToAnchorLeft!=_NewToAnchorLeft )
                    {
                        _ToAnchorLeft = _NewToAnchorLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorLeft"));
                        FromAnchorLeft = Center;
                    }
                }
            }
        }

        private Point _FromAnchorTopLeft;
        private Point _ToAnchorTopLeft;
        private Point FromAnchorTopLeft
        {
            get
            {
                if (new Point() == _FromAnchorTopLeft)
                {
                    FromAnchorTopLeft = Center;
                }
                return _FromAnchorTopLeft;
            }
            set
            {
                if (new Point() == _ToAnchorTopLeft)
                {
                    _FromAnchorTopLeft = new Point(
                        value.X -  Width / 2 + (NodeBorder.Margin.Left + NodeBorder.CornerRadius.TopLeft) * (1-Math.Cos(Math.PI/4)),
                        value.Y - Height / 2 + (NodeBorder.Margin.Top  + NodeBorder.CornerRadius.TopLeft) * (1-Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopLeft"));
                }
                else
                {
                    var _NewFromAnchorTopLeft = new Point(
                        value.X -  Width / 2,
                        value.Y - Height / 2 + NodeBorder.CornerRadius.TopLeft
                    );
                    if (_FromAnchorTopLeft != _NewFromAnchorTopLeft)
                    {
                        _FromAnchorTopLeft = _NewFromAnchorTopLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopLeft"));
                        ToAnchorTopLeft = Center;
                    }
                }
            }
        }
        private Point ToAnchorTopLeft
        {
            get
            {
                if (new Point() == _ToAnchorTopLeft)
                {
                    ToAnchorTopLeft = Center;
                }
                return _ToAnchorTopLeft;
            }
            set
            {
                if (new Point() == _FromAnchorTopLeft)
                {
                    _ToAnchorTopLeft = new Point(
                        value.X -  Width / 2 + (NodeBorder.Margin.Left + NodeBorder.CornerRadius.TopLeft) * (1-Math.Cos(Math.PI/4)),
                        value.Y - Height / 2 + (NodeBorder.Margin.Top  + NodeBorder.CornerRadius.TopLeft) * (1-Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopLeft"));
                }
                else
                {
                    var _NewToAnchorTopLeft = new Point(
                        value.X -  Width / 2 + NodeBorder.CornerRadius.TopLeft,
                        value.Y - Height / 2
                    );
                    if (_ToAnchorTopLeft != _NewToAnchorTopLeft)
                    {
                        _ToAnchorTopLeft = _NewToAnchorTopLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopLeft"));
                        FromAnchorTopLeft = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorTopCenter;
        private Point _ToAnchorTopCenter;
        private Point FromAnchorTopCenter
        {
            get
            {
                if (new Point() == _FromAnchorTopCenter)
                {
                    FromAnchorTopCenter = Center;
                }
                return _FromAnchorTopCenter;
            }
            set
            {
                if (new Point() == _ToAnchorTopCenter)
                {
                    _FromAnchorTopCenter = new Point(
                        value.X,
                        value.Y - Height/2
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopCenter"));
                }
                else
                {
                    var _NewFromAnchorTopCenter = new Point(
                        value.X - 5,
                        value.Y - Height/2
                    );
                    if (_FromAnchorTopCenter != _NewFromAnchorTopCenter)
                    {
                        _FromAnchorTopCenter = _NewFromAnchorTopCenter;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopCenter"));
                        ToAnchorTopCenter = Center;
                    }
                }
            }
        }
        private Point ToAnchorTopCenter
        {
            get
            {
                if (new Point() == _ToAnchorTopCenter)
                {
                    ToAnchorTopCenter = Center;
                }
                return _ToAnchorTopCenter;
            }
            set
            {
                if (new Point() == _FromAnchorTopCenter)
                {
                    _ToAnchorTopCenter = new Point(
                        value.X,
                        value.Y - Height/2
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopCenter"));
                }
                else
                {
                    var _NewToAnchorTopCenter = new Point(
                        value.X + 5,
                        value.Y - Height/2
                    );
                    if (_ToAnchorTopCenter != _NewToAnchorTopCenter)
                    {
                        _ToAnchorTopCenter = _NewToAnchorTopCenter;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopCenter"));
                        FromAnchorTopCenter = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorTopRight;
        private Point _ToAnchorTopRight;
        private Point FromAnchorTopRight
        {
            get
            {
                if (new Point() == _FromAnchorTopRight)
                {
                    FromAnchorTopRight = Center;
                }
                return _FromAnchorTopRight;
            }
            set
            {
                if (new Point() == _ToAnchorTopRight)
                {
                    _FromAnchorTopRight = new Point(
                        value.X +  Width / 2 + (NodeBorder.Margin.Right + NodeBorder.CornerRadius.TopRight) * (-1+Math.Cos(Math.PI/4)),
                        value.Y - Height / 2 + (NodeBorder.Margin.Top   + NodeBorder.CornerRadius.TopLeft)  * ( 1-Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopRight"));
                }
                else
                {
                    var _NewFromAnchorTopRight = new Point(
                        value.X +  Width/2 - NodeBorder.CornerRadius.TopRight,
                        value.Y - Height/2
                    );
                    if (_FromAnchorTopRight != _NewFromAnchorTopRight)
                    {
                        _FromAnchorTopRight = _NewFromAnchorTopRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopRight"));
                        ToAnchorTopRight = Center;
                    }
                }
            }
        }
        private Point ToAnchorTopRight
        {
            get
            {
                if (new Point() == _ToAnchorTopRight)
                {
                    ToAnchorTopRight = Center;
                }
                return _ToAnchorTopRight;
            }
            set
            {
                if (new Point() == _FromAnchorTopRight)
                {
                    _ToAnchorTopRight = new Point(
                        value.X +  Width / 2 + (NodeBorder.Margin.Right + NodeBorder.CornerRadius.TopRight) * (-1+Math.Cos(Math.PI/4)),
                        value.Y - Height / 2 + (NodeBorder.Margin.Top   + NodeBorder.CornerRadius.TopLeft)  * ( 1-Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopRight"));
                }
                else
                {
                    var _NewToAnchorTopRight = new Point(
                        value.X +  Width / 2,
                        value.Y - Height / 2 + NodeBorder.CornerRadius.TopRight
                    );
                    if (_ToAnchorTopRight != _NewToAnchorTopRight)
                    {
                        _ToAnchorTopRight = _NewToAnchorTopRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopRight"));
                        FromAnchorTopRight = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorRight;
        private Point _ToAnchorRight;
        private Point FromAnchorRight
        {
            get
            {
                if (new Point() == _FromAnchorRight)
                {
                    FromAnchorRight = Center;
                }
                return _FromAnchorRight;
            }
            set
            {
                if (new Point() == _ToAnchorRight)
                {
                    _FromAnchorRight = new Point(
                        value.X + Width / 2,
                        value.Y
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorRight"));
                }
                else
                {
                    var _NewFromAnchorRight = new Point(
                        value.X + Width / 2,
                        value.Y - 5
                    );
                    if (_FromAnchorRight != _NewFromAnchorRight)
                    {
                        _FromAnchorRight = _NewFromAnchorRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorRight"));
                        ToAnchorRight = Center;
                    }
                }
            }
        }
        private Point ToAnchorRight
        {
            get
            {
                if (new Point() == _ToAnchorRight)
                {
                    ToAnchorRight = Center;
                }
                return _ToAnchorRight;
            }
            set
            {
                if (new Point() == _FromAnchorRight)
                {
                    _ToAnchorRight = new Point(
                        value.X + Width / 2,
                        value.Y
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorRight"));
                }
                else
                {
                    var _NewToAnchorRight = new Point(
                        value.X + Width / 2,
                        value.Y + 5
                    );
                    if (_ToAnchorRight != _NewToAnchorRight)
                    {
                        _ToAnchorRight = _NewToAnchorRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorRight"));
                        FromAnchorRight = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorBottomRight;
        private Point _ToAnchorBottomRight;
        private Point FromAnchorBottomRight
        {
            get
            {
                if (new Point() == _FromAnchorBottomRight)
                {
                    FromAnchorBottomRight = Center;
                }
                return _FromAnchorBottomRight;
            }
            set
            {
                if (new Point() == _ToAnchorBottomRight)
                {
                    _FromAnchorBottomRight = new Point(
                        value.X +  Width / 2 + (NodeBorder.Margin.Right  + NodeBorder.CornerRadius.BottomRight) * (-1+Math.Cos(Math.PI/4)),
                        value.Y + Height / 2 + (NodeBorder.Margin.Bottom + NodeBorder.CornerRadius.BottomRight) * (-1+Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomRight"));
                }
                else
                {
                    var _NewFromAnchorBottomRight = new Point(
                        value.X +  Width / 2,
                        value.Y + Height / 2 - NodeBorder.CornerRadius.BottomRight
                    );
                    if (_FromAnchorBottomRight != _NewFromAnchorBottomRight)
                    {
                        _FromAnchorBottomRight = _NewFromAnchorBottomRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomRight"));
                        ToAnchorBottomRight = Center;
                    }
                }
            }
        }
        private Point ToAnchorBottomRight
        {
            get
            {
                if (new Point() == _ToAnchorBottomRight)
                {
                    ToAnchorBottomRight = Center;
                }
                return _ToAnchorBottomRight;
            }
            set
            {
                if (new Point() == _FromAnchorBottomRight)
                {
                    _ToAnchorBottomRight = new Point(
                        value.X +  Width / 2 + (NodeBorder.Margin.Right  + NodeBorder.CornerRadius.BottomRight) * (-1+Math.Cos(Math.PI/4)),
                        value.Y + Height / 2 + (NodeBorder.Margin.Bottom + NodeBorder.CornerRadius.BottomRight) * (-1+Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomRight"));
                }
                else
                {
                    var _NewToAnchorBottomRight = new Point(
                        value.X +  Width / 2 - 5,
                        value.Y + Height / 2
                    );
                    if (_ToAnchorBottomRight != _NewToAnchorBottomRight)
                    {
                        _ToAnchorBottomRight = _NewToAnchorBottomRight;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomRight"));
                        FromAnchorBottomRight = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorBottomCenter;
        private Point _ToAnchorBottomCenter;
        private Point FromAnchorBottomCenter
        {
            get
            {
                if (new Point() == _FromAnchorBottomCenter)
                {
                    FromAnchorBottomCenter = Center;
                }
                return _FromAnchorBottomCenter;
            }
            set
            {
                if (new Point() == _ToAnchorBottomCenter)
                {
                    _FromAnchorBottomCenter = new Point(
                        value.X,
                        value.Y + Height / 2
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomCenter"));
                }
                else
                {
                    var _NewFromAnchorBottomCenter = new Point(
                        value.X + 5,
                        value.Y + Height / 2
                    );
                    if (_FromAnchorBottomCenter != _NewFromAnchorBottomCenter)
                    {
                        _FromAnchorBottomCenter = _NewFromAnchorBottomCenter;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomCenter"));
                        ToAnchorBottomCenter = Center;
                    }
                }
            }
        }
        private Point ToAnchorBottomCenter
        {
            get
            {
                if (new Point() == _ToAnchorBottomCenter)
                {
                    ToAnchorBottomCenter = Center;
                }
                return _ToAnchorBottomCenter;
            }
            set
            {
                if (new Point() == _FromAnchorBottomCenter)
                {
                    _ToAnchorBottomCenter = new Point(
                        value.X,
                        value.Y + Height / 2
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomCenter"));
                }
                else
                {
                    var _NewToAnchorBottomCenter = new Point(
                        value.X - 5,
                        value.Y + Height / 2
                    );
                    if (_ToAnchorBottomCenter != _NewToAnchorBottomCenter)
                    {
                        _ToAnchorBottomCenter = _NewToAnchorBottomCenter;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomCenter"));
                        FromAnchorBottomCenter = Center;
                    }
                }
            }
        }
        
        private Point _FromAnchorBottomLeft;
        private Point _ToAnchorBottomLeft;
        private Point FromAnchorBottomLeft
        {
            get
            {
                if (new Point() == _FromAnchorBottomLeft)
                {
                    FromAnchorBottomLeft = Center;
                }
                return _FromAnchorBottomLeft;
            }
            set
            {
                if (new Point() == _ToAnchorBottomLeft)
                {
                    _FromAnchorBottomLeft = new Point(
                        value.X -  Width / 2 + (NodeBorder.Margin.Left   + NodeBorder.CornerRadius.BottomLeft) * ( 1-Math.Cos(Math.PI/4)),
                        value.Y + Height / 2 + (NodeBorder.Margin.Bottom + NodeBorder.CornerRadius.BottomLeft) * (-1+Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomLeft"));
                }
                else
                {
                    var _NewFromAnchorBottomLeft = new Point(
                        value.X -  Width / 2 + 5,
                        value.Y + Height / 2
                    );
                    if (_FromAnchorBottomLeft != _NewFromAnchorBottomLeft)
                    {
                        _FromAnchorBottomLeft = _NewFromAnchorBottomLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomLeft"));
                        ToAnchorBottomLeft = Center;
                    }
                }
            }
        }
        private Point ToAnchorBottomLeft
        {
            get
            {
                if (new Point() == _ToAnchorBottomLeft)
                {
                    ToAnchorBottomLeft = Center;
                }
                return _ToAnchorBottomLeft;
            }
            set
            {
                if (new Point() == _FromAnchorBottomLeft)
                {
                    _ToAnchorBottomLeft = new Point(
                        value.X -  Width / 2 + (NodeBorder.Margin.Left   + NodeBorder.CornerRadius.BottomLeft) * ( 1-Math.Cos(Math.PI/4)),
                        value.Y + Height / 2 + (NodeBorder.Margin.Bottom + NodeBorder.CornerRadius.BottomLeft) * (-1+Math.Sin(Math.PI/4))
                    );
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomLeft"));
                }
                else
                {
                    var _NewToAnchorBottomLeft = new Point(
                        value.X -  Width / 2,
                        value.Y + Height / 2 - 5
                    );
                    if (_ToAnchorBottomLeft != _NewToAnchorBottomLeft)
                    {
                        _ToAnchorBottomLeft = _NewToAnchorBottomLeft;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomLeft"));
                        FromAnchorBottomLeft = Center;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
