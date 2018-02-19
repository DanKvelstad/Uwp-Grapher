using Grapher.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher.Views
{
    public sealed partial class NodeUserControl : UserControl
    {

        private NodeViewModel viewModel;
        
        public NodeUserControl(NodeViewModel viewModel)
        {

            this.viewModel = viewModel;

            InitializeComponent();

        }
        
        private void TextBlock_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            var border = (sender as TextBlock).Parent as Border;
            viewModel.Width  = border.CornerRadius.TopLeft + e.NewSize.Width  + border.Padding.Left + border.Padding.Right  + border.CornerRadius.TopRight;
            viewModel.Height = border.CornerRadius.TopLeft + e.NewSize.Height + border.Padding.Top  + border.Padding.Bottom + border.CornerRadius.BottomLeft;
        }
        
    }
}
