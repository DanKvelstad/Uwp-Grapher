using Grapher.Serialization;
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

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        
        public ConnectPage()
        {
            this.InitializeComponent();
        }
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);

            ContentDialog Dialog = new ContentDialog
            {
                Title = "Error opening file",
                Content = "Verify that it is a valid file for this program.",
                CloseButtonText = "Ok"
            };

            var File = e.Parameter as IStorageFile;
            if (null == File)
            {
                var result = await Dialog.ShowAsync();
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {

                var graph = await Serialization.Serializor.Deserialize(File);
                if (null == graph)
                {
                    var result = await Dialog.ShowAsync();
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {

                    tokenSource = new CancellationTokenSource();

                    LayoutProgress.Maximum = graph.PermutationsCount();
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    
                    await Task.Run(
                        () => graph.Layout(
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
                        )
                    );
                    LayoutProgress.Value = LayoutProgress.Maximum;
                    stopWatch.Stop();

                    //var File = await ApplicationData.Current.LocalFolder.CreateFileAsync("output.xml");
                    //Serialization.Serializor.SerializeAsXml(graph, await File.OpenStreamForWriteAsync());

                    if(!tokenSource.IsCancellationRequested)
                    {
                        this.Frame.Navigate(typeof(DisplayPage), graph);
                    }

                }

            }
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            tokenSource.Cancel();
        }

    }

}
