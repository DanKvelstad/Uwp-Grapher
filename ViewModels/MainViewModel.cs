using Grapher.ViewModels.Graphs;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;

namespace Grapher.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Model.Graphs.CollectionChanged    += GraphsModels_CollectionChanged;
            GraphViewModels.CollectionChanged += GraphsViewModels_CollectionChanged;
        }

        private void GraphsModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var GraphModel in e.NewItems)
                    {
                        GraphViewModels.Add(
                            new Graphs.GraphViewModel(
                                GraphModel as Models.GraphModel
                            )
                        );
                        Task.Run(new Action(GraphViewModels[GraphViewModels.Count - 1].Process));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var GraphModel in e.OldItems)
                    {
                        foreach (var GraphViewModel in GraphViewModels)
                        {
                            if (GraphViewModel.Model == GraphModel)
                            {
                                GraphViewModels.Remove(GraphViewModel);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void GraphsViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (GraphViewModel GraphViewhModel in e.NewItems)
                    {
                        GraphViewhModel.PropertyChanged += GraphViewModel_PropertyChangedAsync;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (GraphViewModel GraphViewhModel in e.OldItems)
                    {
                        GraphViewhModel.PropertyChanged -= GraphViewModel_PropertyChangedAsync;
                    }
                    foreach (GraphViewModel GraphViewhModel in e.NewItems)
                    {
                        GraphViewhModel.PropertyChanged += GraphViewModel_PropertyChangedAsync;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (GraphViewModel GraphViewhModel in e.OldItems)
                    {
                        GraphViewhModel.PropertyChanged -= GraphViewModel_PropertyChangedAsync;
                    }
                    break;
                default:
                    break;
            }
        }

        private async void GraphViewModel_PropertyChangedAsync(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            var GraphViewModel = sender as GraphViewModel;

            if (e.PropertyName == nameof(Graphs.GraphViewModel.Stage))
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {

                        switch (GraphViewModel.Stage)
                        {
                            case GraphViewModel.Stages.Stage1:
                                var Stage1GraphViewModel = new Stage1GraphViewModel(
                                    (GraphViewModel as GraphViewModel)
                                );
                                GraphViewModels[GraphViewModels.IndexOf(GraphViewModel)] = Stage1GraphViewModel;
                                GraphViewModel = Stage1GraphViewModel;
                                break;
                            case GraphViewModel.Stages.Stage2:
                                var Stage2GraphViewModel = new Stage2GraphViewModel(
                                    GraphViewModel as Stage1GraphViewModel
                                );
                                GraphViewModels[GraphViewModels.IndexOf(GraphViewModel)] = Stage2GraphViewModel;
                                GraphViewModel = Stage2GraphViewModel;
                                break;
                            case GraphViewModel.Stages.Stage3:
                                var Stage3GraphViewModel = new Stage3GraphViewModel(
                                    GraphViewModel as Stage2GraphViewModel
                                );
                                GraphViewModels[GraphViewModels.IndexOf(GraphViewModel)] = Stage3GraphViewModel;
                                GraphViewModel = Stage3GraphViewModel;
                                break;
                        }

                        GraphViewModel.Process();

                    }

                );

            };
            
        }

        public Models.MainModel Model
        {
            get
            {
                if (null == model)
                {
                    model = new Models.MainModel();
                }
                return model;
            }
        }
        private Models.MainModel model;

        public ObservableCollection<Graphs.GraphViewModel> GraphViewModels
        {
            get
            {
                if (null == graphViewModels)
                {
                    graphViewModels = new ObservableCollection<Graphs.GraphViewModel>();
                }
                return graphViewModels;
            }
        }
        private ObservableCollection<Graphs.GraphViewModel> graphViewModels;

        public async Task<bool> OpenAsync(IStorageFile File)
        {
            var Graph = await Serialization.Serializor.Deserialize(File);
            if (null == Graph)
            {
                return false;
            }
            else
            {
                Model.Graphs.Add(Graph);
                return true;
            }
        }

    }

}
