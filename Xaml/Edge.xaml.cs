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

        public Edge(State from, State to)
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

        }

        private State FromState;
        private State ToState;

    }

}
