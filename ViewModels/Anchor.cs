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
        private async void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () => handler(this, new PropertyChangedEventArgs(info))
                );
            }
        }

        private Point _HalfDuplex;
        private Point _SourceFullDuplex;
        private Point _TargetFullDuplex;

        public void Set(Point HalfDuplex, Point SourceFullDuplex, Point TargetFullDuplex)
        {

            _HalfDuplex         = HalfDuplex;
            _SourceFullDuplex   = SourceFullDuplex;
            _TargetFullDuplex   = TargetFullDuplex;

            if (SourceActivated && TargetActivated)
            {
                Source = _SourceFullDuplex;
                Target = _TargetFullDuplex;
            }
            else if(SourceActivated)
            {
                Source = _HalfDuplex;
            }
            else if (TargetActivated)
            {
                Target = _HalfDuplex;
            }
            // else neither

        }
        
        private bool  SourceActivated = false;
        private Point _Source;
        public Point Source
        {
            private set
            {
                if(_Source != value)
                {
                    _Source = value;
                    SourceActivated = true;
                    OnPropertyChanged("Source");
                }
            }
            get
            {
                if (TargetActivated)
                {
                    Source = _SourceFullDuplex;
                    Target = _TargetFullDuplex;
                    return _Source;
                }
                else
                {
                    Source = _HalfDuplex;
                    return _Source;
                }
            }
        }

        private bool TargetActivated = false;
        private Point _Target;
        public  Point Target
        {
            private set
            {
                if(_Target != value)
                {
                    _Target = value;
                    TargetActivated = true;
                    OnPropertyChanged("Target");
                }
            }
            get
            {
                if(SourceActivated)
                {
                    Source = _SourceFullDuplex;
                    Target = _TargetFullDuplex;
                    return _Target;
                }
                else
                {
                    Target = _HalfDuplex;
                    return _Target;
                }
            }
        }

    }

}
