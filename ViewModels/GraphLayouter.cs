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

        public Node[] NodeViewModels;
        public Edge[] EdgeViewModels;

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

            NodeViewModels  = new Node[graph.nodes.Count];
            var NodeViews   = new GraphNode[graph.nodes.Count];
            
            EdgeViewModels  = new Edge[graph.edges.Count];
            var EdgeViews   = new GraphEdge[graph.edges.Count];
            var WidestEdge  = 50.0;
            var HighestEdge = 50.0;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {

                    var WidestNode  = 0.0;
                    var HighestNode = 0.0;
                    for (int i = 0; i < graph.nodes.Count; i++)
                    {
                        NodeViewModels[i] = new Node();
                        NodeViewModels[i].Label        = graph.nodes[i];
                        NodeViewModels[i].CornerRadius = 10;
                        NodeViewModels[i].PropertyChanged += NodeViewModel_PropertyChanged;
                        NodeViews[i] = new GraphNode(NodeViewModels[i]);
                        WidestNode  = Math.Max(WidestNode,  NodeViewModels[i].MinWidth);
                        HighestNode = Math.Max(HighestNode, NodeViewModels[i].MinHeight);
                    }
                    
                    var Cols = Math.Ceiling(Math.Sqrt(NodeViewModels.Count()));
                    for (int i = 0; i < NodeViewModels.Count(); i++)
                    {
                        NodeViewModels[i].Width  = WidestNode;
                        NodeViewModels[i].Height = HighestNode;
                        NodeViewModels[i].Left   = 2 * (i % Cols) * NodeViewModels[i].Width;
                        NodeViewModels[i].Top    = 2 * (i / Cols) * NodeViewModels[i].Height;
                    }
                    
                    for (int i = 0; i < graph.edges.Count; i++)
                    {

                        var SourceNode = Array.Find(
                            NodeViewModels,
                            n => 0 == n.Label.CompareTo(graph.nodes[graph.edges[i].Item1])
                        );
                        var TargetNode = Array.Find(
                            NodeViewModels,
                            n => 0 == n.Label.CompareTo(graph.nodes[graph.edges[i].Item2])
                        );

                        EdgeViewModels[i] = new Edge(SourceNode, TargetNode, graph.edges[i].Item3);
                        EdgeViews[i] = new GraphEdge(EdgeViewModels[i]);
                        
                        // Todo 10 is a placeholder for arrow length
                        WidestEdge  = Math.Max(WidestEdge,  EdgeViews[i].Label.DesiredSize.Width  + 10);
                        HighestEdge = Math.Max(HighestEdge, EdgeViews[i].Label.DesiredSize.Height + 10);

                    }

                }

            );

            foreach (var candidate in candidates)
            {

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        for (int i = 0; i < candidate.Count(); i++)
                        {
                            NodeViewModels[i].Left = candidate[i].X * NodeViewModels[i].Width  + candidate[i].X * WidestEdge;
                            NodeViewModels[i].Top  = candidate[i].Y * NodeViewModels[i].Height + candidate[i].Y * HighestEdge;
                        }
                    }
                );

                // ToDo: evaluate if this candidate is good
                break;

            }
            
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {   // This must be done at the very end, 
                    // probably even outside this scope.
                    // We dont want to show intermediate graphs.
                    
                    foreach (var node in NodeViews)
                    {
                        Children.Add(node.NodeBorder);
                    }

                    foreach (var edge in EdgeViews)
                    {
                        Children.Add(edge.Baseline);
                        Children.Add(edge.ArrowAlpha);
                        Children.Add(edge.ArrowBravo);
                        Children.Add(edge.Label);
                    }

                }
            );

        }

        private async void NodeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( "Left"  == e.PropertyName || "Top"    == e.PropertyName ||
                "Width" == e.PropertyName || "Height" == e.PropertyName )
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
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
                );
            }
        }
    }

}
