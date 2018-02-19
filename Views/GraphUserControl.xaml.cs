using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher.Views
{
    public sealed partial class GraphUserControl : UserControl
    {

        private GraphViewModel        viewModel;
        private List<NodeUserControl> nodeViews;
        private List<EdgeUserControl> edgeViews;

        public GraphUserControl(GraphViewModel graphViewModel)
        {

            viewModel = graphViewModel;
            viewModel.NodeViewModels.CollectionChanged += ViewModels_CollectionChanged;
            viewModel.EdgeViewModels.CollectionChanged += ViewModels_CollectionChanged;

            nodeViews = new List<NodeUserControl>();
            edgeViews = new List<EdgeUserControl>();

            InitializeComponent();

            ViewModels_CollectionChanged(
                viewModel.NodeViewModels,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    viewModel.NodeViewModels
                )
            );
            ViewModels_CollectionChanged(
                viewModel.EdgeViewModels,
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    viewModel.EdgeViewModels
                )
            );

        }

        private void ViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                if(item is NodeViewModel)
                {
                    var nodeUserControl = new NodeUserControl(item as NodeViewModel);
                    nodeViews.Add(nodeUserControl);
                    canvas.Children.Add(nodeUserControl);
                }
                else if(item is EdgeViewModel)
                {
                    var edgeUserControl = new EdgeUserControl(item as EdgeViewModel);
                    edgeViews.Add(edgeUserControl);
                    canvas.Children.Add(edgeUserControl);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

    }
}
