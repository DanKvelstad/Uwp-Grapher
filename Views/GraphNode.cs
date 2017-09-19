using System;
using Grapher.Models;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Grapher.Views
{

    public sealed class GraphNode
    {

        public TextBlock    Label;
        public Border       NodeBorder;

        public GraphNode(Node Model)
        {

            Label = new TextBlock();
            Label.FontSize = 13;
            Label.TextAlignment = TextAlignment.Center;
            Label.HorizontalAlignment = HorizontalAlignment.Center;
            Label.VerticalAlignment = VerticalAlignment.Center;
            Label.Margin = new Thickness(10);
            Model.PropertyChanged += Model_PropertyChanged;

            NodeBorder = new Border();
            NodeBorder.Child = Label;
            NodeBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            NodeBorder.BorderThickness = new Thickness(2);
            NodeBorder.SetBinding(
                Canvas.LeftProperty,
                new Binding()
                {
                    Path = new PropertyPath("Left"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                Canvas.TopProperty,
                new Binding()
                {
                    Path = new PropertyPath("Top"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                FrameworkElement.MinWidthProperty,
                new Binding()
                {
                    Path = new PropertyPath("MinWidth"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                FrameworkElement.WidthProperty,
                new Binding()
                {
                    Path = new PropertyPath("Width"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                FrameworkElement.MinHeightProperty,
                new Binding()
                {
                    Path = new PropertyPath("MinHeight"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                FrameworkElement.HeightProperty,
                new Binding()
                {
                    Path = new PropertyPath("Height"),
                    Source = Model,
                    Mode = BindingMode.TwoWay
                }
            );
            NodeBorder.SetBinding(
                Border.CornerRadiusProperty,
                new Binding()
                {
                    Converter   = new ConvertDoubleToCornerRadius(),
                    Path        = new PropertyPath("CornerRadius"),
                    Source      = Model,
                    Mode        = BindingMode.OneWay
                }
            );

            Model_PropertyChanged(
                Model,
                new System.ComponentModel.PropertyChangedEventArgs("Label")
            );

        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var SenderNode = sender as Node;
            if( null != SenderNode )
            {
                if ("Label"==e.PropertyName)
                {
                    Label.Text = SenderNode.Label;
                    Label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    NodeBorder.MinWidth  = NodeBorder.BorderThickness.Left + Label.DesiredSize.Width  + NodeBorder.BorderThickness.Right;
                    NodeBorder.MinHeight = NodeBorder.BorderThickness.Top  + Label.DesiredSize.Height + NodeBorder.BorderThickness.Bottom;
                }
            }
        }

    }

    class ConvertDoubleToCornerRadius : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new CornerRadius((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }

}
