using Grapher.Xaml;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher
{

    public class GraphControls : ObservableCollection<GraphView>
    {
        public GraphControls()
        {
        }
    }

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
          e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {

            ContentDialog Dialog = new ContentDialog
            {
                Title = "Error opening file",
                Content = "Verify that it is a valid file for this program.",
                CloseButtonText = "Ok"
            };

            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                foreach(var item in await e.DataView.GetStorageItemsAsync())
                {
                    var File = item as IStorageFile;
                    if(null == File)
                    {
                        await Dialog.ShowAsync();
                    }
                    else
                    {
                        var graph = await Serialization.Serializor.Deserialize(File);
                        if (null == graph)
                        {
                            await Dialog.ShowAsync();
                        }
                        else
                        {
                            var GraphContr = new GraphView();
                            (Resources["Graphs"] as GraphControls).Add(GraphContr);
                            GraphContr.CloseGraphControlEvent += (sender2, e2) => (Resources["Graphs"] as GraphControls).Remove(GraphContr);
                            GraphContr.Initialize(graph);
                        }
                    }
                }
            }

        }

    }
}
