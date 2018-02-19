using Grapher.Algorithms;
using System.ComponentModel;

namespace Grapher.Models
{
    public class NodeAnchorModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
        public Pixel Source
        {
            get
            {
                if (null == target)
                {
                    Source = SimplexCoordinate;
                }
                else
                {
                    Source = DuplexSourceCoordinate;
                    Target = DuplexTargetCoordinate;
                }
                return source;
            }
            private set
            {
                if (value != source)
                {
                    source = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(Source))
                    );
                }
            }
        }
        private Pixel source;

        public Pixel Target
        {
            get
            {
                if (null == source)
                {
                    Target = SimplexCoordinate;
                }
                else
                {
                    Source = DuplexSourceCoordinate;
                    Target = DuplexTargetCoordinate;
                }
                return target;
            }
            private set
            {
                if (value != target)
                {
                    target = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(Target))
                    );
                }
            }
        }
        private Pixel target;

        public Pixel SimplexCoordinate
        {
            private get;
            set;
        }

        public Pixel DuplexSourceCoordinate
        {
            private get;
            set;
        }

        public Pixel DuplexTargetCoordinate
        {
            private get;
            set;
        }

    }
}
