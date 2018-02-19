using Grapher.Algorithms;
using System;
using System.ComponentModel;

namespace Grapher.Models
{

    public class NodeGeometryModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public double CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                cornerRadius = value;
                InvalidateAnchors();
            }
        }
        private double cornerRadius = 10;

        public void UpdateHorizontally(double left, double width)
        {
            Left  = left;
            Right = left + width;
            InvalidateAnchors();
        }

        public void UpdateVertically(double top, double height)
        {
            Top    = top;
            Bottom = top + height;
            InvalidateAnchors();
        }

        public double Left
        {
            get;
            private set;
        }
        
        public double Top
        {
            get;
            private set;
        }

        public double Right
        {
            get;
            private set;
        }

        public double Bottom
        {
            get;
            private set;
        }

        private void InvalidateAnchors()
        {
            AnchorLeft         = null;
            AnchorTopLeft      = null;
            AnchorTopCenter    = null;
            AnchorTopRight     = null;
            AnchorRight        = null;
            AnchorBottomRight  = null;
            AnchorBottomCenter = null;
            AnchorBottomLeft   = null;
            PropertyChanged?.Invoke(this, null);
        }

        public NodeAnchorModel GetAnchor(NodeGeometryModel other)
        {

            if(Left < other.Left)
            {
                if(Top < other.Top)
                {
                    return AnchorBottomRight;
                }
                else if (Top > other.Top)
                {
                    return AnchorTopRight;
                }
                else
                {
                    return AnchorRight;
                }
            }
            else if(Left > other.Left)
            {
                if (Top < other.Top)
                {
                    return AnchorBottomLeft;
                }
                else if (Top > other.Top)
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
                if (Top < other.Top)
                {
                    return AnchorBottomCenter;
                }
                else if (Top > other.Top)
                {
                    return AnchorTopCenter;
                }
                else
                {
                    return null;
                }
            }

        }
        
        public NodeAnchorModel AnchorLeft
        {
            get
            {
                if (null == anchorLeft)
                {
                    AnchorLeft = new NodeAnchorModel();
                }
                return anchorLeft;
            }
            set
            {
                anchorLeft = value;
                if (null != anchorLeft)
                {
                    var y = Top + (Bottom - Top) / 2;
                    anchorLeft.SimplexCoordinate = new Pixel
                    {
                        X = Left,
                        Y = y
                    };
                    anchorLeft.DuplexSourceCoordinate = new Pixel
                    {
                        X = Left,
                        Y = y + 10
                    };
                    anchorLeft.DuplexTargetCoordinate = new Pixel
                    {
                        X = Left,
                        Y = y - 10
                    };
                    
                }
            }
        }
        public NodeAnchorModel anchorLeft;
        
        public NodeAnchorModel AnchorTopLeft
        {
            get
            {
                if (null == anchorTopLeft)
                {
                    AnchorTopLeft = new NodeAnchorModel();
                }
                return anchorTopLeft;
            }
            set
            {
                anchorTopLeft = value;
                if (null != anchorTopLeft)
                {
                    anchorTopLeft.SimplexCoordinate = new Pixel
                    {
                        X = Left + CornerRadius * (1 + Math.Cos(225 * Math.PI / 180)),
                        Y = Top + CornerRadius * (1 + Math.Sin(225 * Math.PI / 180))
                    };
                    anchorTopLeft.DuplexSourceCoordinate = new Pixel
                    {
                        X = Left,
                        Y = Top + CornerRadius
                    };
                    anchorTopLeft.DuplexTargetCoordinate = new Pixel
                    {
                        X = Left + CornerRadius,
                        Y = Top
                    };
                }
            }
        }
        public NodeAnchorModel anchorTopLeft;
        
        public NodeAnchorModel AnchorTopCenter
        {
            get
            {
                if (null == anchorTopCenter)
                {
                    AnchorTopCenter = new NodeAnchorModel();
                }
                return anchorTopCenter;
            }
            set
            {
                anchorTopCenter = value;
                if (null != anchorTopCenter)
                {
                    var x = Left + (Right - Left) / 2;
                    anchorTopCenter.SimplexCoordinate = new Pixel
                    {
                        X = x,
                        Y = Top
                    };
                    anchorTopCenter.DuplexSourceCoordinate = new Pixel
                    {
                        X = x - 10,
                        Y = Top
                    };
                    anchorTopCenter.DuplexTargetCoordinate = new Pixel
                    {
                        X = x + 10,
                        Y = Top
                    };
                }
            }
        }
        public NodeAnchorModel anchorTopCenter;

        public NodeAnchorModel AnchorTopRight
        {
            get
            {
                if (null == anchorTopRight)
                {
                    AnchorTopRight = new NodeAnchorModel();
                }
                return anchorTopRight;
            }
            set
            {
                anchorTopRight = value;
                if (null != anchorTopRight)
                {
                    anchorTopRight.SimplexCoordinate = new Pixel
                    {
                        X = Right + CornerRadius * (-1 + Math.Cos(315 * Math.PI / 180)),
                        Y = Top + CornerRadius * (1 + Math.Sin(315 * Math.PI / 180))
                    };
                    anchorTopRight.DuplexSourceCoordinate = new Pixel
                    {
                        X = Right - CornerRadius,
                        Y = Top
                    };
                    anchorTopRight.DuplexTargetCoordinate = new Pixel
                    {
                        X = Right,
                        Y = Top + CornerRadius
                    };
                }
            }
        }
        public NodeAnchorModel anchorTopRight;
        
        public NodeAnchorModel AnchorRight
        {
            get
            {
                if (null == anchorRight)
                {
                    AnchorRight = new NodeAnchorModel();
                }
                return anchorRight;
            }
            set
            {
                anchorRight = value;
                if (null != anchorRight)
                {
                    var y = Top + (Bottom - Top) / 2;
                    anchorRight.SimplexCoordinate = new Pixel
                    {
                        X = Right,
                        Y = y
                    };
                    anchorRight.DuplexSourceCoordinate = new Pixel
                    {
                        X = Right,
                        Y = y - 10
                    };
                    anchorRight.DuplexTargetCoordinate = new Pixel
                    {
                        X = Right,
                        Y = y + 10
                    };
                }
            }
        }
        public NodeAnchorModel anchorRight;
        
        public NodeAnchorModel AnchorBottomRight
        {
            get
            {
                if (null == anchorBottomRight)
                {
                    AnchorBottomRight = new NodeAnchorModel();
                }
                return anchorBottomRight;
            }
            set
            {
                anchorBottomRight = value;
                if (null != anchorBottomRight)
                {
                    anchorBottomRight.SimplexCoordinate = new Pixel
                    {
                        X = Right + CornerRadius * (-1 + Math.Cos(45 * Math.PI / 180)),
                        Y = Bottom + CornerRadius * (-1 + Math.Sin(45 * Math.PI / 180))
                    };
                    anchorBottomRight.DuplexSourceCoordinate = new Pixel
                    {
                        X = Right,
                        Y = Bottom - CornerRadius
                    };
                    anchorBottomRight.DuplexTargetCoordinate = new Pixel
                    {
                        X = Right - CornerRadius,
                        Y = Bottom
                    };
                }
            }
        }
        public NodeAnchorModel anchorBottomRight;

        public NodeAnchorModel AnchorBottomCenter
        {
            get
            {
                if (null == anchorBottomCenter)
                {
                    AnchorBottomCenter = new NodeAnchorModel();
                }
                return anchorBottomCenter;
            }
            set
            {
                anchorBottomCenter = value;
                if (null != anchorBottomCenter)
                {
                    var x = Left + (Right - Left) / 2;
                    anchorBottomCenter.SimplexCoordinate = new Pixel
                    {
                        X = x,
                        Y = Bottom
                    };
                    anchorBottomCenter.DuplexSourceCoordinate = new Pixel
                    {
                        X = x + 10,
                        Y = Bottom
                    };
                    anchorBottomCenter.DuplexTargetCoordinate = new Pixel
                    {
                        X = x - 10,
                        Y = Bottom
                    };
                }
            }
        }
        public NodeAnchorModel anchorBottomCenter;
        
        public NodeAnchorModel AnchorBottomLeft
        {
            get
            {
                if (null == anchorBottomLeft)
                {
                    AnchorBottomLeft = new NodeAnchorModel();
                }
                return anchorBottomLeft;
            }
            set
            {
                anchorBottomLeft = value;
                if (null != anchorBottomLeft)
                {
                    anchorBottomLeft.SimplexCoordinate = new Pixel
                    {
                        X = Left + CornerRadius * (1 + Math.Cos(135 * Math.PI / 180)),
                        Y = Bottom + CornerRadius * (-1 + Math.Sin(135 * Math.PI / 180))
                    };
                    anchorBottomLeft.DuplexSourceCoordinate = new Pixel
                    {
                        X = Left + CornerRadius,
                        Y = Bottom
                    };
                    anchorBottomLeft.DuplexTargetCoordinate = new Pixel
                    {
                        X = Left,
                        Y = Bottom - CornerRadius
                    };
                }
            }
        }
        public NodeAnchorModel anchorBottomLeft;

    }

}
