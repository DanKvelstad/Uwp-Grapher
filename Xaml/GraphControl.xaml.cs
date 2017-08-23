using Grapher.ViewModels;
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
        
        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        public event ValueChangedEventHandler CloseGraphControlEvent;
        public Graph graph;

        public GraphControl()
        {
            this.InitializeComponent();
        }

        public void Initiate(Graph graph)
        {
            this.graph = graph;
            ThePivot.SelectedItem = ProcessTabb;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseGraphControlEvent?.Invoke(this, EventArgs.Empty);
        }

        private void ThePivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(e.AddedItems[0] == ProcessTabb)
            {

                GraphGridder gridder = new GraphGridder();
                gridder.graph = graph;
                gridder.available_resolution = (int)Width;

                gridder.PropertyChanged += (object sender2, PropertyChangedEventArgs e2) =>
                {
                    if ("candidates" == e2.PropertyName)
                    {
                        ThePivot.SelectedItem = DisplayTabb;
                    }
                };

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
            else if(e.AddedItems[0] == DisplayTabb)
            {

                var nodes = new GraphNodeControl[graph.candidates[0].Length];
                var edges = new List<GraphEdgeControl>(graph.edges.Count);

                double node_widest = 0;
                double node_highest = 0;
                for (int i = 0; i < graph.candidates[0].Length; i++)
                {
                    nodes[i] = new GraphNodeControl(graph.nodes[i]);
                    nodes[i].Grid_Point = graph.candidates[0][i];
                    if (node_widest < nodes[i].MinWidth)
                    {
                        node_widest = nodes[i].MinWidth;
                    }
                    if (node_highest < nodes[i].MinHeight)
                    {
                        node_highest = nodes[i].MinHeight;
                    }
                }

                foreach (var node in nodes)
                {

                    canvas.Children.Add(node);
                    node.Width = node_widest;
                    node.Height = node_highest;
                    node.Center = new Point(
                        node.Grid_Point.X * node.Width + node.Width / 2 + node.Grid_Point.X * node.Height,
                        node.Grid_Point.Y * node.Height + node.Height / 2 + node.Grid_Point.Y * node.Height
                    );

                }

                double edge_widest = 0;
                double edge_highest = 0;
                foreach (var edge in graph.edges)
                {

                    var FromNode = Array.Find(
                        nodes,
                        p => 0 == p.StateName.Text.CompareTo(graph.nodes[edge.Item1])
                    );
                    var ToNode = Array.Find(
                        nodes,
                        p => 0 == p.StateName.Text.CompareTo(graph.nodes[edge.Item2])
                    );

                    var Edge = edges.Find(
                        p => 0 == p.FromState.CompareTo(FromNode) &&
                             0 == p.ToState.CompareTo(ToNode)
                    );

                    if (null != Edge)
                    {
                        Edge.AppendToEvents(edge.Item3);
                    }
                    else
                    {
                        Edge = new GraphEdgeControl(FromNode, ToNode, edge.Item3);
                        edges.Add(Edge);
                        canvas.Children.Add(Edge);
                    }

                    if (edge_widest < Edge.MinWidth)
                    {
                        edge_widest = Edge.MinWidth;
                    }
                    if (edge_highest < Edge.MinHeight)
                    {
                        edge_highest = Edge.MinHeight;
                    }

                }

                foreach (var node in nodes)
                {
                    node.Center = new Point(
                        node.Grid_Point.X * node.Width + node.Width / 2 + node.Grid_Point.X * edge_widest,
                        node.Grid_Point.Y * node.Height + node.Height / 2 + node.Grid_Point.Y * edge_highest
                    );
                }

                canvas.Width = 0;
                canvas.Height = 0;
                foreach (var node in nodes)
                {
                    if (canvas.Width < node.Center.X + node.Width / 2)
                    {
                        canvas.Width = node.Center.X + node.Width / 2;
                    }
                    if (canvas.Height < node.Center.Y + node.Height / 2)
                    {
                        canvas.Height = node.Center.Y + node.Height / 2;
                    }
                }

            }
        }
    }
}
