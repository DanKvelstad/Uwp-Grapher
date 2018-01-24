using Grapher.ViewModels;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher
{

    public sealed partial class MainPage : Page
    {

        private ViewModels.MainViewModel main;

        public MainPage()
        {
            main = new ViewModels.MainViewModel();
            InitializeComponent();
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
          e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                foreach(var item in await e.DataView.GetStorageItemsAsync())
                {
                    if (item is IStorageFile File)
                    {
                        if(!await main.OpenAsync(File))
                        {
                            //Error
                        }
                    }
                }
            }
        }
        
        private void SetMinimumToDesiredSize(object sender, SizeChangedEventArgs e)
        {
            var element = sender as FrameworkElement;
            element.MinWidth  = element.DesiredSize.Width;
            element.MinHeight = element.DesiredSize.Height;
        }

    }

}
