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
                    TheLine.X1 = FromState.Center.X + FromState.Width /2 - FromState.StateBorder.CornerRadius.BottomRight*Math.Cos(Math.PI/4);
                    TheLine.Y1 = FromState.Center.Y + FromState.Height/2 - FromState.StateBorder.CornerRadius.BottomRight*Math.Sin(Math.PI/4);
                    TheLine.X2 =   ToState.Center.X -   ToState.Width /2 +   ToState.StateBorder.CornerRadius.TopLeft    *Math.Cos(Math.PI/4);
                    TheLine.Y2 =   ToState.Center.Y -   ToState.Height/2 +   ToState.StateBorder.CornerRadius.TopLeft    *Math.Sin(Math.PI/4);
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    TheLine.X1 = FromState.Center.X + FromState.Width /2 - FromState.StateBorder.CornerRadius.TopRight  *Math.Cos(Math.PI/4);
                    TheLine.Y1 = FromState.Center.Y - FromState.Height/2 + FromState.StateBorder.CornerRadius.TopRight  *Math.Sin(Math.PI/4);
                    TheLine.X2 =   ToState.Center.X -   ToState.Width /2 +   ToState.StateBorder.CornerRadius.BottomLeft*Math.Cos(Math.PI/4);
                    TheLine.Y2 =   ToState.Center.Y +   ToState.Height/2 -   ToState.StateBorder.CornerRadius.BottomLeft*Math.Sin(Math.PI/4);
                }
                else
                {
                    TheLine.X1 = FromState.Center.X + FromState.Width/2;
                    TheLine.Y1 = FromState.Center.Y;
                    TheLine.X2 =   ToState.Center.X -   ToState.Width/2;
                    TheLine.Y2 =   ToState.Center.Y;
                }
            }
            else if(FromState.Center.X > ToState.Center.X)
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    TheLine.X1 = FromState.Center.X - FromState.Width /2 + FromState.StateBorder.CornerRadius.BottomLeft*Math.Cos(Math.PI/4);
                    TheLine.Y1 = FromState.Center.Y + FromState.Height/2 - FromState.StateBorder.CornerRadius.BottomLeft*Math.Sin(Math.PI/4);
                    TheLine.X2 =   ToState.Center.X +   ToState.Width /2 -   ToState.StateBorder.CornerRadius.TopRight  *Math.Cos(Math.PI/4);
                    TheLine.Y2 =   ToState.Center.Y -   ToState.Height/2 +   ToState.StateBorder.CornerRadius.TopRight  *Math.Sin(Math.PI/4);
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    TheLine.X1 = FromState.Center.X - FromState.Width /2 + FromState.StateBorder.CornerRadius.TopLeft    *Math.Cos(Math.PI/4);
                    TheLine.Y1 = FromState.Center.Y - FromState.Height/2 + FromState.StateBorder.CornerRadius.TopLeft    *Math.Sin(Math.PI/4);
                    TheLine.X2 =   ToState.Center.X + ToState.Width   /2 -   ToState.StateBorder.CornerRadius.BottomRight*Math.Cos(Math.PI/4);
                    TheLine.Y2 =   ToState.Center.Y +   ToState.Height/2 -   ToState.StateBorder.CornerRadius.BottomRight*Math.Sin(Math.PI/4);
                }
                else
                {
                    TheLine.X1 = FromState.Center.X - FromState.Width / 2;
                    TheLine.Y1 = FromState.Center.Y;
                    TheLine.X2 =   ToState.Center.X + ToState.Width / 2;
                    TheLine.Y2 =   ToState.Center.Y;
                }
            }
            else
            {
                if (FromState.Center.Y < ToState.Center.Y)
                {
                    TheLine.X1 = FromState.Center.X;
                    TheLine.X2 =   ToState.Center.X;
                    TheLine.Y1 = FromState.Center.Y + FromState.Height/2;
                    TheLine.Y2 =   ToState.Center.Y -   ToState.Height/2;
                }
                else if (FromState.Center.Y > ToState.Center.Y)
                {
                    TheLine.X1 = FromState.Center.X;
                    TheLine.X2 =   ToState.Center.X;
                    TheLine.Y1 = FromState.Center.Y - FromState.Height/2;
                    TheLine.Y2 =   ToState.Center.Y +   ToState.Height/2;
                }
                else
                {   // this is actually an error state, they overlap
                    TheLine.X1 = FromState.Center.X;
                    TheLine.X2 =   ToState.Center.X;
                    TheLine.Y1 = FromState.Center.Y;
                    TheLine.Y2 =   ToState.Center.Y;
                }
            }

            Label.Text = label;
            Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            var rotateTransform = (RotateTransform)Label.RenderTransform;
            rotateTransform.Angle   = Math.Atan2(TheLine.Y2 - TheLine.Y1, TheLine.X2 - TheLine.X1) * 180 / Math.PI;
            
            Point LabelOrigin;
            if (90.1>rotateTransform.Angle && 89.9<rotateTransform.Angle)
            {
                LabelOrigin.X            = Math.Min(TheLine.X1, TheLine.X2) + Math.Abs(TheLine.X1 - TheLine.X2) / 2;
                LabelOrigin.Y            = Math.Min(TheLine.Y1, TheLine.Y2) + Math.Abs(TheLine.Y1 - TheLine.Y2) / 2;
                LabelOrigin.Y           -= Label.DesiredSize.Height / 2;
                rotateTransform.CenterX  = Label.DesiredSize.Width  / 2;
                rotateTransform.CenterY  = Label.DesiredSize.Height / 2;
                rotateTransform.Angle    = -90;
            }
            else if(-90.1 < rotateTransform.Angle && -89.9 > rotateTransform.Angle)
            {
                LabelOrigin.X            = Math.Min(TheLine.X1, TheLine.X2) + Math.Abs(TheLine.X1 - TheLine.X2) / 2;
                LabelOrigin.Y            = Math.Min(TheLine.Y1, TheLine.Y2) + Math.Abs(TheLine.Y1 - TheLine.Y2) / 2;
                LabelOrigin.Y           -= Label.DesiredSize.Height / 2;
                rotateTransform.CenterX  = Label.DesiredSize.Width  / 2;
                rotateTransform.CenterY  = Label.DesiredSize.Height / 2;
                rotateTransform.Angle    = -90;
            }
            else
            {

                var Xmin = Math.Min(TheLine.X1, TheLine.X2);
                var Xdel = Math.Abs(TheLine.X1 - TheLine.X2) / 2;
                var Xabs = Xmin + Xdel;

                var Ymin = Math.Min(TheLine.Y1, TheLine.Y2);
                var Ydel = Math.Abs(TheLine.Y1 - TheLine.Y2) / 2;
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
                if (90 < rotateTransform.Angle || -90 > rotateTransform.Angle)
                {
                    rotateTransform.Angle += 180;
                }

            }
            Canvas.SetTop (Label, LabelOrigin.Y);
            Canvas.SetLeft(Label, LabelOrigin.X);

        }

        private State FromState;
        private State ToState;

    }

}
