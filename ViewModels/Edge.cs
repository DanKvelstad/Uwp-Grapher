using Grapher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace Grapher.ViewModels
{

    public class Edge : INotifyPropertyChanged, IDisposable
    {

        EdgeModel Model;

        public Edge(EdgeModel Model, Node Source, Node Target)
        {

            this.Model = Model;

            this.SourceNode = Source;
            this.TargetNode = Target;
            this.Label      = Model.Label;

            SourceNode.PropertyChanged += NodeChanged;
            TargetNode.PropertyChanged += NodeChanged;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Node   SourceNode;
        private Anchor SourceAnchor;

        private Node   TargetNode;
        private Anchor TargetAnchor;

        public  double SourceX
        {
            get
            {
                return Model.SourceX;
            }
            private set
            {
                Model.SourceX = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceX"));
                Angle = double.NaN;
            }
        }
        public  double SourceActualX
        {
            get
            {
                if(null==SourceAnchor)
                {
                    return 0;
                }
                else
                {
                    return SourceAnchor.Source.X;
                }
            }
        }

        public  double SourceY
        {
            get
            {
                return Model.SourceY;
            }
            private set
            {
                Model.SourceY = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceY"));
                Angle = double.NaN;
            }
        }
        public  double SourceActualY
        {
            get
            {
                if (null == SourceAnchor)
                {
                    return 0;
                }
                else
                {
                    return SourceAnchor.Source.Y;
                }
            }
        }

        public  double TargetX
        {
            get
            {
                return Model.TargetX;
            }
            private set
            {
                if(value!=Model.TargetX)
                {
                    Model.TargetX = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetX"));
                    Angle = double.NaN;
                }
            }
        }
        public  double TargetActualX
        {
            get
            {
                if (null == TargetAnchor)
                {
                    return 1;
                }
                else
                {
                    return TargetAnchor.Target.X;
                }
            }
        }

        public  double TargetY
        {
            get
            {
                return Model.TargetY;
            }
            private set
            {
                if (value != Model.TargetY)
                {
                    Model.TargetY = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetY"));
                    Angle = double.NaN;
                }
            }
        }
        public  double TargetActualY
        {
            get
            {
                if (null == SourceAnchor)
                {
                    return 1;
                }
                else
                {
                    return TargetAnchor.Target.Y;
                }
            }
        }

        public  double Angle
        {
            get
            {
                return Model.Angle;
            }
            private set
            {

                if(null==SourceAnchor || null==TargetAnchor)
                {
                    return;
                }
                else if(double.IsNaN(value))
                {

                    Model.Angle = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );
                    if(0>Model.Angle)
                    {
                        Model.Angle += 2 * Math.PI;
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Angle"));

                    LabelLeft   = double.NaN;
                    LabelTop    = double.NaN;
                    ArrowAlphaX = double.NaN;
                    ArrowAlphaY = double.NaN;
                    ArrowBravoX = double.NaN;
                    ArrowBravoY = double.NaN;
                    MinWidth    = double.NaN;
                    MinHeight   = double.NaN;

                }
                else
                {
                    throw new ArgumentException();
                }

            }
        }

        public  double MinWidth
        {
            private set
            {
                if (double.IsNaN(value))
                {
                    Model.MinWidth  = (LabelWidth + 4 * EndpointRadius) * Math.Cos(Angle);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinWidth"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return Model.MinWidth;
            }
        }

        public  double MinHeight
        {
            private set
            {
                if (double.IsNaN(value))
                {
                    Model.MinHeight = (LabelHeight + 4 * EndpointRadius) * Math.Sin(Angle);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MinHeight"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return Model.MinHeight;
            }
        }

        public  double ArrowAlphaX
        {
            get
            {
                return Model.ArrowAlphaX;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    Model.ArrowAlphaX = TargetX + ArrowLenght * Math.Cos(Angle - Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowAlphaX"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public  double ArrowAlphaY
        {
            get
            {
                return Model.ArrowAlphaY;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    Model.ArrowAlphaY = TargetY + ArrowLenght * Math.Sin(Angle - Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowAlphaY"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public  double ArrowBravoX
        {
            get
            {
                return Model.ArrowBravoX;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    Model.ArrowBravoX = TargetX + ArrowLenght * Math.Cos(Angle + Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowBravoX"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        public  double ArrowBravoY
        {
            get
            {
                return Model.ArrowBravoY;
            }
            private set
            {
                if (double.IsNaN(value))
                {
                    var ArrowLenght = Math.Sqrt((EndpointRadius * EndpointRadius) + (1 / 4) * (EndpointRadius * 2) * (EndpointRadius * 2));
                    Model.ArrowBravoY = TargetY + ArrowLenght * Math.Sin(Angle + Math.PI / 4);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrowBravoY"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public  string Label
        {
            get
            {
                return Model.Label;
            }
            set
            {
                if (Model.Label != value)
                {
                    Model.Label = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
                }
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
                if (Model.LabelWidth != value)
                {
                    Model.LabelWidth = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelWidth"));
                    LabelLeft = double.NaN;
                    MinWidth  = double.NaN;
                    MinHeight = double.NaN;
                }
            }
        }

        public  double LabelHeight
        {
            get
            {
                return Model.LabelHeight;
            }
            set
            {
                if (Model.LabelHeight != value)
                {
                    Model.LabelHeight = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelHeight"));
                    LabelTop  = double.NaN;
                    MinWidth  = double.NaN;
                    MinHeight = double.NaN;
                }
            }
        }

        public  double LabelLeft
        {
            get
            {
                return Model.LabelLeft;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var Center  = Math.Min(SourceX, TargetX);
                    Center     += Math.Abs(SourceX - TargetX) / 2;
                    Center     -= LabelWidth / 2;
                    var Offset  = LabelHeight / 2 * Math.Sin(Angle);
                    Model.LabelLeft = Center + Offset;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelLeft"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public  double LabelTop
        {
            get
            {
                return Model.LabelTop;
            }
            private set
            {
                if(double.IsNaN(value))
                {
                    var Center  = Math.Min(SourceY, TargetY);
                    Center     += Math.Abs(SourceY - TargetY) / 2;
                    Center     -= LabelHeight / 2;
                    var Offset  = LabelHeight / 2 * Math.Cos(Angle);
                    Model.LabelTop   = Center - Offset;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelTop"));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public  double EndpointRadius
        {
            get
            {
                return Model.EndpointRadius;
            }
            set
            {
                Model.EndpointRadius = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EndpointRadius"));
                MinHeight = double.NaN;
                MinWidth  = double.NaN;
            }
        }

        public void Dispose()
        {
            SourceNode.PropertyChanged -= NodeChanged;
            TargetNode.PropertyChanged -= NodeChanged;
            // ToDo think about the anchor callbacks!
        }

        private void NodeChanged(object sender, PropertyChangedEventArgs e)
        {
            
            if("Left"==e.PropertyName || "Top" == e.PropertyName)
            {

                if(null != SourceAnchor)
                {
                    SourceAnchor.PropertyChanged -= SourceAnchorChanged;
                }
                SourceAnchor = SourceNode.GetAnchorRelativeTo(TargetNode);
                SourceAnchor.PropertyChanged += SourceAnchorChanged;
                SourceAnchorChanged(SourceAnchor, null);

                if (null != TargetAnchor)
                {
                    TargetAnchor.PropertyChanged -= TargetAnchorChanged;
                }
                TargetAnchor = TargetNode.GetAnchorRelativeTo(SourceNode);
                TargetAnchor.PropertyChanged += TargetAnchorChanged;
                TargetAnchorChanged(TargetAnchor, null);

            }
            
        }

        private void SourceAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            var Anchor = sender as Anchor;
            if(null != Anchor && Anchor == SourceAnchor)
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceActualX"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceActualY"));

                if(null!=TargetAnchor)
                {
                    var BaselineAngleInRadians = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );
                    SourceX = SourceActualX + 2 * EndpointRadius * Math.Cos(BaselineAngleInRadians);
                    SourceY = SourceActualY + 2 * EndpointRadius * Math.Sin(BaselineAngleInRadians);
                }

            }
            else
            {
                throw new ArgumentException();
            }

        }
        
        private void TargetAnchorChanged(object sender, PropertyChangedEventArgs e)
        {

            var Anchor = sender as Anchor;
            if (null != Anchor && Anchor == TargetAnchor)
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetActualX"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetActualY"));

                if(null!=SourceAnchor)
                {
                    
                    var BaselineAngleInRadians = Math.Atan2(
                        TargetActualY - SourceActualY,
                        TargetActualX - SourceActualX
                    );

                    TargetX = TargetActualX - 2 * EndpointRadius * Math.Cos(BaselineAngleInRadians);
                    TargetY = TargetActualY - 2 * EndpointRadius * Math.Sin(BaselineAngleInRadians);

                }

            }
            else
            {
                throw new ArgumentException();
            }

        }
        
    }

}
