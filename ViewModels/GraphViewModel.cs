using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.ViewModels
{
    class GraphViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private GraphModel _Model;
        public GraphModel Model
        {
            get
            {
                return _Model;
            }
            private set
            {
                _Model = value;
            }
        }

        public string Label
        {
            get
            {
                return Model.Label;
            }
            set
            {
                Model.Label = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
            }
        }

        private GraphLayouter _Gridder = new GraphLayouter();
        public GraphLayouter Gridder
        {
            get
            {
                return _Gridder;
            }
        }

        private GraphPresenter _Layouter = new GraphPresenter();
        public GraphPresenter Layouter
        {
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

        public async void ProcessAsync(GraphModel Model)
        {
            this.Model = Model;
            ActiveState = States.Gridding;
            var candidates = await Gridder.GridItAsync(this.Model);
            ActiveState = States.Displaying;
            Layouter.LayoutIt(this.Model, candidates);
        }

    }
}
