using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Grapher.Models
{
    public class DimensionsModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<NodeModel> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
                // ToDo: what if its not empty?
            }
        }
        private IEnumerable<NodeModel> nodes;

        public void Nodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var nodeModel in e.NewItems)
                {
                    (nodeModel as NodeModel).PropertyChanged += Node_PropertyChanged;
                }
                Node_PropertyChanged(this, null);
            }
            if (null != e.OldItems)
            {
                foreach (var nodeModel in e.OldItems)
                {
                    (nodeModel as NodeModel).PropertyChanged -= Node_PropertyChanged;
                }
                Node_PropertyChanged(this, null);
            }
        }

        private void Node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool NodeDimensionsChanged = null == e?.PropertyName                        ||
                                         nameof(NodeModel.LocalWidth) == e.PropertyName ||
                                         nameof(NodeModel.LocalHeight) == e.PropertyName;
            if (!NodeDimensionsChanged)
            {
                return;
            }

            var GraphWidthNew  = double.MinValue;
            var GraphHeightNew = double.MinValue;
            var NodeWidthNew   = double.MinValue;
            var NodeHeightNew  = double.MinValue;

            foreach (var node in Nodes)
            {
                GraphWidthNew  = Math.Max(GraphWidthNew,  node.Geometry.Left );
                GraphHeightNew = Math.Max(GraphHeightNew, node.Geometry.Top  );
                NodeWidthNew   = Math.Max(NodeWidthNew,   node.LocalWidth    );
                NodeHeightNew  = Math.Max(NodeHeightNew,  node.LocalHeight   );
            }
            
            NodeWidth   = NodeWidthNew;
            NodeHeight  = NodeHeightNew;
            GraphWidth  = GraphWidthNew  + NodeWidth;
            GraphHeight = GraphHeightNew + NodeHeight;

        }

        public double NodeWidth
        {
            get
            {
                return nodeWidth;
            }
            set
            {
                if (value != nodeWidth)
                {
                    nodeWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NodeWidth))
                    );
                }
            }
        }
        private double nodeWidth;

        public double NodeHeight
        {
            get
            {
                return nodeHeight;
            }
            set
            {
                if (value != nodeHeight)
                {
                    nodeHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NodeHeight))
                    );
                }
            }
        }
        private double nodeHeight;

        public IEnumerable<EdgeModel> Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = value;
                // ToDo what if its not empty?
            }
        }
        private IEnumerable<EdgeModel> edges;

        public void Edges_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)
            {
                foreach (var EdgeModel in e.NewItems)
                {
                    (EdgeModel as EdgeModel).PropertyChanged += Edge_PropertyChanged;
                }
                Edge_PropertyChanged(this, null);
            }
            if (null != e.OldItems)
            {
                foreach (var EdgeModel in e.OldItems)
                {
                    (EdgeModel as EdgeModel).PropertyChanged -= Edge_PropertyChanged;
                }
                Edge_PropertyChanged(this, null);
            }
        }

        private void Edge_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool EdgeDimensionsChanged = null == e?.PropertyName                         ||
                                         nameof(EdgeModel.LocalWidth)  == e.PropertyName ||
                                         nameof(EdgeModel.LocalHeight) == e.PropertyName;
            if(!EdgeDimensionsChanged)
            {
                return;
            }

            var EdgeWidthNew   = double.MinValue;
            var EdgeHeightNew  = double.MinValue;

            foreach (var edge in Edges)
            {
                EdgeWidthNew   = Math.Max(EdgeWidthNew,   edge.LocalWidth);
                EdgeHeightNew  = Math.Max(EdgeHeightNew,  edge.LocalHeight);
            }

            EdgeWidth  = EdgeWidthNew;
            EdgeHeight = EdgeHeightNew;

        }

        public double EdgeWidth
        {
            get
            {
                return edgeWidth;
            }
            set
            {
                if (value != edgeWidth)
                {
                    edgeWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EdgeWidth))
                    );
                }
            }
        }
        private double edgeWidth;

        public double EdgeHeight
        {
            get
            {
                return edgeHeight;
            }
            set
            {
                if (value != edgeHeight)
                {
                    edgeHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EdgeHeight))
                    );
                }
            }
        }
        private double edgeHeight;

        public double GraphWidth
        {
            get
            {
                return graphWidth;
            }
            set
            {
                if (value != graphWidth)
                {
                    graphWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(GraphWidth))
                    );
                }
            }
        }
        private double graphWidth;

        public double GraphHeight
        {
            get
            {
                return graphHeight;
            }
            set
            {
                if (value != graphHeight)
                {
                    graphHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(GraphHeight))
                    );
                }
            }
        }
        private double graphHeight;

    }
}
