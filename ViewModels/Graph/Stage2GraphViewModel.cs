using Grapher.Algorithms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Grapher.ViewModels.Graphs
{
    public class Stage2GraphViewModel : GraphViewModel
    {
        public Stage2GraphViewModel(Stage1GraphViewModel PreviousStage) : base(PreviousStage)
        {
            stage = Stages.Stage2;
        }

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

            ProgressCurrent  = 0;
            ProgressMaximum  = 100;
            var ProgressStep = (int)Math.Ceiling(Model.Nodes[0].Grid.Count / 100.0);

            var Costs = new List<Tuple<uint, int>>(Model.Nodes[0].Grid.Count);
            for (ProgressCurrent = 0; ProgressCurrent < ProgressMaximum; ProgressCurrent++)
            {
                await Web.CalculateCostsOfRange(
                    Costs, 
                    Model.Edges.ToList(), 
                    ProgressCurrent * ProgressStep, 
                    ProgressStep
                );
            }
            await Web.OrderGridAccordingToCost(
                Costs, 
                Model.Nodes.ToList()
            );
            
            Stage = Stages.Stage3;

        }

    }
}
