using Grapher.Models;
using Grapher.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace Grapher.ViewModels
{

    class GraphPresenter : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public Node[]       NodeViewModels;
        public GraphNode[]  NodeViews;

        public EdgeViewModel[]       EdgeViewModels;
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

        public void LayoutIt(GraphModel graph, List<Point[]> candidates)
        {
            
            NodeViewModels  = new Node[graph.nodes.Count];
            NodeViews       = new GraphNode[graph.nodes.Count];
            
            var WidestNode  = 0.0;
            var HighestNode = 0.0;
            for (int i = 0; i < graph.nodes.Count; i++)
            {

                NodeViewModels[i] = new Node(graph.nodes[i]);
                NodeViewModels[i].Label            = graph.nodes[i].Label;
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
            
            EdgeViewModels  = new EdgeViewModel[graph.edges.Count];
            EdgeViews       = new GraphEdge[graph.edges.Count];
            for (int i = 0; i < graph.edges.Count; i++)
            {
                var from_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x.Label == graph.edges[i].Source;
                    }
                );
                var to_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x.Label == graph.edges[i].Target;
                    }
                );
                var SourceNode = Array.Find(
                    NodeViewModels,
                    n => 0 == n.Label.CompareTo(graph.nodes[from_index].Label)
                );
                var TargetNode = Array.Find(
                    NodeViewModels,
                    n => 0 == n.Label.CompareTo(graph.nodes[to_index].Label)
                );
                var Model = graph.edges.Find(
                    (EdgeModel n) =>
                    {
                        return  0 == n.Label.CompareTo(graph.edges[i].Label)    &&
                                0 == n.Source.CompareTo(SourceNode.Label)       &&
                                0 == n.Target.CompareTo(TargetNode.Label)       ;
                    }
                );

                EdgeViewModels[i] = new EdgeViewModel(Model, SourceNode, TargetNode);

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
