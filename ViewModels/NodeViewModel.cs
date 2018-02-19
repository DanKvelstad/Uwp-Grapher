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
                    model.Geometry.PropertyChanged -= NodeGeometry_PropertyChanged;
                    model.PropertyChanged          -= Model_PropertyChanged;
                }

                model = value;

                if (null != model)
                {
                    model.PropertyChanged          += Model_PropertyChanged;
                    model.Geometry.PropertyChanged += NodeGeometry_PropertyChanged;
                }

            }
        }
        private NodeModel model;

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(NodeModel.Label):
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Label))
                    );
                    break;
            }
        }

        private void NodeGeometry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(Left))
            );
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(Top))
            );
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(Width))
            );
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(Height))
            );
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(CornerRadius))
            );
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
                return new CornerRadius(Model.Geometry.CornerRadius);
            }
        }

        public double Width
        {
            get
            {
                return Model.Geometry.Right - Model.Geometry.Left;
            }
            set
            {
                Model.LocalWidth = value;
            }
        }

        public double Height
        {
            get
            {
                return Model.Geometry.Bottom - Model.Geometry.Top;
            }
            set
            {
                Model.LocalHeight = value;
            }
        }

        public double Left
        {
            get
            {
                return Model.Geometry.Left;
            }
        }

        public double Top
        {
            get
            {
                return Model.Geometry.Top;
            }
        }

    }

}
