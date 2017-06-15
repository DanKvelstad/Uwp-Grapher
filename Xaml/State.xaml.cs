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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

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

            //Width =     StateName.DesiredSize.Width         +
            //            StateBorder.Margin.Left             +
            //            StateBorder.Margin.Right            +
            //            StateBorder.Padding.Left            +
            //            StateBorder.Padding.Right           +
            //            StateBorder.BorderThickness.Left    +
            //            StateBorder.BorderThickness.Right   ;
            //Height =    StateName.DesiredSize.Height        +
            //            StateBorder.Margin.Left             +
            //            StateBorder.Margin.Right            +
            //            StateBorder.Padding.Left            +
            //            StateBorder.Padding.Right           +
            //            StateBorder.BorderThickness.Left    +
            //            StateBorder.BorderThickness.Right   ;

        }

    }
}
