using Grapher.Models;
using System;
using System.ComponentModel;

namespace Grapher.ViewModels
{

    public class EdgeViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public EdgeModel Model
        {
            get
            {
                return model;
            }
            set
            {
                if(null != model)
                {
                    model.PropertyChanged -= Model_PropertyChanged;
                }
                model = value;
                if(null != model)
                {
                    model.PropertyChanged += Model_PropertyChanged;
                }
                PropertyChanged?.Invoke(this, null);
            }
        }
        public EdgeModel model;

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(null)
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

        public double Width
        {
            get
            {
                return Model.Dimensions.EdgeWidth;
            }
            private set
            {
                Model.LocalWidth = value;
            }
        }

        public double Height
        {
            get
            {
                return Model.Dimensions.EdgeHeight;
            }
            private set
            {
                Model.LocalHeight = value;
            }
        }

        public double SourceX
        {
            get
            {
                return Model.Source.X;
            }
        }
        public double SourceY
        {
            get
            {
                return Model.Source.Y;
            }
        }

        public  double TargetX
        {
            get
            {
                return Model.Target.X;
            }
        }
        public  double TargetY
        {
            get
            {
                return Model.Target.Y;
            }
        }
        
        public  double ArrowAlphaX
        {
            get
            {
                return Model.ArrowAlpha.X;
            }
        }
        public  double ArrowAlphaY
        {
            get
            {
                return Model.ArrowAlpha.Y;
            }
        }
        
        public  double ArrowBravoX
        {
            get
            {
                return Model.ArrowBravo.X;
            }
        }
        public  double ArrowBravoY
        {
            get
            {
                return Model.ArrowBravo.Y;
            }
        }
        
        public double Angle
        {
            get
            {
                return Model.AngleInRadians;
            }
        }

        public double LabelLeft
        {
            get
            {
                var Center = Math.Min(SourceX, TargetX);
                Center += Math.Abs(SourceX - TargetX) / 2;
                Center -= LabelWidth / 2;
                var Offset = LabelHeight / 2 * Math.Sin(Angle);
                return Center + Offset;
            }
        }

        public double LabelTop
        {
            get
            {
                var Center = Math.Min(SourceY, TargetY);
                Center += Math.Abs(SourceY - TargetY) / 2;
                Center -= LabelHeight / 2;
                var Offset = LabelHeight / 2 * Math.Cos(Angle);
                return Center - Offset;
            }
        }

        public  double LabelWidth
        {
            get
            {
                return Model.LabelWidth;
            }
            set
            {
                Model.LabelWidth = value;
            }
        }

        public double LabelHeight
        {
            get
            {
                return Model.LabelHeight;
            }
            set
            {
                Model.LabelHeight = value;
            }
        }

        public double EndpointRadius
        {
            get
            {
                return Model.EndpointRadius;
            }
        }
                
    }

}
