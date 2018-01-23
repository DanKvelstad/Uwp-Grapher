using Grapher.ViewModels;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher
{

    public class GraphStageSelector : DataTemplateSelector
    {

        public DataTemplate Stage1Template
        {
            get;
            set;
        }

        public DataTemplate Stage2Template
        {
            get;
            set;
        }

        public DataTemplate Stage3Template
        {
            get;
            set;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item is ViewModels.Graphs.Stage1GraphViewModel)
            {
                return Stage1Template;
            }
            else if (item is ViewModels.Graphs.Stage2GraphViewModel)
            {
                return Stage2Template;
            }
            else if (item is ViewModels.Graphs.Stage3GraphViewModel)
            {
                return Stage3Template;
            }
            else
            {
                return base.SelectTemplateCore(item, container);
            }

        }
    }

    public class GraphElementSelector : DataTemplateSelector
    {

        public DataTemplate NodeTemplate
        {
            get;
            set;
        }

        public DataTemplate EdgeTemplate
        {
            get;
            set;
        }
        
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item is ViewModels.NodeViewModel)
            {
                return NodeTemplate;
            }
            else if (item is ViewModels.EdgeViewModel)
            {
                return EdgeTemplate;
            }
            else
            {
                return base.SelectTemplateCore(item, container);
            }

        }
    }

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
