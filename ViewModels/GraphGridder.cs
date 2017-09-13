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

        private double _Width = 500;
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

        public double _Height = 200;
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
        
        private int _Maximum = 0;
        public  int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                OnPropertyChanged("Maximum");
            }
        }

        private int _Progress = 0;
        public  int Progress
        {
            get
            {
                return _Progress;
            }
            set
            {
                _Progress = value;
                if (0 == _Progress % (int)Math.Max(1, Maximum / Width) || Maximum == _Progress)
                {
                    OnPropertyChanged("Progress");
                }
            }
        }

        public List<Point[]> GridIt(Graph graph)
        {

            var candidates = new List<Point[]>();

            Maximum  = Sequencer.PermutationsCount(graph.nodes.Count + 1);
            Progress = 0;

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
                    candidates.Add(candidate);
                }
                else if (candidate_intersection_count < intersection_count)
                {
                    candidates.Clear();
                    candidates.Add(candidate);
                    intersection_count = candidate_intersection_count;
                }

                Progress++;

            } while (Sequencer.NextPermutation(grid));

            return candidates;

        }

    }

}
