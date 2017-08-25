using Grapher.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace Grapher.ViewModels
{

    public class GraphGridder : INotifyPropertyChanged
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

        private int _available_resolution;
        public  int available_resolution
        {
            get
            {
                return _available_resolution;
            }
            set
            {
                _available_resolution = value;
            }
        }

        private int _maximum;
        public  int maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
                OnPropertyChanged("maximum");
            }
        }

        private int _progress;
        public  int progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                if (0 == _progress % 1000 /*(maximum / available_resolution)*/ || maximum == _progress)
                {
                    OnPropertyChanged("progress");
                }
            }
        }

        public enum States
        {
            Starting,
            Gridding,
            Layouting,
            Displaying
        }
        private States _ActiveState = States.Starting;
        public States ActiveState
        {
            get
            {
                return _ActiveState;
            }
            set
            {
                _ActiveState = value;
                OnPropertyChanged("ActiveState");
            }
        }

        public List<Point[]> candidates
        {
            get
            {
                return graph.candidates;
            }
            set
            {

                // move this into the layouting stage, it takes 2sec for large
                value.Sort(
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

                graph.candidates = value;

                if (0 == _progress % 1000/*(maximum / available_resolution)*/ || maximum == _progress)
                {
                    OnPropertyChanged("candidates");
                }

            }
        }

        public Graph graph;
        
        public void Layout()
        {

            ActiveState = States.Gridding;
            var new_candidates = new List<Point[]>();
            
            maximum = Sequencer.PermutationsCount(graph.nodes.Count + 1);
            progress = 0;

            int grid_dimensions = (int)Math.Ceiling(Math.Sqrt((double)graph.nodes.Count + 1));

            int[] grid = new int[grid_dimensions * grid_dimensions];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = i;
            }

            var intersection_count = int.MaxValue;
            var edges_array = graph.edges.ToArray();

            do
            {

                var candidate = new Point[graph.nodes.Count];
                for (int i = 0; i < candidate.Length; i++)
                {
                    candidate[i].X = grid[i] % grid_dimensions;
                    candidate[i].Y = grid[i] / grid_dimensions;
                }

                int candidate_intersection_count = 0;
                for (int i = 0; i < edges_array.Length; i++)
                {
                    for (int j = i + 1; j < edges_array.Length; j++)
                    {
                        if (
                            LinearAlgebra.Intersection(
                                candidate[edges_array[i].Item1],
                                candidate[edges_array[i].Item2],
                                candidate[edges_array[j].Item1],
                                candidate[edges_array[j].Item2]
                            )
                        )
                        {
                            candidate_intersection_count++;
                        }
                    }
                }

                if (candidate_intersection_count == intersection_count)
                {
                    new_candidates.Add(candidate);
                }
                else if (candidate_intersection_count < intersection_count)
                {
                    new_candidates.Clear();
                    new_candidates.Add(candidate);
                    intersection_count = candidate_intersection_count;
                }

                progress++;

            } while (Sequencer.NextPermutation(grid));
            
            candidates = new_candidates;
            ActiveState = States.Layouting;

        }

    }

}
