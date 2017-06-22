using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher
{

    public sealed partial class Edge : UserControl
    {

        public Edge(State from, State to, String label)
        {

            this.InitializeComponent();

            FromState   = from;
            ToState     = to;

            if(FromState.Center.X < ToState.Center.X)
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X + FromState.Width /2 - FromState.StateBorder.CornerRadius.BottomRight*Math.Cos(Math.PI/4);
                    Baseline.Y1 = FromState.Center.Y + FromState.Height/2 - FromState.StateBorder.CornerRadius.BottomRight*Math.Sin(Math.PI/4);
                    Baseline.X2 =   ToState.Center.X -   ToState.Width /2 +   ToState.StateBorder.CornerRadius.TopLeft    *Math.Cos(Math.PI/4);
                    Baseline.Y2 =   ToState.Center.Y -   ToState.Height/2 +   ToState.StateBorder.CornerRadius.TopLeft    *Math.Sin(Math.PI/4);
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X + FromState.Width /2 - FromState.StateBorder.CornerRadius.TopRight  *Math.Cos(Math.PI/4);
                    Baseline.Y1 = FromState.Center.Y - FromState.Height/2 + FromState.StateBorder.CornerRadius.TopRight  *Math.Sin(Math.PI/4);
                    Baseline.X2 =   ToState.Center.X -   ToState.Width /2 +   ToState.StateBorder.CornerRadius.BottomLeft*Math.Cos(Math.PI/4);
                    Baseline.Y2 =   ToState.Center.Y +   ToState.Height/2 -   ToState.StateBorder.CornerRadius.BottomLeft*Math.Sin(Math.PI/4);
                }
                else
                {
                    Baseline.X1 = FromState.Center.X + FromState.Width/2;
                    Baseline.Y1 = FromState.Center.Y;
                    Baseline.X2 =   ToState.Center.X -   ToState.Width/2;
                    Baseline.Y2 =   ToState.Center.Y;
                }
            }
            else if(FromState.Center.X > ToState.Center.X)
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X - FromState.Width /2 + FromState.StateBorder.CornerRadius.BottomLeft*Math.Cos(Math.PI/4);
                    Baseline.Y1 = FromState.Center.Y + FromState.Height/2 - FromState.StateBorder.CornerRadius.BottomLeft*Math.Sin(Math.PI/4);
                    Baseline.X2 =   ToState.Center.X +   ToState.Width /2 -   ToState.StateBorder.CornerRadius.TopRight  *Math.Cos(Math.PI/4);
                    Baseline.Y2 =   ToState.Center.Y -   ToState.Height/2 +   ToState.StateBorder.CornerRadius.TopRight  *Math.Sin(Math.PI/4);
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X - FromState.Width /2 + FromState.StateBorder.CornerRadius.TopLeft    *Math.Cos(Math.PI/4);
                    Baseline.Y1 = FromState.Center.Y - FromState.Height/2 + FromState.StateBorder.CornerRadius.TopLeft    *Math.Sin(Math.PI/4);
                    Baseline.X2 =   ToState.Center.X + ToState.Width   /2 -   ToState.StateBorder.CornerRadius.BottomRight*Math.Cos(Math.PI/4);
                    Baseline.Y2 =   ToState.Center.Y +   ToState.Height/2 -   ToState.StateBorder.CornerRadius.BottomRight*Math.Sin(Math.PI/4);
                }
                else
                {
                    Baseline.X1 = FromState.Center.X - FromState.Width / 2;
                    Baseline.Y1 = FromState.Center.Y;
                    Baseline.X2 =   ToState.Center.X + ToState.Width / 2;
                    Baseline.Y2 =   ToState.Center.Y;
                }
            }
            else
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X;
                    Baseline.X2 =   ToState.Center.X;
                    Baseline.Y1 = FromState.Center.Y + FromState.Height/2;
                    Baseline.Y2 =   ToState.Center.Y -   ToState.Height/2;
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.Center.X;
                    Baseline.X2 =   ToState.Center.X;
                    Baseline.Y1 = FromState.Center.Y - FromState.Height/2;
                    Baseline.Y2 =   ToState.Center.Y +   ToState.Height/2;
                }
                else
                {   // this is actually an error state, they overlap
                    Baseline.X1 = FromState.Center.X;
                    Baseline.X2 =   ToState.Center.X;
                    Baseline.Y1 = FromState.Center.Y;
                    Baseline.Y2 =   ToState.Center.Y;
                }
            }

            Label.Text = label;
            Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            var rotateTransform = (RotateTransform)Label.RenderTransform;
            rotateTransform.Angle   = Math.Atan2(Baseline.Y2 - Baseline.Y1, Baseline.X2 - Baseline.X1) * 180 / Math.PI;

            {   // Place the arrow head

                double OffsetMagnitude = 10;
                double OffsetAngle     = 3 * Math.PI / 4;

                var OffsetAngleAbove = rotateTransform.Angle * Math.PI / 180 - OffsetAngle;
                ArrowAbove.X2 = Baseline.X2;
                ArrowAbove.Y2 = Baseline.Y2;
                ArrowAbove.X1 = ArrowAbove.X2 + OffsetMagnitude * Math.Cos(OffsetAngleAbove);
                ArrowAbove.Y1 = ArrowAbove.Y2 + OffsetMagnitude * Math.Sin(OffsetAngleAbove);

                var OffsetAngleBelow = rotateTransform.Angle * Math.PI / 180 + OffsetAngle;
                ArrowBelow.X2 = Baseline.X2;
                ArrowBelow.Y2 = Baseline.Y2;
                ArrowBelow.X1 = ArrowBelow.X2 + OffsetMagnitude * Math.Cos(OffsetAngleBelow);
                ArrowBelow.Y1 = ArrowBelow.Y2 + OffsetMagnitude * Math.Sin(OffsetAngleBelow);

            }

            {   // Place the label

                Point LabelOrigin;

                var Xmin = Math.Min(Baseline.X1, Baseline.X2);
                var Xdel = Math.Abs(Baseline.X1 - Baseline.X2) / 2;
                var Xabs = Xmin + Xdel;

                var Ymin = Math.Min(Baseline.Y1, Baseline.Y2);
                var Ydel = Math.Abs(Baseline.Y1 - Baseline.Y2) / 2;
                var Yabs = Ymin + Ydel;

                var OffsetMagnitude = Label.DesiredSize.Height/2;
                var OffsetAngle     = (rotateTransform.Angle-90) * Math.PI / 180;
                var OffsetX         = OffsetMagnitude * Math.Cos(OffsetAngle);
                var OffsetY         = OffsetMagnitude * Math.Sin(OffsetAngle);

                var CenterX = Xabs + OffsetX;
                var CenterY = Yabs + OffsetY;

                LabelOrigin.X = CenterX - Label.DesiredSize.Width / 2;
                LabelOrigin.Y = CenterY - Label.DesiredSize.Height / 2;

                rotateTransform.CenterX = Label.DesiredSize.Width/2;
                rotateTransform.CenterY = Label.DesiredSize.Height/2;
                if (89.9 < rotateTransform.Angle || -90 > rotateTransform.Angle)
                {
                    rotateTransform.Angle += 180;
                }

                Canvas.SetTop(Label, LabelOrigin.Y);
                Canvas.SetLeft(Label, LabelOrigin.X);

            }

        }

        private State FromState;
        private State ToState;

    }

}
