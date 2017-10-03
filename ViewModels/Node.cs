﻿using Grapher.ViewModels;
using System;
using System.ComponentModel;
using Windows.Foundation;

namespace Grapher.Models
{

    public class Node : INotifyPropertyChanged
    {

        public NodeModel Model;
        public Node(NodeModel Model)
        {
            this.Model = Model;
        }

        public  event PropertyChangedEventHandler PropertyChanged;
        
        public  string  Label
        {
            get
            {
                return Model.Label;
            }
            set
            {
                Model.Label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
            }
        }

        public  double  CornerRadius
        {
            get
            {
                return Model.CornerRadius;
            }
            set
            {
                Model.CornerRadius = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CornerRadius"));
                if (null != Model.AnchorTopLeft)
                {
                    AnchorTopLeft = new Anchor();
                }
                if (null != Model.AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != Model.AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        public  double  MinWidth
        {
            get
            {
                return Model.MinWidth;
            }
            set
            {
                Model.MinWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinWidth"));
                if (null != Model.AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                if (null != Model.AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != Model.AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != Model.AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
            }
        }

        public  double  Width
        {
            get
            {
                return Model.Width;
            }
            set
            {
                Model.Width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
                if(null!=Model.AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                if (null != Model.AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != Model.AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != Model.AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
            }
        }

        public  double  MinHeight
        {
            get
            {
                return Model.MinHeight;
            }
            set
            {
                Model.MinHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinHeight"));
                if (null != Model.AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                if (null != Model.AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != Model.AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                if (null != Model.AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        public  double  Height
        {
            get
            {
                return Model.Height;
            }
            set
            {
                Model.Height = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
                if (null != Model.AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                if (null != Model.AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != Model.AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                if (null != Model.AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        public  double  Left
        {
            get
            {
                return Model.Left;
            }
            set
            {
                if(value!=Model.Left)
                {
                    InvalidateAnchors();
                    Model.Left = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Left"));
                }
            }
        }

        public  double  Top
        {
            get
            {
                return Model.Top;
            }
            set
            {
                if(value!=Model.Top)
                {
                    InvalidateAnchors();
                    Model.Top = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Top"));
                }
            }
        }
        
        public Anchor GetAnchorRelativeTo(Node Other)
        {
            if (Left < Other.Left)
            {
                if (Top < Other.Top)
                {
                    return AnchorBottomRight;
                }
                else if (Top > Other.Top)
                {
                    return AnchorTopRight;
                }
                else
                {
                    return AnchorRight;
                }
            }
            else if (Left > Other.Left)
            {
                if (Top < Other.Top)
                {
                    return AnchorBottomLeft;
                }
                else if (Top > Other.Top)
                {
                    return AnchorTopLeft;
                }
                else
                {
                    return AnchorLeft;
                }
            }
            else
            {
                if (Top < Other.Top)
                {
                    return AnchorBottom;
                }
                else if (Top > Other.Top)
                {
                    return AnchorTop;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private void InvalidateAnchors()
        {
            AnchorLeft          = null;
            AnchorTopLeft       = null;
            AnchorTop           = null;
            AnchorTopRight      = null;
            AnchorRight         = null;
            AnchorBottomRight   = null;
            AnchorBottom        = null;
            AnchorBottomLeft    = null;
        }

        public  Anchor  AnchorLeft
        {
            private set
            {
                Model.AnchorLeft = value;
                if ( null != Model.AnchorLeft )
                {
                    Model.AnchorLeft.Set(
                        new Point(Left, Top + Height / 2),
                        new Point(Left, Top + Height / 2 + 10),
                        new Point(Left, Top + Height / 2 - 10)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorLeft"));
            }
            get
            {
                if(null==Model.AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                return Model.AnchorLeft;
            }
        }

        public  Anchor  AnchorTopLeft
        {
            private set
            {
                Model.AnchorTopLeft = value;
                if (null != Model.AnchorTopLeft)
                {
                    Model.AnchorTopLeft.Set(
                        new Point(Left + CornerRadius * ( 1 + Math.Cos(225*Math.PI/180) ),
                                  Top  + CornerRadius * ( 1 + Math.Sin(225*Math.PI/180) )),
                        new Point(Left, Top + CornerRadius),
                        new Point(Left + CornerRadius, Top)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopLeft"));
            }
            get
            {
                if (null == Model.AnchorTopLeft)
                {
                    AnchorTopLeft = new Anchor();
                }
                return Model.AnchorTopLeft;
            }
        }

        public  Anchor  AnchorTop
        {
            private set
            {
                Model.AnchorTop = value;
                if (null != Model.AnchorTop)
                {
                    Model.AnchorTop.Set(
                        new Point(Left + Width / 2, Top),
                        new Point(Left + Width / 2 - 10, Top),
                        new Point(Left + Width / 2 + 10, Top)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTop"));
            }
            get
            {
                if (null == Model.AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                return Model.AnchorTop;
            }
        }

        public  Anchor  AnchorTopRight
        {
            private set
            {
                Model.AnchorTopRight = value;
                if (null != Model.AnchorTopRight)
                {
                    Model.AnchorTopRight.Set(
                        new Point(Left + Width + CornerRadius * ( -1 + Math.Cos(315 * Math.PI / 180) ),
                                  Top          + CornerRadius * (  1 + Math.Sin(315 * Math.PI / 180) )),
                        new Point(Left + Width - CornerRadius, Top),
                        new Point(Left + Width, Top + CornerRadius)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorTopRight"));
            }
            get
            {
                if (null == Model.AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                return Model.AnchorTopRight;
            }
        }

        public  Anchor  AnchorRight
        {
            private set
            {
                Model.AnchorRight = value;
                if (null != Model.AnchorRight)
                {
                    Model.AnchorRight.Set(
                        new Point(Left + Width, Top + Height / 2),
                        new Point(Left + Width, Top + Height / 2 - 10),
                        new Point(Left + Width, Top + Height / 2 + 10)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorRight"));
            }
            get
            {
                if (null == Model.AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                return Model.AnchorRight;
            }
        }

        public  Anchor  AnchorBottomRight
        {
            private set
            {
                Model.AnchorBottomRight = value;
                if (null != Model.AnchorBottomRight)
                {
                    Model.AnchorBottomRight.Set(
                        new Point(Left + Width + CornerRadius * (-1 + Math.Cos(45 * Math.PI / 180)),
                                  Top + Height + CornerRadius * (-1 + Math.Sin(45 * Math.PI / 180))),
                        new Point(Left + Width, Top + Height - CornerRadius),
                        new Point(Left + Width - CornerRadius, Top + Height)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomRight"));
            }
            get
            {
                if (null == Model.AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                return Model.AnchorBottomRight;
            }
        }

        public  Anchor  AnchorBottom
        {
            private set
            {
                Model.AnchorBottom = value;
                if (null != Model.AnchorBottom)
                {
                    Model.AnchorBottom.Set(
                        new Point(Left + Width / 2, Top + Height),
                        new Point(Left + Width / 2 + 10, Top + Height),
                        new Point(Left + Width / 2 - 10, Top + Height)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottom"));
            }
            get
            {
                if (null == Model.AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                return Model.AnchorBottom;
            }
        }

        public  Anchor  AnchorBottomLeft
        {
            private set
            {
                Model.AnchorBottomLeft = value;
                if (null != Model.AnchorBottomLeft)
                {
                    Model.AnchorBottomLeft.Set(
                        new Point(Left + CornerRadius * (1 + Math.Cos(135 * Math.PI / 180)),
                                  Top + Height + CornerRadius * (-1 + Math.Sin(135 * Math.PI / 180))),
                        new Point(Left + CornerRadius, Top + Height),
                        new Point(Left, Top + Height - CornerRadius)
                    );
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnchorBottomLeft"));
            }
            get
            {
                if (null == Model.AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
                return Model.AnchorBottomLeft;
            }
        }

    }

}
