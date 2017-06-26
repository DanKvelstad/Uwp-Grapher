using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Grapher
{

    public sealed partial class DisplayPage : Page
    {

        public DisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var graph  = e.Parameter as Graph;
            var nodes = new Node[graph.candidates[0].Length];
            var edges = new List<Edge>(graph.edges.Count);

            double longest = 0;
            for (int i=0; i < graph.candidates[0].Length; i++)
            {
                nodes[i] = new Node(graph.nodes[i]);
                nodes[i].Grid_Point = graph.candidates[0][i];
                if(longest < nodes[i].Width)
                {
                    longest = nodes[i].Width;
                }
            }

            Node RightState    = nodes[0];
            Node BottomState   = nodes[0];
            foreach (var state in nodes)
            {

                state.Width     = longest;
                state.Center    = new Point(
                    state.Grid_Point.X * state.Width  + state.Width/2  + state.Grid_Point.X * state.Height, 
                    state.Grid_Point.Y * state.Height + state.Height/2 + state.Grid_Point.Y * state.Height
                );
                canvas.Children.Add(state);
                Canvas.SetLeft(state, state.Center.X-state.Width/2 );
                Canvas.SetTop (state, state.Center.Y-state.Height/2);
                
                if(RightState.Center.X<state.Center.X)
                {
                    RightState = state;
                }
                if (BottomState.Center.Y < state.Center.Y)
                {
                    BottomState = state;
                }
                
            }

            canvas.Width  = Canvas.GetLeft(RightState) + RightState.Width;
            canvas.Height = Canvas.GetTop(BottomState) + RightState.Height;

            foreach (var edge in graph.edges)
            {

                var FromState = Array.Find(
                    nodes,
                    p => 0 == p.StateName.Text.CompareTo(graph.nodes[edge.Item1])
                );
                var ToState = Array.Find(
                    nodes,
                    p => 0 == p.StateName.Text.CompareTo(graph.nodes[edge.Item2])
                );
                
                var PreExistingEdge = edges.Find(
                    p => 0 == p.FromState.CompareTo(FromState) &&
                         0 == p.ToState.CompareTo(ToState)
                );

                if(null!=PreExistingEdge)
                {
                    PreExistingEdge.AppendToEvents(edge.Item3);
                }
                else
                {
                    edges.Add(new Edge(FromState, ToState, edge.Item3));
                    canvas.Children.Add(edges.Last());
                }

            }

        }

    }
}
