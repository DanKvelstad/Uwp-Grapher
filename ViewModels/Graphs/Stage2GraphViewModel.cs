using Grapher.Algorithms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Grapher.ViewModels.Graphs
{
    public class Stage2GraphViewModel : GraphViewModel
    {
        public Stage2GraphViewModel(Stage1GraphViewModel PreviousStage) : base(PreviousStage)
        {
            Candidates = PreviousStage.Candidates;
            stage      = Stages.Stage2;
        }
        public List<Point[]> Candidates
        {
            get
            {
                return candidates;
            }
            private set
            {
                candidates = value;
            }
        }
        private List<Point[]> candidates;


        public int ProgressMaximum
        {
            get
            {
                return progressMaximum;
            }
            private set
            {
                if (value > Width)
                {
                    throw new ArgumentOutOfRangeException();
                }
                progressMaximum = value;
                OnPropertyChanged(nameof(ProgressMaximum));
            }
        }
        private int progressMaximum;

        public int ProgressCurrent
        {
            get
            {
                return progressCurrent;
            }
            private set
            {
                if (value > ProgressMaximum)
                {
                    throw new ArgumentOutOfRangeException();
                }
                progressCurrent = value;
                OnPropertyChanged(nameof(ProgressCurrent));
            }
        }
        private int progressCurrent;

        public override async void Process()
        {

            var PermutationCount = (int)Math.Ceiling(Candidates.Count * Math.Log(Candidates.Count));
            ProgressCurrent = 0;
            ProgressMaximum = 100;
            var ProgressStep = (int)Math.Ceiling(PermutationCount / 100.0);

            int ProgressIndex = 0;
            var Sorting = Task.Run(
                new Action(() =>
                {
                    Candidates.Sort(
                        (a, b) =>
                        {

                            Volatile.Write(
                                ref ProgressIndex,
                                Volatile.Read(ref ProgressIndex) + 1
                            );

                            int a_cost = 0;
                            foreach (var edge in Model.edges)
                            {
                                var from_index = Model.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Source;
                                    }
                                );
                                var to_index = Model.nodes.FindIndex(
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
                            foreach (var edge in Model.edges)
                            {
                                var from_index = Model.nodes.FindIndex(
                                    (x) =>
                                    {
                                        return x.Label == edge.Source;
                                    }
                                );
                                var to_index = Model.nodes.FindIndex(
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

            while (!Sorting.IsCompleted)
            {
                if (progressCurrent < ProgressMaximum)
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

            Stage = Stages.Stage3;

        }

    }
}
