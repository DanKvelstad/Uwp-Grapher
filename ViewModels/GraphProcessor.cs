using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Grapher.ViewModels
{
    class GraphProcessor : INotifyPropertyChanged
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
                OnPropertyChanged("ActiveState");
            }
        }

        public void Process(Graph graph)
        {
            Task.Run(() =>
                {
                    ActiveState = States.Gridding;
                    var candidates = Gridder.GridIt(graph);
                    ActiveState = States.Layouting;
                    Layouter.LayoutIt(graph, candidates);
                    ActiveState = States.Displaying;
                    // ToDo: Displayer.DisplayIt
                }
            );
        }

    }
}
