using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Grapher.Views
{
    
    public sealed class GraphEdge
    {

        public Line         Baseline;
        public Line         ArrowAlpha;
        public Line         ArrowBravo;
        public TextBlock    Label;

        public GraphEdge(Edge Model)
        {
            
            Baseline = new Line();
            Baseline.StrokeThickness    = 2;
            Baseline.Stroke             = new SolidColorBrush(Colors.Black);
            Baseline.StrokeStartLineCap = PenLineCap.Round;
            Baseline.StrokeEndLineCap   = PenLineCap.Triangle;
            Baseline.SetBinding(
                Line.X1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("SourceActualX"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.Y1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("SourceActualY"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.X2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualX"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualY"),
                    Mode    = BindingMode.OneWay
                }
            );
            
            ArrowAlpha = new Line();
            ArrowAlpha.StrokeThickness      = 2;
            ArrowAlpha.Stroke               = new SolidColorBrush(Colors.Black);
            ArrowAlpha.StrokeStartLineCap   = PenLineCap.Round;
            ArrowAlpha.StrokeEndLineCap     = PenLineCap.Triangle;
            ArrowAlpha.SetBinding(
                Line.X1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("ArrowAlphaX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowAlpha.SetBinding(
                Line.Y1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("ArrowAlphaY"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowAlpha.SetBinding(
                Line.X2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowAlpha.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualY"),
                    Mode    = BindingMode.OneWay
                }
            );

            ArrowBravo = new Line();
            ArrowBravo.StrokeThickness      = 2;
            ArrowBravo.Stroke               = new SolidColorBrush(Colors.Black);
            ArrowBravo.StrokeStartLineCap   = PenLineCap.Round;
            ArrowBravo.StrokeEndLineCap     = PenLineCap.Triangle;
            ArrowBravo.SetBinding(
                Line.X1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("ArrowBravoX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowBravo.SetBinding(
                Line.Y1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("ArrowBravoY"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowBravo.SetBinding(
                Line.X2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowBravo.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetActualY"),
                    Mode    = BindingMode.OneWay
                }
            );

            Label = new TextBlock();
            Label.FontSize = 13;
            Label.RenderTransform = new RotateTransform();
            Model.PropertyChanged += Model_PropertyChanged;
            Label.SetBinding(
                 TextBlock.TextProperty,
                 new Binding()
                 {
                     Source = Model,
                     Path   = new PropertyPath("Label"),
                     Mode   = BindingMode.OneWay,
                 }
             );
            Label.SetBinding(
                Canvas.LeftProperty,
                new Binding()
                {
                    Source    = Model,
                    Path      = new PropertyPath("LabelLeft"),
                    Mode      = BindingMode.OneWay
                }
            );
            Label.SetBinding(
                Canvas.TopProperty,
                new Binding()
                {
                    Source = Model,
                    Path   = new PropertyPath("LabelTop"),
                    Mode   = BindingMode.OneWay
                }
            );
            BindingOperations.SetBinding(
                Label.RenderTransform,
                RotateTransform.AngleProperty,
                new Binding()
                {
                    Source      = Model,
                    Path        = new PropertyPath("Angle"),
                    Mode        = BindingMode.OneWay,
                    Converter   = new ConverterTextAngle()
                }
            );
            BindingOperations.SetBinding(
                Label.RenderTransform,
                RotateTransform.CenterXProperty,
                new Binding()
                {
                    Source      = Model,
                    Path        = new PropertyPath("LabelWidth"),
                    Mode        = BindingMode.OneWay,
                    Converter   = new ConverterHalf()
                }
            );
            BindingOperations.SetBinding(
                Label.RenderTransform,
                RotateTransform.CenterYProperty,
                new Binding()
                {
                    Source      = Model,
                    Path        = new PropertyPath("LabelHeight"),
                    Mode        = BindingMode.OneWay,
                    Converter   = new ConverterHalf()
                }
            );
            Model_PropertyChanged(
                Model, 
                new System.ComponentModel.PropertyChangedEventArgs("Label")
            );

        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if("Label"==e.PropertyName)
            {
                Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                var Model = (Edge)sender;
                Model.LabelWidth  = Label.DesiredSize.Width;
                Model.LabelHeight = Label.DesiredSize.Height;
            }
        }
        
        public class ConverterHalf : IValueConverter
        {
            
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                return ((double)value) / 2;
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                return ((double)value) * 2;
            }

        }

        public class ConverterTextAngle : IValueConverter
        {

            public object Convert(object value, Type targetType, object parameter, string language)
            {

                var Angle = (double)value * 180 / Math.PI;

                if (359.99 < Angle && 0.01 > Angle)
                {
                    Angle = 0;
                }
                else if (89.99 < Angle && 90.01 > Angle)
                {
                    Angle = 270;
                }
                else if (179.99 < Angle && 180.01 > Angle)
                {
                    Angle = 0;
                }
                else if (269.99 < Angle && 270.01 > Angle)
                {
                    Angle = 270;
                }
                else if ((90 < Angle && 180 > Angle))
                {
                    Angle += 180;
                }
                else if ((180 < Angle && 270 > Angle))
                {
                    Angle -= 180;
                }

                return Angle;
                
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException();
            }

        }

    }

}
