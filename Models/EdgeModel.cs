
using Grapher.Algorithms;
using System;
using System.ComponentModel;

namespace Grapher.Models
{

    public class EdgeModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public NodeAnchorsModel SourceAnchors
        {
            set
            {
                if (null != sourceAnchors)
                {
                    sourceAnchors.PropertyChanged -= OnAnchorsChanged;
                }
                sourceAnchors = value;
                if (null != sourceAnchors)
                {
                    sourceAnchors.PropertyChanged += OnAnchorsChanged;
                }
                OnAnchorsChanged(null, null);
            }
        }
        private NodeAnchorsModel sourceAnchors;

        public NodeAnchorsModel TargetAnchors
        {
            set
            {
                if (null != targetAnchors)
                {
                    targetAnchors.PropertyChanged -= OnAnchorsChanged;
                }
                targetAnchors = value;
                if (null != targetAnchors)
                {
                    targetAnchors.PropertyChanged += OnAnchorsChanged;
                }
                OnAnchorsChanged(null, null);
            }
        }
        private NodeAnchorsModel targetAnchors;

        private void OnAnchorsChanged(object sender, PropertyChangedEventArgs e)
        {
            sourceAnchor = sourceAnchors.GetAnchor(targetAnchors);
            targetAnchor = targetAnchors.GetAnchor(sourceAnchors);
        }

        private NodeAnchorModel SourceAnchor
        {
            set
            {
                if (null != sourceAnchor)
                {
                    sourceAnchor.PropertyChanged -= OnAnchorChanged;
                    sourceAnchor.IgnoreAsSource(this);
                }
                sourceAnchor = value;
                if(null!=sourceAnchor)
                {
                    sourceAnchor.ObserveAsSource(this);
                    sourceAnchor.PropertyChanged += OnAnchorChanged;
                    OnAnchorChanged(null, null);
                }
            }
        }
        private NodeAnchorModel sourceAnchor;

        private NodeAnchorModel TargetAnchor
        {
            set
            {
                if (null != targetAnchor)
                {
                    targetAnchor.PropertyChanged -= OnAnchorChanged;
                    targetAnchor.IgnoreAsTarget(this);
                }
                targetAnchor = value;
                if (null != targetAnchor)
                {
                    targetAnchor.ObserveAsTarget(this);
                    targetAnchor.PropertyChanged += OnAnchorChanged;
                    OnAnchorChanged(null, null);
                }
            }
        }
        private NodeAnchorModel targetAnchor;

        private void OnAnchorChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == sourceAnchor)
            {
                Source = sourceAnchor.Source;
            }
            else if (sender == targetAnchor)
            {
                Target = targetAnchor.Target;
            }
        }
        
        public Point Source
        {
            get
            {
                return source;
            }
            set
            {
                source     = value;
                angleInRadians      = double.NaN;
                arrowAlpha = default(Point);
                arrowBravo = default(Point);
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Source))
                );
            }
        }
        private Point source;

        public Point Target
        {
            get
            {
                return target;
            }
            set
            {
                target     = value;
                angleInRadians      = double.NaN;
                arrowAlpha = default(Point);
                arrowBravo = default(Point);
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Target))
                );
            }
        }
        private Point target;

        public double AngleInRadians
        {
            get
            {
                if(double.NaN == angleInRadians)
                {
                    angleInRadians = Math.Atan2(
                        Target.Y - Source.Y,
                        Target.X - Source.X 
                    );
                }
                return angleInRadians;
            }
        }
        private double angleInRadians;

        public Point ArrowAlpha
        {
            get
            {
                if (default(Point) == arrowAlpha)
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    var arrowAngle = AngleInRadians - Math.PI / 4;
                    arrowAlpha = new Point(
                        Target.X + ArrowLenght * Math.Cos(arrowAngle),
                        Target.Y + ArrowLenght * Math.Sin(arrowAngle)
                    );
                }
                return arrowAlpha;
            }
        }
        public Point arrowAlpha;

        public Point ArrowBravo
        {
            get
            {
                if(default(Point) == arrowBravo)
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    var arrowAngle  = AngleInRadians + Math.PI / 4;
                    arrowBravo = new Point(
                        Target.X + ArrowLenght * Math.Cos(arrowAngle),
                        Target.Y + ArrowLenght * Math.Sin(arrowAngle)
                    );
                }
                return arrowBravo;
            }
        }
        public Point arrowBravo;

        public double EndpointRadius = 10;

        public string Label;
        public double LabelWidth;
        public double LabelHeight;
        public double LabelLeft;
        public double LabelTop;

        public double MinWidth;
        public double MinHeight;

    }

}
