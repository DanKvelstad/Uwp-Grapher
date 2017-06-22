﻿using System;
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
                    Baseline.X1 = FromState.AnchorBottomRight.X;
                    Baseline.Y1 = FromState.AnchorBottomRight.Y;
                    Baseline.X2 =   ToState.AnchorTopLeft.X;
                    Baseline.Y2 =   ToState.AnchorTopLeft.Y;
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.AnchorTopRight.X;
                    Baseline.Y1 = FromState.AnchorTopRight.Y;
                    Baseline.X2 =   ToState.AnchorBottomLeft.X;
                    Baseline.Y2 =   ToState.AnchorBottomLeft.Y;
                }
                else
                {
                    Baseline.X1 = FromState.AnchorRight.X;
                    Baseline.Y1 = FromState.AnchorRight.Y;
                    Baseline.X2 =   ToState.AnchorLeft.X;
                    Baseline.Y2 =   ToState.AnchorLeft.Y;
                }
            }
            else if(FromState.Center.X > ToState.Center.X)
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    Baseline.X1 = FromState.AnchorBottomLeft.X;
                    Baseline.Y1 = FromState.AnchorBottomLeft.Y;
                    Baseline.X2 =   ToState.AnchorTopRight.X;
                    Baseline.Y2 =   ToState.AnchorTopRight.Y;
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.AnchorTopLeft.X;
                    Baseline.Y1 = FromState.AnchorTopLeft.Y;
                    Baseline.X2 =   ToState.AnchorBottomRight.X;
                    Baseline.Y2 =   ToState.AnchorBottomRight.Y;
                }
                else
                {
                    Baseline.X1 = FromState.AnchorLeft.X;
                    Baseline.Y1 = FromState.AnchorLeft.Y;
                    Baseline.X2 =   ToState.AnchorRight.X;
                    Baseline.Y2 =   ToState.AnchorRight.Y;
                }
            }
            else
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    Baseline.X1 = FromState.AnchorBottomCenter.X;
                    Baseline.Y1 = FromState.AnchorBottomCenter.Y;
                    Baseline.X2 =   ToState.AnchorTopCenter.X;
                    Baseline.Y2 =   ToState.AnchorTopCenter.Y;
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    Baseline.X1 = FromState.AnchorTopCenter.X;
                    Baseline.Y1 = FromState.AnchorTopCenter.Y;
                    Baseline.X2 =   ToState.AnchorBottomCenter.X;
                    Baseline.Y2 =   ToState.AnchorBottomCenter.Y;
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

            double ArrowOffsetLength = 10;
            {   // Place the arrow head

                double OffsetAngle     = 3 * Math.PI / 4;

                var OffsetAngleAbove = rotateTransform.Angle * Math.PI / 180 - OffsetAngle;
                ArrowAbove.X2 = Baseline.X2;
                ArrowAbove.Y2 = Baseline.Y2;
                ArrowAbove.X1 = ArrowAbove.X2 + ArrowOffsetLength * Math.Cos(OffsetAngleAbove);
                ArrowAbove.Y1 = ArrowAbove.Y2 + ArrowOffsetLength * Math.Sin(OffsetAngleAbove);

                var OffsetAngleBelow = rotateTransform.Angle * Math.PI / 180 + OffsetAngle;
                ArrowBelow.X2 = Baseline.X2;
                ArrowBelow.Y2 = Baseline.Y2;
                ArrowBelow.X1 = ArrowBelow.X2 + ArrowOffsetLength * Math.Cos(OffsetAngleBelow);
                ArrowBelow.Y1 = ArrowBelow.Y2 + ArrowOffsetLength * Math.Sin(OffsetAngleBelow);

            }

            {   // Place the label

                Point LabelOrigin;

                var ArrowOffsetX = ArrowOffsetLength * Math.Cos(rotateTransform.Angle * Math.PI / 180);
                var ArrowOffsetY = ArrowOffsetLength * Math.Sin(rotateTransform.Angle * Math.PI / 180);

                var Xmin = Math.Min(Baseline.X1,  (Baseline.X2-ArrowOffsetX));
                var Xdel = Math.Abs(Baseline.X1 - (Baseline.X2-ArrowOffsetX)) / 2;
                var Xabs = Xmin + Xdel;

                var Ymin = Math.Min(Baseline.Y1,  (Baseline.Y2-ArrowOffsetY));
                var Ydel = Math.Abs(Baseline.Y1 - (Baseline.Y2-ArrowOffsetY)) / 2;
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
