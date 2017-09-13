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

            Model.PropertyChanged += UpdateLabel;
            
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
                    Path    = new PropertyPath("SourceX"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.Y1Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("SourceY"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.X2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetX"),
                    Mode    = BindingMode.OneWay
                }
            );
            Baseline.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetY"),
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
                    Path    = new PropertyPath("TargetX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowAlpha.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetY"),
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
                    Path    = new PropertyPath("TargetX"),
                    Mode    = BindingMode.OneWay
                }
            );
            ArrowBravo.SetBinding(
                Line.Y2Property,
                new Binding()
                {
                    Source  = Model,
                    Path    = new PropertyPath("TargetY"),
                    Mode    = BindingMode.OneWay
                }
            );

            Label = new TextBlock();
            Label.FontSize = 13;
            Label.RenderTransform = new RotateTransform();
            {   // Label bindings
                Binding binding  = new Binding();
                binding.Path     = new PropertyPath("Label");
                binding.Source   = Model;
                binding.Mode     = BindingMode.TwoWay;
                BindingOperations.SetBinding(Label, TextBlock.TextProperty, binding);
            }
            
        }

        private void UpdateLabel(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if(
                e.PropertyName.StartsWith("Source") || 
                e.PropertyName.StartsWith("Target") ||
                e.PropertyName == "Label"
            )
            {

                Label.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                var rotateTransform = (RotateTransform)Label.RenderTransform;
                rotateTransform.Angle = Math.Atan2(Baseline.Y2 - Baseline.Y1, Baseline.X2 - Baseline.X1) * 180 / Math.PI;

                Point LabelOrigin;

                var ArrowOffsetX = 10.0;
                var ArrowOffsetY = 10.0;

                var Xmin = Math.Min(Baseline.X1 + ArrowOffsetX, (Baseline.X2 - ArrowOffsetX));
                var Xdel = Math.Abs(Baseline.X1 + ArrowOffsetX - (Baseline.X2 - ArrowOffsetX)) / 2;
                var Xabs = Xmin + Xdel;

                var Ymin = Math.Min(Baseline.Y1 + ArrowOffsetY, (Baseline.Y2 - ArrowOffsetY));
                var Ydel = Math.Abs(Baseline.Y1 + ArrowOffsetY - (Baseline.Y2 - ArrowOffsetY)) / 2;
                var Yabs = Ymin + Ydel;

                var OffsetMagnitude = Label.DesiredSize.Height / 2;
                var OffsetAngle = (rotateTransform.Angle - 90) * Math.PI / 180;
                var OffsetX = OffsetMagnitude * Math.Cos(OffsetAngle);
                var OffsetY = OffsetMagnitude * Math.Sin(OffsetAngle);

                var CenterX = Xabs + OffsetX;
                var CenterY = Yabs + OffsetY;

                LabelOrigin.X = CenterX - Label.DesiredSize.Width / 2;
                LabelOrigin.Y = CenterY - Label.DesiredSize.Height / 2;

                rotateTransform.CenterX = Label.DesiredSize.Width / 2;
                rotateTransform.CenterY = Label.DesiredSize.Height / 2;
                if (89.9 < rotateTransform.Angle || -90 > rotateTransform.Angle)
                {
                    rotateTransform.Angle += 180;
                }

                Canvas.SetLeft(Label, LabelOrigin.X);
                Canvas.SetTop(Label, LabelOrigin.Y);

                //MinWidth  = Label.DesiredSize.Width + Math.Abs((ArrowOffsetX + 5) * 2);
                //MinHeight = Label.DesiredSize.Height + Math.Abs((ArrowOffsetY + 5) * 2);

            }

        }

    }

}
