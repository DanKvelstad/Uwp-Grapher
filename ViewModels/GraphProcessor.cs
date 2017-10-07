using System.ComponentModel;

namespace Grapher.ViewModels
{
    class GraphProcessor : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
        private GraphLayouter _Gridder = new GraphLayouter();
        public  GraphLayouter Gridder
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

        private GraphPresenter _Layouter = new GraphPresenter();
        public GraphPresenter Layouter
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

        public async void ProcessAsync(GraphModel graph)
        {
            ActiveState = States.Gridding;
            var candidates = await Gridder.GridItAsync(graph);
            ActiveState = States.Displaying;
            Layouter.LayoutIt(graph, candidates);
        }

    }
}
