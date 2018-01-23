using Grapher.Algorithms;
using System.Collections.Generic;
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
                label = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Label))
                );
            }
        }
        private string label;

        public double CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                cornerRadius = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(CornerRadius))
                );
            }
        }
        private double cornerRadius = 10;

        public double MinWidth
        {
            get
            {
                return minWidth;
            }
            set
            {
                minWidth = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(MinWidth))
                );
            }
        }
        private double minWidth;

        public double MinHeight
        {
            get
            {
                return minHeight;
            }
            set
            {
                minHeight = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(MinHeight))
                );
            }
        }
        private double minHeight;

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Width))
                );
            }
        }
        private double width = double.NaN;

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Height))
                );
            }
        }
        private double height = double.NaN;

        public Point Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Center))
                );
            }
        }
        private Point center;
        
        public List<Point> Grid
        {
            get
            {
                if (null == grid)
                {
                    grid = new List<Point>();
                }
                return grid;
            }
            set
            {
                grid = value;
            }
        }
        private List<Point> grid;

        public int GridIndex
        {
            get
            {
                return gridIndex;
            }
            set
            {
                gridIndex = value;
                Center = new Point(
                    Grid[gridIndex].X * ( Width * 2),
                    Grid[gridIndex].Y * (Height * 2)
                );
            }
        }
        private int gridIndex;
        
        public NodeAnchorsModel Anchors
        {
            get
            {
                if(null==anchors)
                {
                    anchors = new NodeAnchorsModel();
                    PropertyChanged += anchors.NodeModel_PropertyChanged;
                }
                return anchors;
            }
        }
        private NodeAnchorsModel anchors;

    }

}
