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

            TheLine.X1  = FromState.Center.X;
            TheLine.Y1  = FromState.Center.Y;
            TheLine.X2  =   ToState.Center.X;
            TheLine.Y2  =   ToState.Center.Y;

        }

        private State FromState;
        private State ToState;

    }

}
