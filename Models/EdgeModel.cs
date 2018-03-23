using System;
using System.ComponentModel;

namespace Grapher.Models
{

    public class EdgeModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public DimensionsModel Dimensions
        {
            get;
            set;
        }

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
        private double localWidth;

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
        private double localHeight;

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
        
        public double AngleInRadians
        {
            get
            {
                return Math.Atan2(
                    Target.Y - Source.Y,
                    Target.X - Source.X 
                );
            }
        }
        
        public Pixel ArrowAlpha
        {
            get
            {
                var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                var arrowAngle = AngleInRadians - Math.PI / 4;
                return new Pixel
                {
                    X = Target.X + ArrowLenght * Math.Cos(arrowAngle),
                    Y = Target.Y + ArrowLenght * Math.Sin(arrowAngle)
                };
            }
        }
        
        public Pixel ArrowBravo
        {
            get
            {
                var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                var arrowAngle  = AngleInRadians + Math.PI / 4;
                return new Pixel{
                    X = Target.X + ArrowLenght * Math.Cos(arrowAngle),
                    Y = Target.Y + ArrowLenght * Math.Sin(arrowAngle)
                };
            }
        }

        public double LabelWidth
        {
            get
            {
                return labelWidth;
            }
            set
            {
                if (labelWidth != value)
                {
                    labelWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LabelWidth))
                    );
                }
                ToDo // calculate the local width/height
            }
        }
        private double labelWidth;

        public double LabelHeight
        {
            get
            {
                return labelHeight;
            }
            set
            {
                if (labelHeight != value)
                {
                    labelHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LabelHeight))
                    );
                    ToDo // calculate the local width/height
                }
            }
        }
        private double labelHeight;

        public double EndpointRadius
        {
            get
            {
                return endpointRadius;
            }
            set
            {
                endpointRadius = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(EndpointRadius))
                );
            }
        }
        private double endpointRadius = 10;
    }

}
