using Grapher.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Grapher.ViewModels
{
    public class GraphViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphModel Model
        {
            get
            {
                return model;
            }
            set
            {

                if (null != model)
                {

                    model.EdgeModels.CollectionChanged -= EdgeModels_CollectionChanged;
                    EdgeModels_CollectionChanged(
                        model.EdgeModels,
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Remove,
                            model.EdgeModels
                        )
                    );

                    model.NodeModels.CollectionChanged -= NodeModels_CollectionChanged;
                    NodeModels_CollectionChanged(
                        model.NodeModels,
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Remove,
                            model.NodeModels
                        )
                    );

                    model.Dimensions.PropertyChanged   -= Dimensions_PropertyChanged;

                    model.PropertyChanged              -= Model_PropertyChanged;
                    
                }

                model = value;

                if (null != model)
                {

                    model.PropertyChanged += Model_PropertyChanged;

                    model.Dimensions.PropertyChanged += Dimensions_PropertyChanged;

                    model.NodeModels.CollectionChanged += NodeModels_CollectionChanged;
                    NodeModels_CollectionChanged(
                        model.NodeModels, 
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Add, 
                            model.NodeModels
                        )
                    );

                    model.EdgeModels.CollectionChanged += EdgeModels_CollectionChanged;
                    EdgeModels_CollectionChanged(
                        model.EdgeModels,
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Add,
                            model.EdgeModels
                        )
                    );

                }

                PropertyChanged?.Invoke(
                    model,
                    new PropertyChangedEventArgs(null)
                );

            }
        }
        GraphModel model;

        private void Dimensions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DimensionsModel.GraphWidth):
                    PropertyChanged.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Width))
                    );
                    break;
                case nameof(DimensionsModel.GraphHeight):
                    PropertyChanged.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Height))
                    );
                    break;
            }
        }
        
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(GraphModel.Label):
                    PropertyChanged.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Label))
                    );
                    break;
            }
        }

        private void NodeModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var item in e.NewItems)
                {
                    NodeViewModels.Add(
                        new NodeViewModel()
                        {
                            Model = item as NodeModel,
                        }
                    );
                }
            }
            if (null != e.OldItems)
            {
                var deleteUs = new List<NodeViewModel>();
                foreach (var item in e.OldItems)
                {
                    var model = item as NodeModel;
                    foreach(var child in NodeViewModels)
                    {
                        var viewModel = child as NodeViewModel;
                        if (model == viewModel.Model)
                        {
                            deleteUs.Add(viewModel);
                        }
                    }
                }
                foreach(var deleteMe in deleteUs)
                {
                    NodeViewModels.Remove(deleteMe);
                }
            }
        }

        private void EdgeModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var item in e.NewItems)
                {
                    EdgeViewModels.Add(
                        new EdgeViewModel()
                        {
                            Model = item as EdgeModel,
                        }
                    );
                }
            }
            if (null != e.OldItems)
            {
                var deleteUs = new List<EdgeViewModel>();
                foreach (var item in e.OldItems)
                {
                    var model = item as EdgeModel;
                    foreach (var child in EdgeViewModels)
                    {
                        var viewModel = child as EdgeViewModel;
                        if (model == viewModel.Model)
                        {
                            deleteUs.Add(viewModel);
                        }
                    }
                }
                foreach (var deleteMe in deleteUs)
                {
                    EdgeViewModels.Remove(deleteMe);
                }
            }
        }

        public string Label
        {
            get
            {
                return Model.Label;
            }
        }

        public double Width
        {
            get
            {
                return model.Dimensions.GraphWidth;
            }
        }
        
        public double Height
        {
            get
            {
                return model.Dimensions.GraphHeight;
            }
        }
        
        public ObservableCollection<NodeViewModel> NodeViewModels
        {
            get
            {
                if (null == nodeViewModels)
                {
                    nodeViewModels = new ObservableCollection<NodeViewModel>();
                }
                return nodeViewModels;
            }
        }
        private ObservableCollection<NodeViewModel> nodeViewModels;

        public ObservableCollection<EdgeViewModel> EdgeViewModels
        {
            get
            {
                if (null == edgeViewModels)
                {
                    edgeViewModels = new ObservableCollection<EdgeViewModel>();
                }
                return edgeViewModels;
            }
        }
        private ObservableCollection<EdgeViewModel> edgeViewModels;
        
    }

}
