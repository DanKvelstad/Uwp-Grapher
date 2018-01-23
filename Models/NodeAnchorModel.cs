using Grapher.Algorithms;
using System.Collections.Generic;
using System.ComponentModel;

namespace Grapher.Models
{
    public class NodeAnchorModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void ObserveAsSource(object who)
        {
            SourceObservers.Add(who);
            if (0 < TargetObservers.Count)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(null)
                );
            }
        }
        public void IgnoreAsSource(object who)
        {
            SourceObservers.Remove(who);
            if(0>=SourceObservers.Count)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(null)
                );
            }
        }

        public void ObserveAsTarget(object who)
        {
            TargetObservers.Add(who);
            if (0 < SourceObservers.Count)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(null)
                );
            }
        }
        public void IgnoreAsTarget(object who)
        {
            TargetObservers.Remove(who);
            if (0 >= TargetObservers.Count)
            {
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(null)
                );
            }
        }

        private List<object> SourceObservers;
        private List<object> TargetObservers;

        public Point Source
        {
            get
            {
                bool source = 0 < SourceObservers.Count;
                bool target = 0 < TargetObservers.Count;
                if (source && target)
                {
                    return DuplexSourceCoordinate;
                }
                else if (source)
                {
                    return SimplexCoordinate;
                }
                else if (target)
                {
                    throw new System.Exception();
                }
                else
                {
                    throw new System.Exception();
                }
            }
        }
        public Point Target
        {
            get
            {
                bool source = 0 < SourceObservers.Count;
                bool target = 0 < TargetObservers.Count;
                if (source && target)
                {
                    return DuplexTargetCoordinate;
                }
                else if (source)
                {
                    throw new System.Exception();
                }
                else if (target)
                {
                    return SimplexCoordinate;
                }
                else
                {
                    throw new System.Exception();
                }
            }
        }

        public void NewCoordinates(Point Simplex, Point DuplexSource, Point DuplexTarget)
        {
            SimplexCoordinate      = Simplex;
            DuplexSourceCoordinate = DuplexSource;
            DuplexTargetCoordinate = DuplexTarget;
        }

        public Point SimplexCoordinate
        {
            get
            {
                return simplexCoordinate;
            }
            private set
            {
                simplexCoordinate = value;
                if (0 < SourceObservers.Count && 0 >= TargetObservers.Count)
                {
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Source))
                    );
                }
                else if (0 >= SourceObservers.Count && 0 < TargetObservers.Count)
                {
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Target))
                    );
                }
            }
        }
        private Point simplexCoordinate;

        public Point DuplexSourceCoordinate
        {
            get
            {
                return duplexSourceCoordinate;
            }
            private set
            {
                duplexSourceCoordinate = value;
                if (0 < SourceObservers.Count && 0 < TargetObservers.Count)
                {
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Source))
                    );
                }
            }
        }
        private Point duplexSourceCoordinate;

        public Point DuplexTargetCoordinate
        {
            get
            {
                return duplexTargetCoordinate;
            }
            private set
            {
                duplexTargetCoordinate = value;
                if (0 < SourceObservers.Count && 0 < TargetObservers.Count)
                {
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Target))
                    );
                }
            }
        }
        private Point duplexTargetCoordinate;

    }
}
