using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        public event ValueChangedEventHandler CloseGraphControlEvent;
        
        public GraphControl()
        {
            this.InitializeComponent();
        }

        public void Initiate(Graph graph)
        {
            
            Processor = new GraphProcessingControl();
            WindowPanel.Children.Add(Processor);
            Processor.gridder.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if("candidates"==e.PropertyName)
                {
                    WindowPanel.Children.Remove(Processor);
                    Displayer = new GraphDisplayControl();
                    WindowPanel.Children.Add(Displayer);
                    Displayer.Adopt(graph);
                }
            };
            Processor.Process(graph);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseGraphControlEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
