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
                OnPropertyChanged("SourceX");
                Angle = double.NaN;
            }
        }
        public  double SourceActualX
        {
            get
            {
                return SourceAnchor.Source.X;
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
                OnPropertyChanged("SourceY");
                Angle = double.NaN;
            }
        }
        public  double SourceActualY
        {
            get
            {
                return SourceAnchor.Source.Y;
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
                    OnPropertyChanged("TargetX");
                    Angle = double.NaN;
                }
            }
        }
        public double TargetActualX
        {
            get
            {
                return TargetAnchor.Target.X;
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
                    OnPropertyChanged("TargetY");
                    Angle = double.NaN;
                }
            }
        }
        public  double TargetActualY
        {
            get
            {
                return TargetAnchor.Target.Y;
            }
        }

        public double _Angle;
        public double Angle
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
                    ) * 180 / Math.PI;
                    if(0>_Angle)
                    {
                        _Angle += 360;
                    }
                    OnPropertyChanged("Angle");
                }
                else
                {
                    throw new ArgumentException();
                }

                LabelLeft = double.NaN;
                LabelTop  = double.NaN;

                var AngleInRadians = _Angle / 180 * Math.PI;
                // isosceles triangle
                // EndpointRadius = sqrt(ArrowLenght^2-(1/4)*(EndpointRadius*2)^2)
                // EndpointRadius^2+(1/4)*(EndpointRadius*2)^2 = ArrowLenght^2
                // ArrowLenght = sqrt(EndpointRadius^2+(1/4)*(EndpointRadius*2)^2)
                var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                ArrowAlphaX = TargetX + ArrowLenght * Math.Cos(AngleInRadians - Math.PI / 4);
                ArrowAlphaY = TargetY + ArrowLenght * Math.Sin(AngleInRadians - Math.PI / 4);
                ArrowBravoX = TargetX + ArrowLenght * Math.Cos(AngleInRadians + Math.PI / 4);
                ArrowBravoY = TargetY + ArrowLenght * Math.Sin(AngleInRadians + Math.PI / 4);

            }
        }
        
        public double _ArrowAlphaX;
        public double ArrowAlphaX
        {
            get
            {
                return _ArrowAlphaX;
            }
            private set
            {
                _ArrowAlphaX = value;
                OnPropertyChanged("ArrowAlphaX");
            }
        }
        public double _ArrowAlphaY;
        public double ArrowAlphaY
        {
            get
            {
                return _ArrowAlphaY;
            }
            private set
            {
                _ArrowAlphaY = value;
                OnPropertyChanged("ArrowAlphaY");
            }
        }
        
        public double _ArrowBravoX;
        public double ArrowBravoX
        {
            get
            {
                return _ArrowBravoX;
            }
            private set
            {
                _ArrowBravoX = value;
                OnPropertyChanged("ArrowBravoX");
            }
        }
        public double _ArrowBravoY;
        public double ArrowBravoY
        {
            get
            {
                return _ArrowBravoY;
            }
            private set
            {
                _ArrowBravoY = value;
                OnPropertyChanged("ArrowBravoY");
            }
        }

        private string _Label;
        public string Label
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
                    OnPropertyChanged("Label");
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
                    OnPropertyChanged("LabelWidth");
                    LabelLeft = double.NaN;
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
                    OnPropertyChanged("LabelHeight");
                    LabelTop = double.NaN;
                }
            }
        }

        private double _LabelLeft;
        public double LabelLeft
        {
            get
            {
                return _LabelLeft;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    _LabelLeft = Math.Min(SourceX, TargetX) + Math.Abs(SourceX - TargetX) / 2 - LabelWidth / 2;
                    OnPropertyChanged("LabelLeft");
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private double _LabelTop;
        public double LabelTop
        {
            get
            {
                return _LabelTop;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var EdgeCenterY = Math.Min(SourceY, TargetY) + Math.Abs(SourceY - TargetY) / 2;
                    _LabelTop = EdgeCenterY - LabelHeight;
                    OnPropertyChanged("LabelTop");
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public double _EndpointRadius = 10;
        public double EndpointRadius
        {
            get
            {
                return _EndpointRadius;
            }
            set
            {
                _EndpointRadius = value;
                OnPropertyChanged("EndpointRadius");
            }
        }

        public Edge(Node Source, Node Target, String Label)
        {

            SourceNode                       = Source;
            SourceNode.PropertyChanged      += SourceNodeChanged;
            SourceAnchor                     = Source.GetAnchorRelativeTo(Target);
            if (null != SourceAnchor)
            {
                SourceAnchor.PropertyChanged += SourceAnchorChanged;
                SourceAnchorChanged(SourceAnchor, null);
            }

            TargetNode                       = Target;
            TargetNode.PropertyChanged      += TargetNodeChanged;
            TargetAnchor                     = Target.GetAnchorRelativeTo(Source);
            if(null != TargetAnchor)
            {
                TargetAnchor.PropertyChanged += TargetAnchorChanged;
                TargetAnchorChanged(TargetAnchor, null);
            }

            this.Label = Label;

        }
        
        public void Dispose()
        {
            SourceNode.PropertyChanged -= SourceNodeChanged;
            TargetNode.PropertyChanged -= TargetNodeChanged;
            // ToDo think about the anchor callbacks!
        }

        private void SourceNodeChanged(object sender, PropertyChangedEventArgs e)
        {

            var Source = sender as Node;
            if (null != Source && Source == SourceNode)
            {
                if("Left"==e.PropertyName || "Top" == e.PropertyName)
                {
                    if(null != SourceAnchor)
                    {
                        SourceAnchor.PropertyChanged -= SourceAnchorChanged;
                    }
                    SourceAnchor = SourceNode.GetAnchorRelativeTo(TargetNode);
                    if (null != SourceAnchor)
                    {
                        SourceAnchor.PropertyChanged += SourceAnchorChanged;
                        SourceAnchorChanged(SourceAnchor, null);
                    }
                    // SourceNode will call Anchor changed, so dont do that here
                    // SourceAnchorChanged(SourceAnchor, null);
                }
                // else the node has not moved and we don't need to reevaluate the anchor
            }
            else
            {
                throw new ArgumentException();
            }

        }

        private void SourceAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            var Anchor = sender as Anchor;
            if(null != Anchor && Anchor == SourceAnchor)
            {

                OnPropertyChanged("SourceActualX");
                OnPropertyChanged("SourceActualY");

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

        private void TargetNodeChanged(object sender, PropertyChangedEventArgs e)
        {

            var Target = sender as Node;
            if (null != Target && Target == TargetNode)
            {
                if ("Left" == e.PropertyName || "Top" == e.PropertyName)
                {
                    if (null != TargetAnchor)
                    {
                        TargetAnchor.PropertyChanged -= TargetAnchorChanged;
                    }
                    TargetAnchor = TargetNode.GetAnchorRelativeTo(SourceNode);
                    if (null != TargetAnchor)
                    {
                        TargetAnchor.PropertyChanged += TargetAnchorChanged;
                        TargetAnchorChanged(TargetAnchor, null);
                    }
                    // TargetNode will call Anchor changed, so dont do that here
                    // TargetAnchorChanged(TargetAnchor, null);
                }
                // else the node has not moved and we don't need to reevaluate the anchor
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

                OnPropertyChanged("TargetActualX");
                OnPropertyChanged("TargetActualY");

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
