﻿using Grapher.ViewModels;
using System;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace Grapher.Models
{

    public class Node : INotifyPropertyChanged
    {

        public  event PropertyChangedEventHandler PropertyChanged;
        private async void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => handler(this, new PropertyChangedEventArgs(info))
                );
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
                _Label = value;
                OnPropertyChanged("Label");
            }
        }

        private double _CornerRadius = 0;
        public  double CornerRadius
        {
            get
            {
                return _CornerRadius;
            }
            set
            {
                _CornerRadius = value;
                OnPropertyChanged("CornerRadius");
                if (null != _AnchorTopLeft)
                {
                    AnchorTopLeft = new Anchor();
                }
                if (null != _AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != _AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        private double _MinWidth;
        public double MinWidth
        {
            get
            {
                return _MinWidth;
            }
            set
            {
                _MinWidth = value;
                OnPropertyChanged("MinWidth");
                if (null != _AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                if (null != _AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != _AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != _AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
            }
        }

        private double _Width;
        public  double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                OnPropertyChanged("Width");
                if(null!=_AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                if (null != _AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                if (null != _AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != _AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
            }
        }

        private double _MinHeight;
        public double MinHeight
        {
            get
            {
                return _MinHeight;
            }
            set
            {
                _MinHeight = value;
                OnPropertyChanged("MinHeight");
                if (null != _AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                if (null != _AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != _AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                if (null != _AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        private double _Height;
        public double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
                OnPropertyChanged("Height");
                if (null != _AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                if (null != _AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                if (null != _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                if (null != _AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                if (null != _AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
            }
        }

        private double _Left;
        public  double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                InvalidateAnchors();
                _Left = value;
                OnPropertyChanged("Left");
            }
        }

        private double _Top;
        public  double Top
        {
            get
            {
                return _Top;
            }
            set
            {
                InvalidateAnchors();
                _Top = value;
                OnPropertyChanged("Top");
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
                    return null;
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
        
        public  Anchor _AnchorLeft;
        public  Anchor AnchorLeft
        {
            private set
            {
                _AnchorLeft = value;
                if ( null != _AnchorLeft )
                {
                    _AnchorLeft.Set(
                        new Point(Left, Top + Height / 2),
                        new Point(Left, Top + Height / 2 + 10),
                        new Point(Left, Top + Height / 2 - 10)
                    );
                }
                OnPropertyChanged("AnchorLeft");
            }
            get
            {
                if(null==_AnchorLeft)
                {
                    AnchorLeft = new Anchor();
                }
                return _AnchorLeft;
            }
        }
        
        public  Anchor _AnchorTopLeft;
        public  Anchor AnchorTopLeft
        {
            private set
            {
                _AnchorTopLeft = value;
                if (null != _AnchorTopLeft)
                {
                    _AnchorTopLeft.Set(
                        new Point(Left + CornerRadius * ( 1 + Math.Cos(225*Math.PI/180) ),
                                  Top  + CornerRadius * ( 1 + Math.Sin(225*Math.PI/180) )),
                        new Point(Left, Top + CornerRadius),
                        new Point(Left + CornerRadius, Top)
                    );
                }
                OnPropertyChanged("AnchorTopLeft");
            }
            get
            {
                if (null == _AnchorTopLeft)
                {
                    AnchorTopLeft = new Anchor();
                }
                return _AnchorTopLeft;
            }
        }

        public  Anchor _AnchorTop;
        public  Anchor AnchorTop
        {
            private set
            {
                _AnchorTop = value;
                if (null != _AnchorTop)
                {
                    _AnchorTop.Set(
                        new Point(Left + Width / 2, Top),
                        new Point(Left + Width / 2 - 10, Top),
                        new Point(Left + Width / 2 + 10, Top)
                    );
                }
                OnPropertyChanged("AnchorTop");
            }
            get
            {
                if (null == _AnchorTop)
                {
                    AnchorTop = new Anchor();
                }
                return _AnchorTop;
            }
        }

        public  Anchor _AnchorTopRight;
        public  Anchor AnchorTopRight
        {
            private set
            {
                _AnchorTopRight = value;
                if (null != _AnchorTopRight)
                {
                    _AnchorTopRight.Set(
                        new Point(Left + Width + CornerRadius * ( -1 + Math.Cos(315 * Math.PI / 180) ),
                                  Top          + CornerRadius * (  1 + Math.Sin(315 * Math.PI / 180) )),
                        new Point(Left + Width - CornerRadius, Top),
                        new Point(Left + Width, Top + CornerRadius)
                    );
                }
                OnPropertyChanged("AnchorTopRight");
            }
            get
            {
                if (null == _AnchorTopRight)
                {
                    AnchorTopRight = new Anchor();
                }
                return _AnchorTopRight;
            }
        }

        public  Anchor _AnchorRight;
        public  Anchor AnchorRight
        {
            private set
            {
                _AnchorRight = value;
                if (null != _AnchorRight)
                {
                    _AnchorRight.Set(
                        new Point(Left + Width, Top + Height / 2),
                        new Point(Left + Width, Top + Height / 2 - 10),
                        new Point(Left + Width, Top + Height / 2 + 10)
                    );
                }
                OnPropertyChanged("AnchorRight");
            }
            get
            {
                if (null == _AnchorRight)
                {
                    AnchorRight = new Anchor();
                }
                return _AnchorRight;
            }
        }

        public  Anchor _AnchorBottomRight;
        public  Anchor AnchorBottomRight
        {
            private set
            {
                _AnchorBottomRight = value;
                if (null != _AnchorBottomRight)
                {
                    _AnchorBottomRight.Set(
                        new Point(Left + Width + CornerRadius * (-1 + Math.Cos(45 * Math.PI / 180)),
                                  Top + Height + CornerRadius * (-1 + Math.Sin(45 * Math.PI / 180))),
                        new Point(Left + Width, Top + Height - CornerRadius),
                        new Point(Left + Width - CornerRadius, Top + Height)
                    );
                }
                OnPropertyChanged("AnchorBottomRight");
            }
            get
            {
                if (null == _AnchorBottomRight)
                {
                    AnchorBottomRight = new Anchor();
                }
                return _AnchorBottomRight;
            }
        }

        public  Anchor _AnchorBottom;
        public  Anchor AnchorBottom
        {
            private set
            {
                _AnchorBottom = value;
                if (null != _AnchorBottom)
                {
                    _AnchorBottom.Set(
                        new Point(Left + Width / 2, Top + Height),
                        new Point(Left + Width / 2 + 10, Top + Height),
                        new Point(Left + Width / 2 - 10, Top + Height)
                    );
                }
                OnPropertyChanged("AnchorBottom");
            }
            get
            {
                if (null == _AnchorBottom)
                {
                    AnchorBottom = new Anchor();
                }
                return _AnchorBottom;
            }
        }

        public  Anchor _AnchorBottomLeft;
        public  Anchor AnchorBottomLeft
        {
            private set
            {
                _AnchorBottomLeft = value;
                if (null != _AnchorBottomLeft)
                {
                    _AnchorBottomLeft.Set(
                        new Point(Left + CornerRadius * (1 + Math.Cos(135 * Math.PI / 180)),
                                  Top + Height + CornerRadius * (-1 + Math.Sin(135 * Math.PI / 180))),
                        new Point(Left + CornerRadius, Top + Height),
                        new Point(Left, Top + Height - CornerRadius)
                    );
                }
                OnPropertyChanged("AnchorBottomLeft");
            }
            get
            {
                if (null == _AnchorBottomLeft)
                {
                    AnchorBottomLeft = new Anchor();
                }
                return _AnchorBottomLeft;
            }
        }

    }

}