using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Grapher.Models
{

    class NodesDimensionsModel
    {
        
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if(double.IsNaN(value))
                {
                    double newWidth = 0;
                    foreach (var nodeModel in nodes)
                    {
                        newWidth = Math.Max(newWidth, nodeModel.MinWidth);
                    }
                    if(newWidth != width)
                    {
                        Width = newWidth;
                    }
                }
                else
                {
                    width = value;
                    foreach (var nodeModel in nodes)
                    {
                        nodeModel.Width = width;
                    }
                }
            }
        }
        private double width;

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (double.IsNaN(value))
                {
                    double newHeight = 0;
                    foreach (var nodeModel in nodes)
                    {
                        newHeight = Math.Max(newHeight, nodeModel.MinHeight);
                    }
                    if (newHeight != height)
                    {
                        Height = newHeight;
                    }
                }
                else
                {
                    height = value;
                    foreach (var nodeModel in nodes)
                    {
                        nodeModel.Height = height;
                    }
                }
            }
        }
        private double height;

        public ObservableCollection<NodeModel> Nodes
        {
            set
            {

                if (null != nodes)
                {
                    nodes.CollectionChanged -= Nodes_CollectionChanged;
                }

                nodes = value;

                if (null != nodes)
                {
                    nodes.CollectionChanged += Nodes_CollectionChanged;
                }

            }
        }
        private ObservableCollection<NodeModel> nodes;

        private void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (null != e.OldItems)
            {
                foreach (var nodeModel in e.OldItems)
                {
                    (nodeModel as NodeModel).PropertyChanged -= Node_PropertyChanged;
                }
            }
            if (null != e.NewItems)
            {
                foreach (var nodeModel in e.NewItems)
                {
                    (nodeModel as NodeModel).PropertyChanged += Node_PropertyChanged;
                }
            }
        }

        private void Node_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(NodeModel.MinWidth):
                    Width = double.NaN;
                    break;
                case nameof(NodeModel.MinHeight):
                    Height = double.NaN;
                    break;
            }
        }

    }

}
