using System.ComponentModel;

namespace Grapher.Models
{

    public class NodeModel : INotifyPropertyChanged
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
                if (value != label)
                {
                    label = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Label))
                    );
                }
            }
        }
        private string label = "";

        public double LocalWidth
        {
            get
            {
                return localWidth;
            }
            set
            {
                if (value != localWidth)
                {
                    localWidth = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LocalWidth))
                    );
                }
            }
        }
        private double localWidth;

        public double LocalHeight
        {
            get
            {
                return localHeight;
            }
            set
            {
                if (value != localHeight)
                {
                    localHeight = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(LocalHeight))
                    );
                }
            }
        }
        private double localHeight;
        
        public Point Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Grid))
                );
            }
        }
        private Point grid;

        public NodeGeometryModel Geometry
        {
            get
            {
                if(null == geometry)
                {
                    geometry = new NodeGeometryModel();
                }
                return geometry;
            }
        }
        private NodeGeometryModel geometry;

    }

}
