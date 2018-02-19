using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher.Views
{
    public sealed partial class GraphPanelUserControl : UserControl
    {

        private MainViewModel mainViewModel;
        private ObservableCollection<GraphUserControl> graphUserControls;

        public GraphPanelUserControl()
        {

            mainViewModel = new MainViewModel();
            mainViewModel.GraphViewModels.CollectionChanged += GraphViewModels_CollectionChanged;

            graphUserControls = new ObservableCollection<GraphUserControl>();

            InitializeComponent();
            
        }

        public void GraphViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var item in e.NewItems)
                {
                    graphUserControls.Add(
                        new GraphUserControl(
                            item as GraphViewModel
                        )
                    );
                }
            }
        }

        public void Page_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Page_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                foreach (var item in await e.DataView.GetStorageItemsAsync())
                {
                    if (item is IStorageFile File)
                    {
                        if (!await mainViewModel.OpenFileAsync(File))
                        {
                            //Error
                        }
                    }
                }
            }
        }

    }
}
