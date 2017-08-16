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

        public CancellationTokenSource tokenSource = new CancellationTokenSource();

        public GraphProcessingControl()
        {
            this.InitializeComponent();
        }
        
        public async Task Process(Graph graph)
        {
            
            tokenSource = new CancellationTokenSource();

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => LayoutProgress.Maximum = graph.PermutationsCount()
            );
            
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            graph.Layout(
                tokenSource.Token,
                async (current) =>
                {
                    if (TimeSpan.FromMilliseconds(500) < stopWatch.Elapsed)
                    {
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () => LayoutProgress.Value = current
                        );
                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }
            );
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => LayoutProgress.Value = LayoutProgress.Maximum
            );
            stopWatch.Stop();
            
        }

    }
}
