using Grapher.Algorithms;
using Grapher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grapher.ViewModels.Graphs
{
    public class Stage1GraphViewModel : GraphViewModel
    {

        public Stage1GraphViewModel(GraphViewModel viewModel) : base(viewModel)
        {
            stage = Stages.Stage1;
        }

        public  int ProgressMaximum
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
                if(value>ProgressMaximum)
                {
                    throw new ArgumentOutOfRangeException();
                }
                progressCurrent = value;
                OnPropertyChanged(nameof(ProgressCurrent));
            }
        }
        private int progressCurrent;

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

        public override async void Process()
        {

            var grid    = new Grid(Model);
            Candidates  = new List<Point[]>();

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
                                if (grid.Intersection_count == intersection_count)
                                {
                                    Candidates.Add(grid.Candidate);
                                }
                                else if (grid.Intersection_count < intersection_count)
                                {
                                    Candidates.Clear();
                                    Candidates.Add(new Point[grid.Candidate.Length]);
                                    grid.Candidate.CopyTo(Candidates.Last(), 0);
                                    intersection_count = grid.Intersection_count;
                                }
                                if (0==intersection_count)
                                {
                                    return;
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

            Stage = Stages.Stage2;
            
        }

    }
}
