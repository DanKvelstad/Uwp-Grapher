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
using Windows.UI.Xaml;

namespace Grapher.ViewModels
{

    public class GraphGridder : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
            }
        }
        
        private int _ProgressMaximum;
        public  int ProgressMaximum
        {
            get
            {
                return _ProgressMaximum;
            }
            private set
            {
                if (value > Width)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _ProgressMaximum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressMaximum"));
            }
        }

        private int _ProgressCurrent;
        public  int ProgressCurrent
        {
            get
            {
                return _ProgressCurrent;
            }
            private set
            {
                if(value>ProgressMaximum)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _ProgressCurrent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressCurrent"));
            }
        }

        public async Task<List<Point[]>> GridItAsync(Graph graph)
        {

            var candidates = new List<Point[]>();

            int grid_dimensions = (int)Math.Ceiling(Math.Sqrt(graph.nodes.Count));
            int[] grid = new int[grid_dimensions * grid_dimensions];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = i;
            }

            var edges_array = graph.edges.ToArray();

            var PermutationCount = Sequencer.PermutationsCount(grid.Count());
                ProgressCurrent  = 0;
                ProgressMaximum  = 100;
            var ProgressStep     = PermutationCount / 100;
            if(0 >= ProgressStep)
            {
                ProgressStep    = 1;
                ProgressMaximum = 1;
            }
            
            var intersection_count = int.MaxValue;
            for (ProgressCurrent = 0; ProgressCurrent < ProgressMaximum; ProgressCurrent++)
            {

                await Task.Run(
                    new Action(
                        () =>
                        {
                            for (int i = 0; i < ProgressStep; i++)
                            {
                                ProcessPermutation(
                                    graph, 
                                    grid, 
                                    grid_dimensions, 
                                    edges_array, 
                                    candidates,
                                    ref intersection_count
                                );
                                if (!Sequencer.NextPermutation(grid))
                                {
                                    return;
                                }
                            }
                        }
                    )
                );
                
            }
            
            if(0>=candidates.Count)
            {
                throw new Exception("Could not grid graph, candidates is zero");
            }

            return candidates;

        }

        private void ProcessPermutation(Graph graph, int[] grid, int grid_dimensions, Tuple<int, int, string>[] edges_array, List<Point[]> candidates, ref int intersection_count)
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
            
        }

    }

}
