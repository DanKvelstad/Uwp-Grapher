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

            double longest = 0;
            for (int i = 0; i < graph.candidates[0].Length; i++)
            {
                nodes[i] = new GraphNodeControl(graph.nodes[i]);
                nodes[i].Grid_Point = graph.candidates[0][i];
                if (longest < nodes[i].Width)
                {
                    longest = nodes[i].Width;
                }
            }

            GraphNodeControl RightState = nodes[0];
            GraphNodeControl BottomState = nodes[0];
            foreach (var node in nodes)
            {

                node.Width = longest;
                node.Center = new Point(
                    node.Grid_Point.X * node.Width + node.Width / 2 + node.Grid_Point.X * node.Height,
                    node.Grid_Point.Y * node.Height + node.Height / 2 + node.Grid_Point.Y * node.Height
                );
                canvas.Children.Add(node);
                Canvas.SetLeft(node, node.Center.X - node.Width / 2);
                Canvas.SetTop(node, node.Center.Y - node.Height / 2);

                if (RightState.Center.X < node.Center.X)
                {
                    RightState = node;
                }
                if (BottomState.Center.Y < node.Center.Y)
                {
                    BottomState = node;
                }

            }

            Width = Canvas.GetLeft(RightState) + RightState.Width;
            Height = Canvas.GetTop(BottomState) + RightState.Height;

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

                var PreExistingEdge = edges.Find(
                    p => 0 == p.FromState.CompareTo(FromNode) &&
                         0 == p.ToState.CompareTo(ToNode)
                );

                if (null != PreExistingEdge)
                {
                    PreExistingEdge.AppendToEvents(edge.Item3);
                }
                else
                {
                    edges.Add(new GraphEdgeControl(FromNode, ToNode, edge.Item3));
                    canvas.Children.Add(edges.Last());
                }

            }

        }

    }

}
