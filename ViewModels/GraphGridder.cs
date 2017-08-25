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
                graph.candidates = value;
                if (0 == _progress % 1000/*(maximum / available_resolution)*/ || maximum == _progress)
                {
                    OnPropertyChanged("candidates");
                }
            }
        }

        public Graph graph;
        
        public int PermutationsCount(double state_count)
        {
            int grid_dimensions = (int)Math.Ceiling(Math.Sqrt(state_count));
            int n = grid_dimensions * grid_dimensions;
            return n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);
        }

        public void Layout()
        {

            ActiveState = States.Gridding;
            var new_candidates = new List<Point[]>();
            
            maximum = PermutationsCount(graph.nodes.Count + 1);
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
                            Intersection(
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

            } while (NextPermutation(grid));

            new_candidates.Sort(
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

            candidates = new_candidates;
            ActiveState = States.Layouting;

        }

        private bool NextPermutation(int[] perm)
        {

            int n = perm.Length;

            int k = -1;

            for (int i = 1; i < n; i++)

                if (perm[i - 1] < perm[i])

                    k = i - 1;

            if (k == -1)

            {

                for (int i = 0; i < n; i++)

                    perm[i] = i;

                return false;

            }



            int l = k + 1;

            for (int i = l; i < n; i++)

                if (perm[k] < perm[i])

                    l = i;



            int t = perm[k];

            perm[k] = perm[l];

            perm[l] = t;



            Array.Reverse(perm, k + 1, perm.Length - (k + 1));



            return true;

        }

        private bool Intersection(Point ts, Point tt, Point os, Point ot)
        {

            // https://en.wikipedia.org/wiki/line%E2%80%93line_intersection

            var denominator = (ts.X - tt.X) * (os.Y - ot.Y) - (ts.Y - tt.Y) * (os.X - ot.X);

            if (-0.001 < denominator && 0.001 > denominator)
            {   // "When the two lines are parallel or coincident the denominator is zero"

                // |  |   |
                // |  |   ||
                // |  ||  ||
                // |   |  |

                if (ts == os && tt == ot)
                {   // They are the same segment in the same different directions
                    return true;
                }
                else if (ts == ot && tt == os)
                {   // They are the same segment in different directions
                    return true;
                }
                else if (Contains(ts, tt, os))
                {
                    return true;
                }
                else if (Contains(ts, tt, ot))
                {
                    return true;
                }
                else if (Contains(os, ot, ts))
                {
                    return true;
                }
                else if (Contains(os, ot, tt))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {   // " Note that the intersection point is for the infinitely long lines defined by the points, 
                //   rather than the line segments between the points, 
                //   and can produce an intersection point beyond the lengths of the line segments. "

                if (ts == os || ts == ot || tt == os || tt == ot)
                {
                    return false;
                }
                else
                {

                    var point = new Point(
                        (ts.X * tt.Y - ts.Y * tt.X) * (os.X - ot.X) - (ts.X - tt.X) * (os.X * ot.Y - os.Y * ot.X) / denominator,
                        (ts.X * tt.Y - ts.Y * tt.X) * (os.Y - ot.Y) - (ts.Y - tt.Y) * (os.X * ot.Y - os.Y * ot.X) / denominator
                    );

                    return Contains(ts, tt, point) && Contains(os, ot, point);

                }
            }

        }

        private bool Contains(Point s, Point t, Point p)
        {

            var cross_product = (p.Y - s.Y) * (t.X - s.X) - (p.X - s.X) * (t.Y - s.Y);

            if (s == p)
            {   // if p is source
                return false;
            }
            else if (t == p)
            {   // if p is target
                return false;
            }
            else if (!(0.1 > cross_product && -0.1 < cross_product))
            {   // if p is not on the line
                return false;
            }
            else
            {   // else p is betw<een source and target

                // http://stackoverflow.com/a/328122

                var dot_product = (p.X - s.X) * (t.X - s.X) + (p.Y - s.Y) * (t.Y - s.Y);
                var squared_length = (t.X - s.X) * (t.X - s.X) + (t.Y - s.Y) * (t.Y - s.Y);

                if (0.0 <= dot_product && dot_product <= squared_length)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

    }

}
