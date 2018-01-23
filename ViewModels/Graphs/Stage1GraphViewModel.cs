using Grapher.Algorithms;
using System;
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
        
        public override async void Process()
        {
            uint dimensions = (uint)Math.Ceiling(Math.Sqrt(Model.Nodes.Count));
            var permutationArray = new PermutationArray(dimensions, dimensions);

            var PermutationCount    = permutationArray.Count;
                ProgressCurrent     = 0;
                ProgressMaximum     = 100;
            var ProgressStep        = (int)Math.Ceiling(PermutationCount / 100.0);
            
            var intersection_count_best = int.MaxValue;
            for (ProgressCurrent = 0; ProgressCurrent < ProgressMaximum; ProgressCurrent++)
            {
                await Web.CalculateNodeGrids(
                    Model,
                    permutationArray,
                    ProgressCurrent * ProgressStep,
                    ProgressStep,
                    intersection_count_best
                );
            }

            Stage = Stages.Stage2;
            
        }

    }
}
