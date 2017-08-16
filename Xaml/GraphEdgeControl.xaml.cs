using System;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Grapher
{

    public sealed partial class GraphEdgeControl : UserControl
    {

        public GraphEdgeControl(GraphNodeControl from, GraphNodeControl to, String label)
        {

            this.InitializeComponent();

            FromState = from;
            FromState.PropertyChanged += new PropertyChangedEventHandler((n, e) => UpdateLine());

            ToState = to;
            ToState.PropertyChanged += new PropertyChangedEventHandler((n, e) => UpdateLine());

            Label.Text = label;
            UpdateLine();

        }

        private void UpdateLine()
        {

            var FromAnchor = FromState.GetFromAnchorRelativeTo(ToState);
            Baseline.X1 = FromAnchor.X;
            Baseline.Y1 = FromAnchor.Y;

            var ToAnchor = ToState.GetToAnchorRelativeTo(FromState);
            Baseline.X2 = ToAnchor.X;
            Baseline.Y2 = ToAnchor.Y;

            UpdateArrow();
            UpdateLabel();

        }

        private void UpdateArrow()
        {

            var Angle = Math.Atan2(Baseline.Y2 - Baseline.Y1, Baseline.X2 - Baseline.X1) * 180 / Math.PI;

            double ArrowOffsetLength = 10;

            double OffsetAngle = 3 * Math.PI / 4;

            var OffsetAngleAbove = Angle * Math.PI / 180 - OffsetAngle;
            ArrowAbove.X2 = Baseline.X2;
            ArrowAbove.Y2 = Baseline.Y2;
            ArrowAbove.X1 = ArrowAbove.X2 + ArrowOffsetLength * Math.Cos(OffsetAngleAbove);
            ArrowAbove.Y1 = ArrowAbove.Y2 + ArrowOffsetLength * Math.Sin(OffsetAngleAbove);

            var OffsetAngleBelow = Angle * Math.PI / 180 + OffsetAngle;
            ArrowBelow.X2 = Baseline.X2;
            ArrowBelow.Y2 = Baseline.Y2;
            ArrowBelow.X1 = ArrowBelow.X2 + ArrowOffsetLength * Math.Cos(OffsetAngleBelow);
            ArrowBelow.Y1 = ArrowBelow.Y2 + ArrowOffsetLength * Math.Sin(OffsetAngleBelow);

        }

        private void UpdateLabel()
        {

            Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            var rotateTransform = (RotateTransform)Label.RenderTransform;
            rotateTransform.Angle = Math.Atan2(Baseline.Y2 - Baseline.Y1, Baseline.X2 - Baseline.X1) * 180 / Math.PI;

            Point LabelOrigin;

            var ArrowDeltaX = ArrowAbove.X2 - ArrowAbove.X1;
            var ArrowDeltaY = ArrowAbove.Y2 - ArrowAbove.Y1;
            var ArrowDelta = Math.Sqrt(ArrowDeltaX * ArrowDeltaX + ArrowDeltaY * ArrowDeltaY);

            var ArrowOffsetX = ArrowDelta * Math.Cos(rotateTransform.Angle * Math.PI / 180);
            var ArrowOffsetY = ArrowDelta * Math.Sin(rotateTransform.Angle * Math.PI / 180);

            var Xmin = Math.Min(Baseline.X1 + ArrowOffsetX, (Baseline.X2 - ArrowOffsetX));
            var Xdel = Math.Abs(Baseline.X1 + ArrowOffsetX - (Baseline.X2 - ArrowOffsetX)) / 2;
            var Xabs = Xmin + Xdel;

            var Ymin = Math.Min(Baseline.Y1 + ArrowOffsetY, (Baseline.Y2 - ArrowOffsetY));
            var Ydel = Math.Abs(Baseline.Y1 + ArrowOffsetY - (Baseline.Y2 - ArrowOffsetY)) / 2;
            var Yabs = Ymin + Ydel;

            var OffsetMagnitude = Label.DesiredSize.Height / 2;
            var OffsetAngle = (rotateTransform.Angle - 90) * Math.PI / 180;
            var OffsetX = OffsetMagnitude * Math.Cos(OffsetAngle);
            var OffsetY = OffsetMagnitude * Math.Sin(OffsetAngle);

            var CenterX = Xabs + OffsetX;
            var CenterY = Yabs + OffsetY;

            LabelOrigin.X = CenterX - Label.DesiredSize.Width / 2;
            LabelOrigin.Y = CenterY - Label.DesiredSize.Height / 2;

            rotateTransform.CenterX = Label.DesiredSize.Width / 2;
            rotateTransform.CenterY = Label.DesiredSize.Height / 2;
            if (89.9 < rotateTransform.Angle || -90 > rotateTransform.Angle)
            {
                rotateTransform.Angle += 180;
            }

            Canvas.SetTop(Label, LabelOrigin.Y);
            Canvas.SetLeft(Label, LabelOrigin.X);

            MinWidth  = Label.DesiredSize.Width  + Math.Abs((ArrowOffsetX+5)*2);
            MinHeight = Label.DesiredSize.Height + Math.Abs((ArrowOffsetY+5)*2);

        }

        internal void AppendToEvents(string EventName)
        {

            Label.Text += ", " + EventName;
            
            UpdateLabel();
            
        }

        public GraphNodeControl FromState;
        public GraphNodeControl ToState;

    }

}
