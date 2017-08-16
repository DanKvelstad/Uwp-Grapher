using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Grapher.Xaml
{
    public sealed partial class GraphControl : UserControl
    {

        GraphProcessingControl Processor;
        GraphDisplayControl Displayer;

        public GraphControl()
        {
            this.InitializeComponent();
        }

        public async void Initiate(Graph graph)
        {
            
            Processor = new GraphProcessingControl();
            WindowPanel.Children.Add(Processor);
            Processor.SetValue(RelativePanel.BelowProperty, Name);

            await Task.Run(
                () => Processor.Process(graph)
            ).ContinueWith(
                (antecedent) => 
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        WindowPanel.Children.Remove(Processor);
                        Displayer = new GraphDisplayControl();
                        WindowPanel.Children.Add(Displayer);
                        Processor.SetValue(RelativePanel.BelowProperty, Name);
                        Displayer.Adopt(graph);
                    }
                )
            );
            
        }

    }
}
