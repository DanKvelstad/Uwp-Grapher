using Grapher.Models;
using Grapher.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Grapher.ViewModels
{

    class GraphLayouter : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public Node[]       NodeViewModels;
        public GraphNode[]  NodeViews;

        public Edge[]       EdgeViewModels;
        public GraphEdge[]  EdgeViews;

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

        public ObservableCollection<UIElement> Children = new ObservableCollection<UIElement>();

        public async Task LayoutIt(Graph graph, List<Point[]> candidates)
        {

            await Task.Run(
                new Action(() =>
                {
                    candidates.Sort(
                        (a, b) =>
                {

                    int a_cost = 0;
                    foreach (var edge in graph.edges)
                    {
                        var from_index = graph.nodes.FindIndex(
                            (x) =>
                            {
                                return x == edge.Item1;
                            }
                        );
                        var to_index = graph.nodes.FindIndex(
                            (x) =>
                            {
                                return x == edge.Item2;
                            }
                        );
                        var p1 = a[from_index];
                        var p2 = a[to_index];
                        var dx = p1.X - p2.X;
                        var dy = p1.Y - p2.Y;
                        a_cost += (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                    }

                    int b_cost = 0;
                    foreach (var edge in graph.edges)
                    {
                        var from_index = graph.nodes.FindIndex(
                            (x) =>
                            {
                                return x == edge.Item1;
                            }
                        );
                        var to_index = graph.nodes.FindIndex(
                            (x) =>
                            {
                                return x == edge.Item2;
                            }
                        );
                        var p1 = b[from_index];
                        var p2 = b[to_index];
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
                })
            );

            NodeViewModels  = new Node[graph.nodes.Count];
            NodeViews       = new GraphNode[graph.nodes.Count];
            
            var WidestNode  = 0.0;
            var HighestNode = 0.0;
            for (int i = 0; i < graph.nodes.Count; i++)
            {

                NodeViewModels[i] = new Node();
                NodeViewModels[i].Label            = graph.nodes[i];
                NodeViewModels[i].CornerRadius     = 10;
                NodeViewModels[i].PropertyChanged += NodeViewModel_PropertyChanged;

                NodeViews[i] = new GraphNode(NodeViewModels[i]);
                Children.Add(NodeViews[i].NodeBorder);

                WidestNode  = Math.Max(WidestNode,  NodeViewModels[i].MinWidth);
                HighestNode = Math.Max(HighestNode, NodeViewModels[i].MinHeight);
                
            }

            for (int i = 0; i < NodeViewModels.Count(); i++)
            {
                NodeViewModels[i].Width  = WidestNode;
                NodeViewModels[i].Height = HighestNode;
            }
            
            EdgeViewModels  = new Edge[graph.edges.Count];
            EdgeViews       = new GraphEdge[graph.edges.Count];
            for (int i = 0; i < graph.edges.Count; i++)
            {
                var from_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x == graph.edges[i].Item1;
                    }
                );
                var to_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x == graph.edges[i].Item2;
                    }
                );
                var SourceNode = Array.Find(
                    NodeViewModels,
                    n => 0 == n.Label.CompareTo(graph.nodes[from_index])
                );
                var TargetNode = Array.Find(
                    NodeViewModels,
                    n => 0 == n.Label.CompareTo(graph.nodes[to_index])
                );

                EdgeViewModels[i] = new Edge(SourceNode, TargetNode, graph.edges[i].Item3);

                EdgeViews[i] = new GraphEdge(EdgeViewModels[i]);
                Children.Add(EdgeViews[i].Baseline);
                Children.Add(EdgeViews[i].ArrowAlpha);
                Children.Add(EdgeViews[i].ArrowBravo);
                Children.Add(EdgeViews[i].Label);

            }

            foreach (var candidate in candidates)
            {

                for (int i = 0; i < candidate.Count(); i++)
                {
                    NodeViewModels[i].Left = candidate[i].X * (2 * NodeViewModels[i].Width );
                    NodeViewModels[i].Top  = candidate[i].Y * (2 * NodeViewModels[i].Height);
                }

                var WidestEdge  = 0.0;
                var HighestEdge = 0.0;
                foreach (var Edge in EdgeViewModels)
                {
                    WidestEdge  = Math.Max(WidestEdge,  Edge.MinWidth);
                    HighestEdge = Math.Max(HighestEdge, Edge.MinHeight);
                }
                
                for (int i = 0; i < candidate.Count(); i++)
                {
                    NodeViewModels[i].Left = candidate[i].X * (NodeViewModels[i].Width  + WidestEdge);
                    NodeViewModels[i].Top  = candidate[i].Y * (NodeViewModels[i].Height + HighestEdge);
                }
                
                // ToDo: evaluate if this candidate is good
                break;

            }
            
        }

        private void NodeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( "Left"  == e.PropertyName || "Top"    == e.PropertyName ||
                "Width" == e.PropertyName || "Height" == e.PropertyName )
            {
                var WidthOfCanvas  = 0.0;
                var HeightOfCanvas = 0.0;
                foreach (var node in NodeViewModels)
                {
                    WidthOfCanvas  = Math.Max(WidthOfCanvas,  node.Left + node.Width);
                    HeightOfCanvas = Math.Max(HeightOfCanvas, node.Top  + node.Height);
                }
                Width  = WidthOfCanvas;
                Height = HeightOfCanvas;
            }
        }
    }

}
