using Grapher.Models;
using Grapher.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;

namespace Grapher.ViewModels.Graphs
{

    public class Stage3GraphViewModel : GraphViewModel
    {

        public ObservableCollection<object> Children
        {
            get
            {
                if (null == children)
                {
                    children = new ObservableCollection<object>();
                }
                return children;
            }
        }
        private ObservableCollection<object> children;
        
        public Stage3GraphViewModel(Stage2GraphViewModel PreviousStage) : base(PreviousStage)
        {
            stage = Stages.Stage3;
        }

        public override async void Process()
        {

            foreach (var nodeModel in Model.Nodes)
            {
                var nodeViewModel = new NodeViewModel()
                {
                    Model = nodeModel, 
                };
                Children.Add(nodeViewModel);
            }
        
            
            //EdgeViewModels  = new List<EdgeViewModel>(Model.Edges.Count);
            //EdgeViews       = new List<GraphEdge>(Model.Edges.Count);
            //
            //foreach(var edge in Model.Edges)
            //{
            //    EdgeViewModels.Add(
            //        new EdgeViewModel()
            //        {
            //            Model = edge
            //        }
            //    );
            //    EdgeViews.Add(new GraphEdge(EdgeViewModels.Last()));
            //    Children.Add(EdgeViews.Last().Baseline);
            //    Children.Add(EdgeViews.Last().ArrowAlpha);
            //    Children.Add(EdgeViews.Last().ArrowBravo);
            //    Children.Add(EdgeViews.Last().Label);
            //}
            //
            ////foreach (var node in Model.Nodes)
            ////{
            ////    node.ActivateGridWithoutOffset(0);
            ////}
            //
            //var WidestEdge  = 0.0;
            //var HighestEdge = 0.0;
            //foreach (var Edge in EdgeViewModels)
            //{
            //    WidestEdge  = Math.Max(WidestEdge, Edge.MinWidth);
            //    HighestEdge = Math.Max(HighestEdge, Edge.MinHeight);
            //}
            //
            ////foreach (var node in Model.Nodes)
            ////{
            ////    node.ActivateGridWithOffset(0, WidestEdge, HighestEdge);
            ////    // ToDo: evaluate if this candidate is good
            ////}
            
        }
        
        private void NodeViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if( "Left"  == e.PropertyName || "Top"    == e.PropertyName ||
                "Width" == e.PropertyName || "Height" == e.PropertyName )
            {
                var WidthOfCanvas  = 0.0;
                var HeightOfCanvas = 0.0;
                foreach (var child in Children)
                {
                    var nodeViewModel = child as NodeViewModel;
                    WidthOfCanvas  = Math.Max(WidthOfCanvas,  nodeViewModel.Left + nodeViewModel.Width);
                    HeightOfCanvas = Math.Max(HeightOfCanvas, nodeViewModel.Top  + nodeViewModel.Height);
                }
                Width  = WidthOfCanvas;
                Height = HeightOfCanvas;
            }
        }

    }

}
