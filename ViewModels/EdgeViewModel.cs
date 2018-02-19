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
            switch (e.PropertyName)
            {
                case nameof(EdgeModel.Source):
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(SourceX))
                    );
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SourceY))
                    );
                    break;
                case nameof(EdgeModel.Target):
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TargetX))
                    );
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TargetY))
                    );
                    break;
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
        
        //public  double ArrowAlphaX
        //{
        //    get
        //    {
        //        return Model.ArrowAlpha.X;
        //    }
        //}
        //public  double ArrowAlphaY
        //{
        //    get
        //    {
        //        return Model.ArrowAlpha.Y;
        //    }
        //}
        //
        //public  double ArrowBravoX
        //{
        //    get
        //    {
        //        return Model.ArrowBravo.X;
        //    }
        //}
        //public  double ArrowBravoY
        //{
        //    get
        //    {
        //        return Model.ArrowBravo.Y;
        //    }
        //}
        //
        //public double Angle
        //{
        //    get
        //    {
        //        return Model.AngleInRadians;
        //    }
        //}
        //
        //public double MinWidth
        //{
        //    private set
        //    {
        //        if (double.IsNaN(value))
        //        {
        //            Model.MinWidth = (LabelWidth + 4 * EndpointRadius) * Math.Cos(Angle);
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinWidth"));
        //        }
        //        else
        //        {
        //            throw new ArgumentException();
        //        }
        //    }
        //    get
        //    {
        //        return Model.MinWidth;
        //    }
        //}
        //
        //public double MinHeight
        //{
        //    private set
        //    {
        //        if (double.IsNaN(value))
        //        {
        //            Model.MinHeight = (LabelHeight + 4 * EndpointRadius) * Math.Sin(Angle);
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinHeight"));
        //        }
        //        else
        //        {
        //            throw new ArgumentException();
        //        }
        //    }
        //    get
        //    {
        //        return Model.MinHeight;
        //    }
        //}
        //
        //public string Label
        //{
        //    get
        //    {
        //        return Model.Label;
        //    }
        //    set
        //    {
        //        if (Model.Label != value)
        //        {
        //            Model.Label = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
        //        }
        //    }
        //}
        //
        //public  double LabelWidth
        //{
        //    get
        //    {
        //        return Model.LabelWidth;
        //    }
        //    set
        //    {
        //        if (Model.LabelWidth != value)
        //        {
        //            Model.LabelWidth = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelWidth"));
        //            LabelLeft = double.NaN;
        //            MinWidth  = double.NaN;
        //            MinHeight = double.NaN;
        //        }
        //    }
        //}
        //
        //public  double LabelHeight
        //{
        //    get
        //    {
        //        return Model.LabelHeight;
        //    }
        //    set
        //    {
        //        if (Model.LabelHeight != value)
        //        {
        //            Model.LabelHeight = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelHeight"));
        //            LabelTop  = double.NaN;
        //            MinWidth  = double.NaN;
        //            MinHeight = double.NaN;
        //        }
        //    }
        //}
        //
        //public  double LabelLeft
        //{
        //    get
        //    {
        //        return Model.LabelLeft;
        //    }
        //    private set
        //    {
        //        if(double.IsNaN(value))
        //        {
        //            var Center  = Math.Min(SourceX, TargetX);
        //            Center     += Math.Abs(SourceX - TargetX) / 2;
        //            Center     -= LabelWidth / 2;
        //            var Offset  = LabelHeight / 2 * Math.Sin(Angle);
        //            Model.LabelLeft = Center + Offset;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelLeft"));
        //        }
        //        else
        //        {
        //            throw new ArgumentException();
        //        }
        //    }
        //}
        //
        //public  double LabelTop
        //{
        //    get
        //    {
        //        return Model.LabelTop;
        //    }
        //    private set
        //    {
        //        if(double.IsNaN(value))
        //        {
        //            var Center  = Math.Min(SourceY, TargetY);
        //            Center     += Math.Abs(SourceY - TargetY) / 2;
        //            Center     -= LabelHeight / 2;
        //            var Offset  = LabelHeight / 2 * Math.Cos(Angle);
        //            Model.LabelTop   = Center - Offset;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelTop"));
        //        }
        //        else
        //        {
        //            throw new ArgumentException();
        //        }
        //    }
        //}
        //
        //public  double EndpointRadius
        //{
        //    get
        //    {
        //        return Model.EndpointRadius;
        //    }
        //    set
        //    {
        //        Model.EndpointRadius = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EndpointRadius"));
        //        MinHeight = double.NaN;
        //        MinWidth  = double.NaN;
        //    }
        //}
                
    }

}
