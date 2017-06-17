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
    public sealed partial class State : UserControl
    {

        public State(String Name)
        {

            this.InitializeComponent();

            StateName.Text = Name;
            StateName.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            StateBorder.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Width   = StateBorder.DesiredSize.Width;
            Height  = StateBorder.DesiredSize.Height;
            
        }

        public Point Center;
        
        public Point Grid_Point;

    }
}
