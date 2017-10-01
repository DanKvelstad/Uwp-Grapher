using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using System;

namespace Grapher.ViewModels
{

    public class Anchor : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Point _HalfDuplex;
        private Point _SourceFullDuplex;
        private Point _TargetFullDuplex;

        public void Set(Point HalfDuplex, Point SourceFullDuplex, Point TargetFullDuplex)
        {

            _HalfDuplex         = HalfDuplex;
            _SourceFullDuplex   = SourceFullDuplex;
            _TargetFullDuplex   = TargetFullDuplex;

            if(SourceActivated)
            {
                SourceActivated = false;
                var Ignore = Source;
            }

            if (TargetActivated)
            {
                TargetActivated = false;
                var Ignore = Target;
            }
            
        }
        
        private bool  SourceActivated = false;
        private Point _Source;
        public Point Source
        {
            private set
            {
                _Source         = value;
                SourceActivated = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Source"));
            }
            get
            {
                if(!SourceActivated)
                {
                    if (TargetActivated)
                    {
                        Source = _SourceFullDuplex;
                        Target = _TargetFullDuplex;
                    }
                    else
                    {
                        Source = _HalfDuplex;
                    }
                }
                return _Source;
            }
        }

        private bool TargetActivated = false;
        private Point _Target;
        public  Point Target
        {
            private set
            {
                _Target         = value;
                TargetActivated = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Target"));
            }
            get
            {
                if(!TargetActivated)
                {
                    if (SourceActivated)
                    {
                        Target = _TargetFullDuplex;
                        Source = _SourceFullDuplex;
                    }
                    else
                    {
                        Target = _HalfDuplex;
                    }
                }
                return _Target;
            }
        }

    }

}
