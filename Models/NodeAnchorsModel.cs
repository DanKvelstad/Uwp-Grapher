using Grapher.Algorithms;
using System;
using System.ComponentModel;

namespace Grapher.Models
{

    public class NodeAnchorsModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
        internal void NodeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            var nodeModel = sender as NodeModel;

            switch (e.PropertyName)
            {
                case nameof(NodeModel.Width):
                    AnchorBottomLeftUpdate(nodeModel);
                    AnchorLeftUpdate(nodeModel);
                    AnchorTopLeftUpdate(nodeModel);
                    AnchorTopRightUpdate(nodeModel);
                    AnchorRightUpdate(nodeModel);
                    AnchorBottomRightUpdate(nodeModel);
                    break;
                case nameof(NodeModel.Height):
                    AnchorTopLeftUpdate(nodeModel);
                    AnchorTopCenterUpdate(nodeModel);
                    AnchorTopRightUpdate(nodeModel);
                    AnchorBottomRightUpdate(nodeModel);
                    AnchorBottomCenterUpdate(nodeModel);
                    AnchorBottomLeftUpdate(nodeModel);
                    break;
                case nameof(NodeModel.CornerRadius):
                    AnchorTopLeftUpdate(nodeModel);
                    AnchorTopRightUpdate(nodeModel);
                    AnchorBottomRightUpdate(nodeModel);
                    AnchorBottomLeftUpdate(nodeModel);
                    break;
                case nameof(NodeModel.Center):
                    AnchorLeftUpdate(nodeModel);
                    AnchorTopLeftUpdate(nodeModel);
                    AnchorTopCenterUpdate(nodeModel);
                    AnchorTopRightUpdate(nodeModel);
                    AnchorRightUpdate(nodeModel);
                    AnchorBottomRightUpdate(nodeModel);
                    AnchorBottomCenterUpdate(nodeModel);
                    AnchorBottomLeftUpdate(nodeModel);
                    break;
            }
        }

        public NodeAnchorModel GetAnchor(NodeAnchorsModel other)
        {   // Remember that a growing X means moving right and that
            // a growing Y means moving downwards!
            
            if(AnchorLeft.SimplexCoordinate.X == other.AnchorLeft.SimplexCoordinate.X)
            {
                if(AnchorLeft.SimplexCoordinate.Y < other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorBottomCenter;
                }
                else if (AnchorLeft.SimplexCoordinate.Y > other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorTopCenter;
                }
            }
            else if(AnchorLeft.SimplexCoordinate.Y == other.AnchorLeft.SimplexCoordinate.Y)
            {
                if (AnchorLeft.SimplexCoordinate.X < other.AnchorLeft.SimplexCoordinate.X)
                {
                    return AnchorRight;
                }
                else if (AnchorLeft.SimplexCoordinate.X > other.AnchorLeft.SimplexCoordinate.X)
                {
                    return AnchorLeft;
                }
            }
            else if(AnchorLeft.SimplexCoordinate.X < other.AnchorLeft.SimplexCoordinate.X)
            {
                if (AnchorLeft.SimplexCoordinate.Y < other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorBottomRight;
                }
                else if (AnchorLeft.SimplexCoordinate.Y > other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorTopRight;
                }
            }
            else if (AnchorLeft.SimplexCoordinate.X > other.AnchorLeft.SimplexCoordinate.X)
            {
                if (AnchorLeft.SimplexCoordinate.Y < other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorBottomLeft;
                }
                else if (AnchorLeft.SimplexCoordinate.Y > other.AnchorLeft.SimplexCoordinate.Y)
                {
                    return AnchorTopLeft;
                }
            }

            throw new Exception();

        }

        private void AnchorLeftUpdate(NodeModel model)
        {
            var Left = model.Center.X - model.Width  / 2;
            AnchorLeft.NewCoordinates(
                new Point(Left, model.Center.Y),
                new Point(Left, model.Center.Y + 10),
                new Point(Left, model.Center.Y - 10)
            );
        }
        public NodeAnchorModel AnchorLeft
        {
            get
            {
                if (null == anchorLeft)
                {
                    anchorLeft = new NodeAnchorModel();
                }
                return anchorLeft;
            }
        }
        public NodeAnchorModel anchorLeft;

        private void AnchorTopLeftUpdate(NodeModel model)
        {
            var Left = model.Center.X - model.Width / 2;
            var Top = model.Center.Y - model.Height / 2;
            AnchorTopLeft.NewCoordinates(
                new Point(Left + model.CornerRadius * (1 + Math.Cos(225 * Math.PI / 180)),
                          Top  + model.CornerRadius * (1 + Math.Sin(225 * Math.PI / 180))),
                new Point(Left, Top + model.CornerRadius),
                new Point(Left + model.CornerRadius, Top)
            );
        }
        public NodeAnchorModel AnchorTopLeft
        {
            get
            {
                if (null == anchorTopLeft)
                {
                    anchorTopLeft = new NodeAnchorModel();
                }
                return anchorTopLeft;
            }
        }
        public NodeAnchorModel anchorTopLeft;

        private void AnchorTopCenterUpdate(NodeModel model)
        {
            var Top = model.Center.Y - model.Height / 2;
            AnchorTopCenter.NewCoordinates(
                new Point(model.Center.X, Top),
                new Point(model.Center.X - 10, Top),
                new Point(model.Center.X + 10, Top)
            );
        }
        public NodeAnchorModel AnchorTopCenter
        {
            get
            {
                if (null == anchorTopCenter)
                {
                    anchorTopCenter = new NodeAnchorModel();
                }
                return anchorTopCenter;
            }
        }
        public NodeAnchorModel anchorTopCenter;

        private void AnchorTopRightUpdate(NodeModel model)
        {
            var Top   = model.Center.Y - model.Height / 2;
            var Right = model.Center.X + model.Width  / 2;
            AnchorTopRight.NewCoordinates(
                new Point(Right + model.CornerRadius * (-1 + Math.Cos(315 * Math.PI / 180)),
                          Top   + model.CornerRadius * (1 + Math.Sin(315 * Math.PI / 180))),
                new Point(Right - model.CornerRadius, Top),
                new Point(Right, Top + model.CornerRadius)
            );
        }
        public NodeAnchorModel AnchorTopRight
        {
            get
            {
                if (null == anchorTopRight)
                {
                    anchorTopRight = new NodeAnchorModel();
                }
                return anchorTopRight;
            }
        }
        public NodeAnchorModel anchorTopRight;

        private void AnchorRightUpdate(NodeModel model)
        {
            var Right = model.Center.X + model.Width / 2;
            AnchorRight.NewCoordinates(
                new Point(Right, model.Center.Y),
                new Point(Right, model.Center.Y - 10),
                new Point(Right, model.Center.Y + 10)
            );
        }
        public NodeAnchorModel AnchorRight
        {
            get
            {
                if (null == anchorRight)
                {
                    anchorRight = new NodeAnchorModel();
                }
                return anchorRight;
            }
        }
        public NodeAnchorModel anchorRight;

        private void AnchorBottomRightUpdate(NodeModel model)
        {
            var Bottom = model.Center.Y + model.Height / 2;
            var Right  = model.Center.X + model.Width  / 2;
            AnchorBottomRight.NewCoordinates(
                new Point(Right  + model.CornerRadius * (-1 + Math.Cos(45 * Math.PI / 180)),
                          Bottom + model.CornerRadius * (-1 + Math.Sin(45 * Math.PI / 180))),
                new Point(Right, Bottom - model.CornerRadius),
                new Point(Right - model.CornerRadius, Bottom)
            );
        }
        public NodeAnchorModel AnchorBottomRight
        {
            get
            {
                if (null == anchorBottomRight)
                {
                    anchorBottomRight = new NodeAnchorModel();
                }
                return anchorBottomRight;
            }
        }
        public NodeAnchorModel anchorBottomRight;

        private void AnchorBottomCenterUpdate(NodeModel model)
        {
            var Bottom = model.Center.Y + model.Height / 2;
            AnchorBottomCenter.NewCoordinates(
                new Point(model.Center.X,      Bottom),
                new Point(model.Center.X + 10, Bottom),
                new Point(model.Center.X - 10, Bottom)
            );
        }
        public NodeAnchorModel AnchorBottomCenter
        {
            get
            {
                if (null == anchorBottomCenter)
                {
                    anchorBottomCenter = new NodeAnchorModel();
                }
                return anchorBottomCenter;
            }
        }
        public NodeAnchorModel anchorBottomCenter;

        private void AnchorBottomLeftUpdate(NodeModel model)
        {
            var Bottom = model.Center.Y + model.Height / 2;
            var Left   = model.Center.X - model.Width  / 2;
            AnchorBottomLeft.NewCoordinates(
                new Point(Left + model.CornerRadius * (1 + Math.Cos(135 * Math.PI / 180)),
                          Bottom + model.CornerRadius * (-1 + Math.Sin(135 * Math.PI / 180))),
                new Point(Left + model.CornerRadius, Bottom),
                new Point(Left, Bottom - model.CornerRadius)
            );
        }
        public NodeAnchorModel AnchorBottomLeft
        {
            get
            {
                if (null == anchorBottomLeft)
                {
                    anchorBottomLeft = new NodeAnchorModel();
                }
                return anchorBottomLeft;
            }
        }
        public NodeAnchorModel anchorBottomLeft;

    }

}
