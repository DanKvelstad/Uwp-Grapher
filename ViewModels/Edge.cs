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

        private string _Label;
        public  string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                if(_Label != value)
                {
                    _Label = value;
                    OnPropertyChanged("Label");
                }
            }
        }
        
        private Node   SourceNode;
        private Anchor SourceAnchor;

        private Node   TargetNode;
        private Anchor TargetAnchor;

        public double SourceX
        {
            get
            {
                return SourceAnchor.Source.X;
            }
        }
        public double SourceY
        {
            get
            {
                return SourceAnchor.Source.Y;
            }
        }

        public double TargetX
        {
            get
            {
                return TargetAnchor.Target.X;
            }
        }
        public double TargetY
        {
            get
            {
                return TargetAnchor.Target.Y;
            }
        }

        public double _ArrowAlphaX;
        public double ArrowAlphaX
        {
            get
            {
                return _ArrowAlphaX;
            }
            set
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
            set
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
            set
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
            set
            {
                _ArrowBravoY = value;
                OnPropertyChanged("ArrowBravoY");
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
                OnPropertyChanged("SourceX");
                OnPropertyChanged("SourceY");
                UpdateArrow();
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
                OnPropertyChanged("TargetX");
                OnPropertyChanged("TargetY");
                UpdateArrow();
            }
            else
            {
                throw new ArgumentException();
            }

        }

        private void UpdateArrow()
        {

            if(null!=SourceAnchor && null!=TargetAnchor)
            {

                var Angle = Math.Atan2(TargetY - SourceY, TargetX - SourceX) * 180 / Math.PI;
                double ArrowOffsetLength = 10;
                double OffsetAngle = 3 * Math.PI / 4;

                var OffsetAngleAbove = Angle * Math.PI / 180 - OffsetAngle;
                ArrowAlphaX = TargetX + ArrowOffsetLength * Math.Cos(OffsetAngleAbove);
                ArrowAlphaY = TargetY + ArrowOffsetLength * Math.Sin(OffsetAngleAbove);

                var OffsetAngleBelow = Angle * Math.PI / 180 + OffsetAngle;
                ArrowBravoX = TargetX + ArrowOffsetLength * Math.Cos(OffsetAngleBelow);
                ArrowBravoY = TargetY + ArrowOffsetLength * Math.Sin(OffsetAngleBelow);

            }

        }

        //private void UpdateLabel()
        //{
        //
        //    Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
        //
        //    var rotateTransform = (RotateTransform)Label.RenderTransform;
        //    rotateTransform.Angle = Math.Atan2(TargetY - Baseline.Y1, TargetX - SourceX) * 180 / Math.PI;
        //
        //    Point LabelOrigin;
        //
        //    var ArrowDeltaX = ArrowAbove.X2 - ArrowAlphaX;
        //    var ArrowDeltaY = ArrowAbove.Y2 - ArrowAlphaY;
        //    var ArrowDelta = Math.Sqrt(ArrowDeltaX * ArrowDeltaX + ArrowDeltaY * ArrowDeltaY);
        //
        //    var ArrowOffsetX = ArrowDelta * Math.Cos(rotateTransform.Angle * Math.PI / 180);
        //    var ArrowOffsetY = ArrowDelta * Math.Sin(rotateTransform.Angle * Math.PI / 180);
        //
        //    var Xmin = Math.Min(SourceX + ArrowOffsetX, (TargetX - ArrowOffsetX));
        //    var Xdel = Math.Abs(SourceX + ArrowOffsetX - (TargetX - ArrowOffsetX)) / 2;
        //    var Xabs = Xmin + Xdel;
        //
        //    var Ymin = Math.Min(Baseline.Y1 + ArrowOffsetY, (TargetY - ArrowOffsetY));
        //    var Ydel = Math.Abs(Baseline.Y1 + ArrowOffsetY - (TargetY - ArrowOffsetY)) / 2;
        //    var Yabs = Ymin + Ydel;
        //
        //    var OffsetMagnitude = Label.DesiredSize.Height / 2;
        //    var OffsetAngle = (rotateTransform.Angle - 90) * Math.PI / 180;
        //    var OffsetX = OffsetMagnitude * Math.Cos(OffsetAngle);
        //    var OffsetY = OffsetMagnitude * Math.Sin(OffsetAngle);
        //
        //    var CenterX = Xabs + OffsetX;
        //    var CenterY = Yabs + OffsetY;
        //
        //    LabelOrigin.X = CenterX - Label.DesiredSize.Width / 2;
        //    LabelOrigin.Y = CenterY - Label.DesiredSize.Height / 2;
        //
        //    rotateTransform.CenterX = Label.DesiredSize.Width / 2;
        //    rotateTransform.CenterY = Label.DesiredSize.Height / 2;
        //    if (89.9 < rotateTransform.Angle || -90 > rotateTransform.Angle)
        //    {
        //        rotateTransform.Angle += 180;
        //    }
        //
        //    // Canvas.SetTop(Label, LabelOrigin.Y);
        //    // Canvas.SetLeft(Label, LabelOrigin.X);
        //
        //    MinWidth = Label.DesiredSize.Width + Math.Abs((ArrowOffsetX + 5) * 2);
        //    MinHeight = Label.DesiredSize.Height + Math.Abs((ArrowOffsetY + 5) * 2);
        //
        //}

        //public void AppendToEvents(string EventName)
        //{
        //
        //    Label += ", " + EventName;
        //    
        //    UpdateLabel();
        //    
        //}

    }

}
