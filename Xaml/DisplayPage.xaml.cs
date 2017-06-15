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

            double longest = 0;
            for (int i=0; i < graph.candidates[0].Length; i++)
            {
                states[i] = new State(graph.nodes[i]);
                if(longest < states[i].Width)
                {
                    longest = states[i].Width;
                }
            }

            for (int i= 0; i<graph.candidates[0].Length; i++)
            {

                states[i].Width = longest;

                canvas.Children.Add(states[i]);
                Canvas.SetLeft(
                    states[i], 
                    /*frame_thickness*/ states[i].Height + graph.candidates[0][i].X * states[i].Width + graph.candidates[0][i].X * states[i].Height //spacing_width
                );
                Canvas.SetTop(
                    states[i], 
                    /*frame_thickness*/ states[i].Height + graph.candidates[0][i].Y * states[i].Height + graph.candidates[0][i].Y * states[i].Height//spacing_height
                );
                
            }
            
        }

    }
}
