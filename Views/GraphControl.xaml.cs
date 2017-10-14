using Grapher.Serialization;
using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Grapher.Xaml
{

    public sealed partial class GraphControl : UserControl
    {
        
        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        public event ValueChangedEventHandler CloseGraphControlEvent;
        GraphProcessor processor = new GraphProcessor();
        GraphModel Graph;

        public GraphControl()
        {
            this.InitializeComponent();
        }

        public void Initialize(GraphModel graph)
        {
            Graph = graph;
            processor.ProcessAsync(graph);
        }
        
        private void MenuFlyoutItem_Delete(object sender, RoutedEventArgs e)
        {
            CloseGraphControlEvent?.Invoke(this, EventArgs.Empty);
        }

        private async void MenuFlyoutItem_SaveAs(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("XML Document", new List<string>() { ".xml" });
            savePicker.FileTypeChoices.Add("C++", new List<string>() { ".cpp" });
            savePicker.SuggestedFileName = "New Document";
            var File = await savePicker.PickSaveFileAsync();
            if(null!=File)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(File);
                var Stream = await File.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                using (var outputStream = Stream.GetOutputStreamAt(0))
                {
                    switch (File.FileType)
                    {
                        case ".xml":
                            Serializor.SerializeAsXml(Graph, outputStream);
                            break;
                        case ".cpp":
                            break;
                        default:
                            break;
                    }
                    await outputStream.FlushAsync();
                }
                var status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(File);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    //this.textBlock.Text = "File " + File.Name + " was saved.";
                }
                else
                {
                    //this.textBlock.Text = "File " + File.Name + " couldn't be saved.";
                }
                
            }
        }
    }

    public class ConverterVisibileIfStateIsProcessing : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (GraphProcessor.States.Gridding == (GraphProcessor.States)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterVisibileIfStateIsDisplaying : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (GraphProcessor.States.Displaying == (GraphProcessor.States)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

}
