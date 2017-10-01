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
    class GraphProcessor : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
        private GraphGridder _Gridder = new GraphGridder();
        public  GraphGridder Gridder
        {
            private set
            {
                _Gridder = value;
            }
            get
            {
                return _Gridder;
            }
        }

        private GraphLayouter _Layouter = new GraphLayouter();
        public GraphLayouter Layouter
        {
            private set
            {
                _Layouter = value;
            }
            get
            {
                return _Layouter;
            }
        }

        public enum States
        {
            Uninitiated,
            Gridding,
            Layouting,
            Displaying
        }
        private States _ActiveState = States.Uninitiated;
        public States ActiveState
        {
            get
            {
                return _ActiveState;
            }
            set
            {
                _ActiveState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveState"));
            }
        }

        public async void ProcessAsync(Graph graph)
        {
            ActiveState = States.Gridding;
            var candidates = await Gridder.GridItAsync(graph);
            ActiveState = States.Layouting;
            await Layouter.LayoutIt(graph, candidates);
            ActiveState = States.Displaying;
            // ToDo: Displayer.DisplayIt
        }

    }
}
