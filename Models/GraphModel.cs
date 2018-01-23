using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Grapher.Models
{

    public class GraphModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Debug { get; set; }

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Label))
                );
            }
        }
        private string label = "Anonymous";

        public double Width
        {
            get
            {
                return width;
            }
        }
        public double width = 200;

        public double Height
        {
            get
            {
                return height;
            }
        }
        private double height = 200;

        public ObservableCollection<NodeModel> Nodes
        {
            get
            {
                if (null == nodes)
                {
                    Nodes = new ObservableCollection<NodeModel>();
                }
                return nodes;
            }
            private set
            {
                nodes                 = value;
                NodesDimensions.Nodes = nodes;
            }
        }
        private ObservableCollection<NodeModel> nodes;

        private NodesDimensionsModel NodesDimensions
        {
            get
            {
                if(null == nodesDimensions)
                {
                    nodesDimensions = new NodesDimensionsModel();
                }
                return nodesDimensions;
            }
        }
        private NodesDimensionsModel nodesDimensions;

        public ObservableCollection<EdgeModel> Edges
        {
            get
            {
                if (null == edges)
                {
                    edges = new ObservableCollection<EdgeModel>();
                }
                return edges;
            }
        }
        private ObservableCollection<EdgeModel> edges;

    }

}
