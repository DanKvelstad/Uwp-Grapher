using Grapher.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Windows.Storage;

namespace Grapher.ViewModels
{

    public class MainViewModel
    {
        
        public MainModel Model
        {
            get
            {
                if (null == model)
                {
                    Model = new Models.MainModel();
                }
                return model;
            }
            set
            {

                if (null != model)
                {
                    model.GraphModels.CollectionChanged -= GraphModels_CollectionChanged;
                }

                model = value;

                if (null != model)
                {
                    model.GraphModels.CollectionChanged += GraphModels_CollectionChanged;
                }

            }
        }
        private MainModel model;

        private void GraphModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var graphModel in e.NewItems)
                    {
                        GraphViewModels.Add(
                            new GraphViewModel()
                            {
                                Model = graphModel as GraphModel
                            }
                        );
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var graphModel in e.OldItems)
                    {
                        foreach (var graphViewModel in GraphViewModels)
                        {
                            if (graphViewModel.Model == graphModel)
                            {
                                GraphViewModels.Remove(graphViewModel);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        public ObservableCollection<GraphViewModel> GraphViewModels
        {
            get
            {
                if (null == graphViewModels)
                {
                    graphViewModels = new ObservableCollection<GraphViewModel>();
                }
                return graphViewModels;
            }
        }
        private ObservableCollection<GraphViewModel> graphViewModels;

        public async Task<bool> OpenFileAsync(IStorageFile File)
        {
            var graphModel = await Serialization.Serializor.Deserialize(File);
            if (null == graphModel)
            {
                return false;
            }
            else
            {
                Model.GraphModels.Add(graphModel);
                return true;
            }
        }
        
    }

}
