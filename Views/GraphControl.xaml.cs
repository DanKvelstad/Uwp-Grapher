using Grapher.Models;
using Grapher.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Shapes;

namespace Grapher.Xaml
{
    
    public sealed partial class GraphControl : UserControl
    {
        
        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        public event ValueChangedEventHandler CloseGraphControlEvent;
        GraphProcessor processor = new GraphProcessor();

        public GraphControl()
        {
            this.InitializeComponent();
        }

        public void Initialize(Graph graph)
        {
            processor.ProcessAsync(graph);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseGraphControlEvent?.Invoke(this, EventArgs.Empty);
        }
        
    }

    public class ConverterVisibileIfStateIsProcessing : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (GraphProcessor.States.Gridding == (GraphProcessor.States)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterVisibileIfStateIsLayouting : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (GraphProcessor.States.Layouting == (GraphProcessor.States)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterVisibileIfStateIsDisplaying : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (GraphProcessor.States.Displaying == (GraphProcessor.States)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

}
