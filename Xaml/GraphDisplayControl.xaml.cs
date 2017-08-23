using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher.Xaml
{

    public sealed partial class GraphDisplayControl : UserControl
    {
        public GraphDisplayControl()
        {
            this.InitializeComponent();
        }

        public void Adopt(Graph graph)
        {

            var nodes = new GraphNodeControl[graph.candidates[0].Length];
            var edges = new List<GraphEdgeControl>(graph.edges.Count);

            double node_widest  = 0;
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
                node.Width  = node_widest;
                node.Height = node_highest;
                node.Center = new Point(
                    node.Grid_Point.X * node.Width + node.Width / 2 + node.Grid_Point.X * node.Height,
                    node.Grid_Point.Y * node.Height + node.Height / 2 + node.Grid_Point.Y * node.Height
                );
                
            }

            double edge_widest  = 0;
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

                if(edge_widest < Edge.MinWidth)
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
                    node.Grid_Point.X * node.Width  + node.Width / 2  + node.Grid_Point.X * edge_widest,
                    node.Grid_Point.Y * node.Height + node.Height / 2 + node.Grid_Point.Y * edge_highest
                );
            }

            Width = 0;
            Height = 0;
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

        }

    }

}
