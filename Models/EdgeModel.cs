
using Grapher.Algorithms;
using System.ComponentModel;

namespace Grapher.Models
{

    public class EdgeModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                if(value != label)
                {
                    label = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Label))
                    );
                }
            }
        }
        private string label;

        public double LocalWidth
        {
            get
            {
                return localWidth;
            }
            set
            {
                if (value != localWidth)
                {
                    localWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LocalWidth))
                    );
                }
            }
        }
        private double localWidth = 50;

        public double LocalHeight
        {
            get
            {
                return localHeight;
            }
            set
            {
                if (value != localHeight)
                {
                    localHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LocalHeight))
                    );
                }
            }
        }
        private double localHeight = 50;

        public NodeGeometryModel SourceGeometry
        {
            private get
            {
                return sourceGeometry;
            }
            set
            {
                if (null != sourceGeometry)
                {
                    sourceGeometry.PropertyChanged -= Geometry_PropertyChanged;
                }
                sourceGeometry = value;
                if (null != sourceGeometry)
                {
                    sourceGeometry.PropertyChanged += Geometry_PropertyChanged;
                }
            }
        }
        private NodeGeometryModel sourceGeometry;

        public NodeGeometryModel TargetGeometry
        {
            private get
            {
                return targetGeometry;
            }
            set
            {
                if (null != targetGeometry)
                {
                    targetGeometry.PropertyChanged -= Geometry_PropertyChanged;
                }
                targetGeometry = value;
                if (null != targetGeometry)
                {
                    targetGeometry.PropertyChanged += Geometry_PropertyChanged;
                }
            }
        }
        private NodeGeometryModel targetGeometry;

        private void Geometry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SourceAnchor = SourceGeometry.GetAnchor(TargetGeometry);
            TargetAnchor = TargetGeometry.GetAnchor(SourceGeometry);
        }

        private NodeAnchorModel SourceAnchor
        {
            get
            {
                if(null == sourceAnchor)
                {
                    SourceAnchor = SourceGeometry.GetAnchor(TargetGeometry);
                }
                return sourceAnchor;
            }
            set
            {
                if(sourceAnchor != value)
                {
                    if (null != sourceAnchor)
                    {
                        sourceAnchor.PropertyChanged -= OnAnchorChanged;
                    }
                    sourceAnchor = value;
                    if (null != sourceAnchor)
                    {
                        sourceAnchor.PropertyChanged += OnAnchorChanged;
                    }
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Source))
                    );
                }
            }
        }
        private NodeAnchorModel sourceAnchor;

        private NodeAnchorModel TargetAnchor
        {
            get
            {
                if (null == targetAnchor)
                {
                    TargetAnchor = TargetGeometry.GetAnchor(SourceGeometry);
                }
                return targetAnchor;
            }
            set
            {
                if (targetAnchor != value)
                {
                    if (null != targetAnchor)
                    {
                        targetAnchor.PropertyChanged -= OnAnchorChanged;
                    }
                    targetAnchor = value;
                    if (null != targetAnchor)
                    {
                        targetAnchor.PropertyChanged += OnAnchorChanged;
                    }
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Target))
                    );
                }
            }
        }
        private NodeAnchorModel targetAnchor;

        private void OnAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            if(sender == SourceAnchor && nameof(NodeAnchorModel.Source) == e.PropertyName)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Source))
                );
            }
            else if (sender == TargetAnchor && nameof(NodeAnchorModel.Target) == e.PropertyName)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Target))
                );
            }
            
        }
        
        public Pixel Source
        {
            get
            {
                return SourceAnchor.Source;
            }
        }
        
        public Pixel Target
        {
            get
            {
                return TargetAnchor.Target;
            }
        }
        
        //public double AngleInRadians
        //{
        //    get
        //    {
        //        if(double.NaN == angleInRadians)
        //        {
        //            angleInRadians = Math.Atan2(
        //                Target.Y - Source.Y,
        //                Target.X - Source.X 
        //            );
        //        }
        //        return angleInRadians;
        //    }
        //}
        //private double angleInRadians;
        //
        //public Point ArrowAlpha
        //{
        //    get
        //    {
        //        if (Point.IsValid() == arrowAlpha)
        //        {
        //            var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
        //            var arrowAngle = AngleInRadians - Math.PI / 4;
        //            arrowAlpha = new Point(
        //                Target.X + ArrowLenght * Math.Cos(arrowAngle),
        //                Target.Y + ArrowLenght * Math.Sin(arrowAngle)
        //            );
        //        }
        //        return arrowAlpha;
        //    }
        //}
        //public Point arrowAlpha;
        //
        //public Point ArrowBravo
        //{
        //    get
        //    {
        //        if(Point.IsValid() == arrowBravo)
        //        {
        //            var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
        //            var arrowAngle  = AngleInRadians + Math.PI / 4;
        //            arrowBravo = new Point(
        //                Target.X + ArrowLenght * Math.Cos(arrowAngle),
        //                Target.Y + ArrowLenght * Math.Sin(arrowAngle)
        //            );
        //        }
        //        return arrowBravo;
        //    }
        //}
        //public Point arrowBravo;
        //
        //public double EndpointRadius = 10;
        //
        //public string Label;
        //public double LabelWidth;
        //public double LabelHeight;
        //public double LabelLeft;
        //public double LabelTop;
        //
        //public double MinWidth;
        //public double MinHeight;

    }

}
