using Grapher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace Grapher.ViewModels
{

    public class Edge : INotifyPropertyChanged, IDisposable
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Node   SourceNode;
        private Anchor SourceAnchor;

        private Node   TargetNode;
        private Anchor TargetAnchor;

        private double _SourceX;
        public  double SourceX
        {
            get
            {
                return _SourceX;
            }
            private set
            {
                _SourceX = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceX"));
                Angle = double.NaN;
            }
        }
        public  double SourceActualX
        {
            get
            {
                if(null==SourceAnchor)
                {
                    return 0;
                }
                else
                {
                    return SourceAnchor.Source.X;
                }
            }
        }

        private double _SourceY;
        public  double SourceY
        {
            get
            {
                return _SourceY;
            }
            private set
            {
                _SourceY = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceY"));
                Angle = double.NaN;
            }
        }
        public  double SourceActualY
        {
            get
            {
                if (null == SourceAnchor)
                {
                    return 0;
                }
                else
                {
                    return SourceAnchor.Source.Y;
                }
            }
        }

        private double _TargetX;
        public  double TargetX
        {
            get
            {
                return _TargetX;
            }
            private set
            {
                if(value!=_TargetX)
                {
                    _TargetX = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetX"));
                    Angle = double.NaN;
                }
            }
        }
        public  double TargetActualX
        {
            get
            {
                if (null == TargetAnchor)
                {
                    return 1;
                }
                else
                {
                    return TargetAnchor.Target.X;
                }
            }
        }

        private double _TargetY;
        public  double TargetY
        {
            get
            {
                return _TargetY;
            }
            private set
            {
                if (value != _TargetY)
                {
                    _TargetY = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetY"));
                    Angle = double.NaN;
                }
            }
        }
        public  double TargetActualY
        {
            get
            {
                if (null == SourceAnchor)
                {
                    return 1;
                }
                else
                {
                    return TargetAnchor.Target.Y;
                }
            }
        }

        private double _Angle;
        public  double Angle
        {
            get
            {
                return _Angle;
            }
            private set
            {

                if(null==SourceAnchor || null==TargetAnchor)
                {
                    return;
                }
                else if(double.IsNaN(value))
                {

                    _Angle = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );
                    if(0>_Angle)
                    {
                        _Angle += 2 * Math.PI;
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Angle"));

                    LabelLeft   = double.NaN;
                    LabelTop    = double.NaN;
                    ArrowAlphaX = double.NaN;
                    ArrowAlphaY = double.NaN;
                    ArrowBravoX = double.NaN;
                    ArrowBravoY = double.NaN;
                    MinWidth    = double.NaN;
                    MinHeight   = double.NaN;

                }
                else
                {
                    throw new ArgumentException();
                }

            }
        }

        private double _MinWidth;
        public  double MinWidth
        {
            private set
            {
                if (double.IsNaN(value))
                {
                    _MinWidth  = (LabelWidth + 4 * EndpointRadius) * Math.Cos(Angle);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinWidth"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return _MinWidth;
            }
        }

        private double _MinHeight;
        public  double MinHeight
        {
            private set
            {
                if (double.IsNaN(value))
                {
                    _MinHeight = (LabelHeight + 4 * EndpointRadius) * Math.Sin(Angle);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinHeight"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return _MinHeight;
            }
        }

        private double _ArrowAlphaX;
        public  double ArrowAlphaX
        {
            get
            {
                return _ArrowAlphaX;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    _ArrowAlphaX = TargetX + ArrowLenght * Math.Cos(Angle - Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowAlphaX"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        private double _ArrowAlphaY;
        public  double ArrowAlphaY
        {
            get
            {
                return _ArrowAlphaY;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    _ArrowAlphaY = TargetY + ArrowLenght * Math.Sin(Angle - Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowAlphaY"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private double _ArrowBravoX;
        public  double ArrowBravoX
        {
            get
            {
                return _ArrowBravoX;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    _ArrowBravoX = TargetX + ArrowLenght * Math.Cos(Angle + Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowBravoX"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        private double _ArrowBravoY;
        public  double ArrowBravoY
        {
            get
            {
                return _ArrowBravoY;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    _ArrowBravoY = TargetY + ArrowLenght * Math.Sin(Angle + Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowBravoY"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private string _Label;
        public  string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                if (_Label != value)
                {
                    _Label = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
                }
            }
        }

        private double _LabelWidth;
        public  double LabelWidth
        {
            get
            {
                return _LabelWidth;
            }
            set
            {
                if (_LabelWidth != value)
                {
                    _LabelWidth = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelWidth"));
                    LabelLeft = double.NaN;
                    MinWidth  = double.NaN;
                    MinHeight = double.NaN;
                }
            }
        }

        private double _LabelHeight;
        public  double LabelHeight
        {
            get
            {
                return _LabelHeight;
            }
            set
            {
                if (_LabelHeight != value)
                {
                    _LabelHeight = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelHeight"));
                    LabelTop  = double.NaN;
                    MinWidth  = double.NaN;
                    MinHeight = double.NaN;
                }
            }
        }

        private double _LabelLeft;
        public  double LabelLeft
        {
            get
            {
                return _LabelLeft;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var Center  = Math.Min(SourceX, TargetX);
                    Center     += Math.Abs(SourceX - TargetX) / 2;
                    Center     -= LabelWidth / 2;
                    var Offset  = LabelHeight / 2 * Math.Sin(Angle);
                    _LabelLeft = Center + Offset;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelLeft"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private double _LabelTop;
        public  double LabelTop
        {
            get
            {
                return _LabelTop;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var Center  = Math.Min(SourceY, TargetY);
                    Center     += Math.Abs(SourceY - TargetY) / 2;
                    Center     -= LabelHeight / 2;
                    var Offset  = LabelHeight / 2 * Math.Cos(Angle);
                    _LabelTop   = Center - Offset;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelTop"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private double _EndpointRadius = 10;
        public  double EndpointRadius
        {
            get
            {
                return _EndpointRadius;
            }
            set
            {
                _EndpointRadius = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EndpointRadius"));
                MinHeight = double.NaN;
                MinWidth  = double.NaN;
            }
        }

        public Edge(Node Source, Node Target, String Label)
        {

            this.SourceNode = Source;
            this.TargetNode = Target;
            this.Label      = Label;

            SourceNode.PropertyChanged += NodeChanged;            
            TargetNode.PropertyChanged += NodeChanged;
            
        }
        
        public void Dispose()
        {
            SourceNode.PropertyChanged -= NodeChanged;
            TargetNode.PropertyChanged -= NodeChanged;
            // ToDo think about the anchor callbacks!
        }

        private void NodeChanged(object sender, PropertyChangedEventArgs e)
        {
            
            if("Left"==e.PropertyName || "Top" == e.PropertyName)
            {

                if(null != SourceAnchor)
                {
                    SourceAnchor.PropertyChanged -= SourceAnchorChanged;
                }
                SourceAnchor = SourceNode.GetAnchorRelativeTo(TargetNode);
                SourceAnchor.PropertyChanged += SourceAnchorChanged;
                SourceAnchorChanged(SourceAnchor, null);

                if (null != TargetAnchor)
                {
                    TargetAnchor.PropertyChanged -= TargetAnchorChanged;
                }
                TargetAnchor = TargetNode.GetAnchorRelativeTo(SourceNode);
                TargetAnchor.PropertyChanged += TargetAnchorChanged;
                TargetAnchorChanged(TargetAnchor, null);

            }
            
        }

        private void SourceAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            var Anchor = sender as Anchor;
            if(null != Anchor && Anchor == SourceAnchor)
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceActualX"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceActualY"));

                if(null!=TargetAnchor)
                {
                    var BaselineAngleInRadians = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );
                    SourceX = SourceActualX + 2 * EndpointRadius * Math.Cos(BaselineAngleInRadians);
                    SourceY = SourceActualY + 2 * EndpointRadius * Math.Sin(BaselineAngleInRadians);
                }

            }
            else
            {
                throw new ArgumentException();
            }

        }
        
        private void TargetAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            var Anchor = sender as Anchor;
            if (null != Anchor && Anchor == TargetAnchor)
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetActualX"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetActualY"));

                if(null!=SourceAnchor)
                {
                    
                    var BaselineAngleInRadians = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );

                    TargetX = TargetActualX - 2 * EndpointRadius * Math.Cos(BaselineAngleInRadians);
                    TargetY = TargetActualY - 2 * EndpointRadius * Math.Sin(BaselineAngleInRadians);

                }

            }
            else
            {
                throw new ArgumentException();
            }

        }
        
    }

}
