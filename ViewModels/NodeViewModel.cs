using Grapher.Models;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Grapher.ViewModels
{

    public class NodeViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public NodeModel Model
        {
            get
            {
                return model;
            }
            set
            {
                if (null != model)
                {
                    model.PropertyChanged -= Model_PropertyChanged;
                }
                model = value;
                if (null != model)
                {
                    model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }
        private NodeModel model;

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(NodeModel.Center):
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Left))
                    );
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Top))
                    );
                    break;
                default:
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(e.PropertyName)
                    );
                    break;
            }
        }
        
        public string Label
        {
            get
            {
                return Model.Label;
            }
            set
            {
                Model.Label = value;
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return new CornerRadius(Model.CornerRadius);
            }
        }

        public double MinWidth
        {
            get
            {
                return Model.MinWidth;
            }
            set
            {
                Model.MinWidth = value;
            }
        }

        public double MinHeight
        {
            get
            {
                return Model.MinHeight;
            }
            set
            {
                Model.MinHeight = value;
            }
        }

        public double Width
        {
            get
            {
                return Model.Width;
            }
            set
            {
                Model.Width = value;
            }
        }

        public double Height
        {
            get
            {
                return Model.Height;
            }
            set
            {
                Model.Height = value;
            }
        }

        public double Left
        {
            get
            {
                //return Model.Center.X - Model.Width / 2;
                return left;
            }
            set
            {
                left = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Left))
                );
            }
        }
        private double left;

        public double Top
        {
            get
            {
                //return Model.Center.Y - Model.Height / 2;
                return top;
            }
            set
            {
                top = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Top))
                );
            }
        }
        private double top;
        
    }

}
