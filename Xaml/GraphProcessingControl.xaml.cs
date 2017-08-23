using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher.Xaml
{
    public sealed partial class GraphProcessingControl : UserControl
    {

        //public CancellationTokenSource tokenSource;
        //
        //tokenSource = new CancellationTokenSource();

        public GraphGridder gridder = new GraphGridder();

        public GraphProcessingControl()
        {
            this.InitializeComponent();
        }
        
        public void Process(Graph graph)
        {

            gridder.graph = graph;
            gridder.available_resolution = (int)Width;
            
            Binding progressBinding = new Binding();
            progressBinding.Source = gridder;
            progressBinding.Path = new PropertyPath("progress");
            progressBinding.Mode = BindingMode.TwoWay;
            progressBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            LayoutProgress.SetBinding(ProgressBar.ValueProperty, progressBinding);

            Binding maximumBinding = new Binding();
            maximumBinding.Source = gridder;
            maximumBinding.Path = new PropertyPath("maximum");
            maximumBinding.Mode = BindingMode.TwoWay;
            maximumBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            LayoutProgress.SetBinding(ProgressBar.MaximumProperty, maximumBinding);

            Task.Run(() => gridder.Layout());
            
        }

    }
}
