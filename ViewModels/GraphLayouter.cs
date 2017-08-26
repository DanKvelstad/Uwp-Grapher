using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Grapher.ViewModels
{

    class GraphLayouter : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private async void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => handler(this, new PropertyChangedEventArgs(info))
                );
            }
        }
        
        public List<UIElement> Children = new List<UIElement>();
        
        private double _Width = 0;
        public double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
        }

        public double _Height = 0;
        public double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
        }

        async public void LayoutIt(Graph graph, List<Point[]> candidates)
        {

            candidates.Sort(
                (a, b) =>
                {

                    int a_cost = 0;
                    foreach (var edge in graph.edges)
                    {
                        var p1 = a[edge.Item1];
                        var p2 = a[edge.Item2];
                        var dx = p1.X - p2.X;
                        var dy = p1.Y - p2.Y;
                        a_cost += (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                    }

                    int b_cost = 0;
                    foreach (var edge in graph.edges)
                    {
                        var p1 = b[edge.Item1];
                        var p2 = b[edge.Item2];
                        var dx = p1.X - p2.X;
                        var dy = p1.Y - p2.Y;
                        b_cost += (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                    }

                    if (a_cost < b_cost)
                    {   // a precedes b in the sort order
                        return -1;
                    }
                    else if (a_cost > b_cost)
                    {   // a follows the b in the sort order
                        return 1;
                    }
                    else
                    {   // a and b occur in the same position in the sort order
                        for (var i = 0; i < a.Length; i++)
                        {
                            var da = (int)Math.Round(Math.Sqrt(a[i].X * a[i].X + a[i].Y * a[i].Y));
                            var db = (int)Math.Round(Math.Sqrt(b[i].X * b[i].X + b[i].Y * b[i].Y));
                            if (da < db)
                            {
                                return -1;
                            }
                            else if (da > db)
                            {
                                return 1;
                            }
                        }
                        return 0;
                    }

                }
            );

            var nodes = new GraphNodeControl[candidates[0].Length];
            var edges = new List<GraphEdgeControl>(graph.edges.Count);

            // ToDo: Dont do this in GUI thread...
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {

                    double node_widest = 0;
                    double node_highest = 0;
                    for (int i = 0; i < candidates[0].Length; i++)
                    {
                        nodes[i] = new GraphNodeControl(graph.nodes[i]);
                        nodes[i].Grid_Point = candidates[0][i];
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

                        Children.Add(node);
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
                            Children.Add(Edge);
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

                    foreach (var node in nodes)
                    {
                        if (Width < node.Center.X + node.Width / 2)
                        {
                            Width = node.Center.X + node.Width / 2;
                        }
                        if (Height < node.Center.Y + node.Height / 2)
                        {
                            Height = node.Center.Y + node.Height / 2;
                        }
                    }

                    OnPropertyChanged("Children");

                }
            );

        }

    }

}
