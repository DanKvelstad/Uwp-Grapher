using Grapher.Models;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Grapher.Services
{

    public class GraphGridToPixelService
    {

        public DimensionsModel Dimensions
        {
            set
            {

                if (null != dimensions)
                {
                    dimensions.PropertyChanged -= Dimensions_PropertyChanged;
                }

                dimensions = value;

                if (null != dimensions)
                {
                    dimensions.PropertyChanged += Dimensions_PropertyChanged;
                }

            }
        }
        private DimensionsModel dimensions;

        private void Dimensions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            DimensionsModel dimensions = sender as DimensionsModel;
            uint cols = (uint)Math.Ceiling(Math.Sqrt(dimensions.Nodes.Count()));

            switch (e.PropertyName)
            {

                case nameof(DimensionsModel.NodeWidth):
                case nameof(DimensionsModel.EdgeWidth):
                    var horizontalOffset = dimensions.NodeWidth + dimensions.EdgeWidth;
                    foreach (var nodeModel in dimensions.Nodes)
                    {
                        nodeModel.Geometry.UpdateHorizontally(
                            nodeModel.Grid.X * horizontalOffset,
                            dimensions.NodeWidth
                        );
                    }
                    break;

                case nameof(DimensionsModel.NodeHeight):
                case nameof(DimensionsModel.EdgeHeight):
                    var verticalOffset = dimensions.NodeHeight + dimensions.EdgeHeight;
                    foreach (var nodeModel in dimensions.Nodes)
                    {
                        nodeModel.Geometry.UpdateVertically(
                            nodeModel.Grid.Y * verticalOffset,
                            dimensions.NodeHeight
                        );
                    }
                    break;

            }

        }

        public void Nodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var nodeModel in e.NewItems)
                {
                    (nodeModel as NodeModel).PropertyChanged += NodeModel_PropertyChanged;
                }
            }
            if (null != e.OldItems)
            {
                foreach (var nodeModel in e.OldItems)
                {
                    (nodeModel as NodeModel).PropertyChanged -= NodeModel_PropertyChanged;
                }
            }
        }

        private void NodeModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if(nameof(NodeModel.Grid) == e.PropertyName)
            {

                var nodeModel    = sender as NodeModel;

                var horizontalOffset = dimensions.NodeWidth  + dimensions.EdgeWidth;
                nodeModel.Geometry.UpdateHorizontally(
                    nodeModel.Grid.X * horizontalOffset,
                    dimensions.NodeWidth
                );

                var verticalOffset   = dimensions.NodeHeight + dimensions.EdgeHeight;
                nodeModel.Geometry.UpdateVertically(
                    nodeModel.Grid.Y * verticalOffset,
                    dimensions.NodeHeight
                );

            }

        }
        
    }

}
