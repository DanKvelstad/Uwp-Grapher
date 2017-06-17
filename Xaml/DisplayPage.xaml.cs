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
            var states = new State[graph.candidates[0].Length];
            var edges = new Edge[graph.edges.Count];

            double longest = 0;
            for (int i=0; i < graph.candidates[0].Length; i++)
            {
                states[i] = new State(graph.nodes[i]);
                states[i].Grid_Point = graph.candidates[0][i];
                if(longest < states[i].Width)
                {
                    longest = states[i].Width;
                }
            }

            foreach (var state in states)
            {

                state.Width     = longest;
                state.Center.X  = /*frame_thickness*/ state.Height + state.Grid_Point.X * state.Width  + state.Width/2  + state.Grid_Point.X * state.Height; //spacing_width;
                state.Center.Y  = /*frame_thickness*/ state.Height + state.Grid_Point.Y * state.Height + state.Height/2 + state.Grid_Point.Y * state.Height; //spacing_height
                canvas.Children.Add(state);
                Canvas.SetLeft(state, state.Center.X-state.Width/2 );
                Canvas.SetTop (state, state.Center.Y-state.Height/2);
                
            }

            for (int i = 0; i < graph.edges.Count; i++)
            {
                var FromState = Array.Find(
                    states,
                    p => 0 == p.StateName.Text.CompareTo(graph.nodes[graph.edges[i].Item1])
                );
                var ToState = Array.Find(
                    states,
                    p => 0 == p.StateName.Text.CompareTo(graph.nodes[graph.edges[i].Item2])
                );
                edges[i] = new Edge(FromState, ToState);
                canvas.Children.Add(edges[i]);
            }

        }

    }
}
