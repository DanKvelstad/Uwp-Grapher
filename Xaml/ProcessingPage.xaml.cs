using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher
{

    public sealed partial class ConnectPage : Page
    {

        private Graph graph;

        public ConnectPage()
        {
            this.InitializeComponent();
        }

        private void DoNotConnect_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DisplayPage));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            // todo
            // var storageFile = e.Parameter as StorageFile;
            // var fileContent = await FileIO.ReadTextAsync(storageFile);

            base.OnNavigatedTo(e);
            
            graph = new Graph();
            graph.EmplaceNode("s0");
            graph.EmplaceNode("s1");
            graph.EmplaceNode("s2");
            graph.EmplaceNode("s3");
            graph.EmplaceNode("s4");

            graph.EmplaceEdge("s1", "s2", "e0");
            graph.EmplaceEdge("s2", "s3", "e0");
            graph.EmplaceEdge("s3", "s4", "e0");
            graph.EmplaceEdge("s4", "s1", "e0");

            graph.EmplaceEdge("s0", "s1", "e1");
            graph.EmplaceEdge("s0", "s2", "e2");
            graph.EmplaceEdge("s0", "s3", "e3");
            graph.EmplaceEdge("s0", "s4", "e4");

            LayoutProgress.Maximum = graph.PermutationsCount();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task<bool>.Run(
                () => graph.Layout(
                    async (current) =>
                    {
                        if(TimeSpan.FromMilliseconds(500) < stopWatch.Elapsed)
                        {
                            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                                CoreDispatcherPriority.Normal,
                                () => LayoutProgress.Value = current
                            );
                            stopWatch.Reset();
                        }
                    }
                )
            );
            LayoutProgress.Value = LayoutProgress.Maximum;
            stopWatch.Stop();
            
            ContinueButton.IsEnabled = true;

        }

        private async System.Threading.Tasks.Task<bool> ProcessFile()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            return true;
        }

    }

}
