using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Grapher.Models
{

    public class GraphModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
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
        private string label;
        
        public ObservableCollection<NodeModel> NodeModels
        {
            get
            {
                if (null == nodeModels)
                {
                    NodeModels = new ObservableCollection<NodeModel>();
                }
                return nodeModels;
            }
            private set
            {

                if (null != nodeModels)
                {
                    nodeModels.CollectionChanged -= GridPlacementService.Nodes_CollectionChanged;
                    nodeModels.CollectionChanged -= GraphGridToPixelService.Nodes_CollectionChanged;
                    nodeModels.CollectionChanged -= Dimensions.Nodes_CollectionChanged;
                    Dimensions.Nodes              = null;
                }

                nodeModels = value;

                if (null != nodeModels)
                {
                    Dimensions.Nodes              = nodeModels;
                    nodeModels.CollectionChanged += Dimensions.Nodes_CollectionChanged;
                    nodeModels.CollectionChanged += GraphGridToPixelService.Nodes_CollectionChanged;
                    nodeModels.CollectionChanged += GridPlacementService.Nodes_CollectionChanged;
                }

            }
        }
        private ObservableCollection<NodeModel> nodeModels;

        public ObservableCollection<EdgeModel> EdgeModels
        {
            get
            {
                if (null == edgeModels)
                {
                    edgeModels = new ObservableCollection<EdgeModel>();
                }
                return edgeModels;
            }
            set
            {

                if (null != edgeModels)
                {
                    edgeModels.CollectionChanged -= Dimensions.Edges_CollectionChanged;
                    Dimensions.Edges              = null;
                }

                edgeModels = value;

                if (null != edgeModels)
                {
                    Dimensions.Edges              = edgeModels;
                    edgeModels.CollectionChanged += Dimensions.Edges_CollectionChanged;
                }

            }
        }
        private ObservableCollection<EdgeModel> edgeModels;

        public Services.GraphGridToPixelService GraphGridToPixelService
        {
            get
            {
                if (null == graphGridToPixelService)
                {
                    GraphGridToPixelService = new Services.GraphGridToPixelService();
                }
                return graphGridToPixelService;
            }
            set
            {
                if (null != graphGridToPixelService)
                {
                    throw new System.Exception();
                }
                graphGridToPixelService = value;
                graphGridToPixelService.Dimensions = Dimensions;
            }
        }
        private Services.GraphGridToPixelService graphGridToPixelService;

        public Services.SimpleGridPlacementService GridPlacementService
        {
            get
            {
                if (null == gridPlacementService)
                {
                    GridPlacementService = new Services.SimpleGridPlacementService();
                }
                return gridPlacementService;
            }
            set
            {
                if (null != gridPlacementService)
                {
                    throw new System.Exception();
                }
                gridPlacementService = value;
                gridPlacementService.Nodes = NodeModels;
            }
        }
        private Services.SimpleGridPlacementService gridPlacementService;

        public DimensionsModel Dimensions
        {
            get
            {
                if (null == dimensions)
                {
                    Dimensions = new DimensionsModel();
                }
                return dimensions;
            }
            set
            {

                if (null != dimensions)
                {

                    dimensions.Nodes              = null;
                    NodeModels.CollectionChanged -= dimensions.Nodes_CollectionChanged;

                    dimensions.Edges              = null;
                    EdgeModels.CollectionChanged -= dimensions.Edges_CollectionChanged;

                }

                dimensions = value;

                if (null != dimensions)
                {

                    dimensions.Nodes              = NodeModels;
                    NodeModels.CollectionChanged += dimensions.Nodes_CollectionChanged;

                    dimensions.Edges              = EdgeModels;
                    EdgeModels.CollectionChanged += dimensions.Edges_CollectionChanged;

                }

            }
        }
        private DimensionsModel dimensions;

    }

}
