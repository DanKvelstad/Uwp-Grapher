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
                TheLine.X1 = FromState.Center.X + FromState.Width/2;
                TheLine.X2 =   ToState.Center.X -   ToState.Width/2;
            }
            else if(FromState.Center.X > ToState.Center.X)
            {
                TheLine.X1 = FromState.Center.X - FromState.Width/2;
                TheLine.X2 =   ToState.Center.X +   ToState.Width/2;
            }
            else
            {
                TheLine.X1 = FromState.Center.X;
                TheLine.X2 =   ToState.Center.X;
            }

            if (FromState.Center.Y < ToState.Center.Y)
            {
                TheLine.Y1 = FromState.Center.Y + FromState.Height/2;
                TheLine.Y2 =   ToState.Center.Y -   ToState.Height/2;
            }
            else if (FromState.Center.Y > ToState.Center.Y)
            {
                TheLine.Y1 = FromState.Center.Y - FromState.Height/2;
                TheLine.Y2 =   ToState.Center.Y +   ToState.Height/2;
            }
            else
            {
                TheLine.Y1 = FromState.Center.Y;
                TheLine.Y2 =   ToState.Center.Y;
            }

        }

        private State FromState;
        private State ToState;

    }

}
