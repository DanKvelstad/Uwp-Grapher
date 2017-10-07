using Grapher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Grapher.ViewModels
{

    public class GraphLayouter : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _Label;
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                _Label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
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

        public async Task<List<Point[]>> GridItAsync(GraphModel graph)
        {

            var grid                = new Grid(graph);
            var candidates          = new List<Point[]>();

            Label = "Gridding (1/2)";

            var PermutationCount    = grid.PermutationsCount();
                ProgressCurrent     = 0;
                ProgressMaximum     = 100;
            var ProgressStep        = (int)Math.Ceiling(PermutationCount / 100.0);
            
            var intersection_count = int.MaxValue;
            for (ProgressCurrent = 0; ProgressCurrent < ProgressMaximum; ProgressCurrent++)
            {
                await Task.Run(
                    new Action(
                        () =>
                        {
                            for (int i = 0; i < ProgressStep; i++)
                            {
                                if (grid.intersection_count == intersection_count)
                                {
                                    candidates.Add((Point[])grid.candidate.Clone());
                                }
                                else if (grid.intersection_count < intersection_count)
                                {
                                    candidates.Clear();
                                    candidates.Add((Point[])grid.candidate.Clone());
                                    intersection_count = grid.intersection_count;
                                }
                                if (!grid.Next())
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

            Label = "Evaluating (2/2)";

            PermutationCount = (int)Math.Ceiling(candidates.Count * Math.Log(candidates.Count));
            ProgressCurrent  = 0;
            ProgressMaximum  = 100;
            ProgressStep     = (int)Math.Ceiling(PermutationCount / 100.0);

            int ProgressIndex = 0;
            var Sorting = Task.Run(
                new Action(() =>
                {
                    candidates.Sort(
                        (a, b) =>
                        {
                            
                            Volatile.Write(
                                ref ProgressIndex, 
                                Volatile.Read(ref ProgressIndex) + 1
                            );

                            int a_cost = 0;
                            foreach (var edge in graph.edges)
                            {
                                var from_index = graph.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Source;
                                    }
                                );
                                var to_index = graph.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Target;
                                    }
                                );
                                var p1 = a[from_index];
                                var p2 = a[to_index];
                                var dx = p1.X - p2.X;
                                var dy = p1.Y - p2.Y;
                                a_cost += (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
                            }

                            int b_cost = 0;
                            foreach (var edge in graph.edges)
                            {
                                var from_index = graph.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Source;
                                    }
                                );
                                var to_index = graph.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Target;
                                    }
                                );
                                var p1 = b[from_index];
                                var p2 = b[to_index];
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
                })
            );

            while(!Sorting.IsCompleted)
            {
                if(_ProgressCurrent<ProgressMaximum)
                {
                    while (Volatile.Read(ref ProgressIndex) > ProgressStep)
                    {
                        ProgressCurrent++;
                        Volatile.Write(
                            ref ProgressIndex,
                            Volatile.Read(ref ProgressIndex) - ProgressStep
                        );
                    }
                    await Task.Delay(10);
                }
                else
                {
                    Sorting.Wait();
                }
            }
            Sorting.Wait();

            return candidates;

        }

    }

}
