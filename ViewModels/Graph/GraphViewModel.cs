//using Grapher.Models;
//using System.ComponentModel;

//namespace Grapher.ViewModels.Graphs
//{

//    public class GraphViewModel : INotifyPropertyChanged
//    {

//        public event PropertyChangedEventHandler PropertyChanged;

//        public GraphViewModel(GraphModel model)
//        {
//            Model = model;
//            stage = Stages.Stage0;
//        }
//        public GraphViewModel(GraphViewModel other)
//        {
//            model  = other.model;
//            height = other.Height;
//            width  = other.Width;
//        }

//        protected void OnPropertyChanged(string PropertyName)
//        {
//            PropertyChanged?.Invoke(
//                this, 
//                new PropertyChangedEventArgs(PropertyName)
//            );
//        }

//        public enum Stages
//        {
//            Stage0,
//            Stage1,
//            Stage2,
//            Stage3
//        }
//        public Stages Stage
//        {
//            get
//            {
//                return stage;
//            }
//            protected set
//            {
//                stage = value;
//                PropertyChanged?.Invoke(
//                    this,
//                    new PropertyChangedEventArgs(nameof(Stage))
//                );
//            }
//        }
//        protected Stages stage;

//        public string Label
//        {
//            get
//            {
//                return Model.Label;
//            }
//        }

//        public double Width
//        {
//            get
//            {
//                return width;
//            }
//            set
//            {
//                width = value;
//                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
//            }
//        }
//        private double width = 500;

//        public double Height
//        {
//            get
//            {
//                return height;
//            }
//            set
//            {
//                height = value;
//                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
//            }
//        }
//        public double height = 250;

//        public GraphModel Model
//        {
//            get
//            {
//                return model;
//            }
//            protected set
//            {
//                model = value;
//            }
//        }
//        private GraphModel model;

//        public virtual async void Process()
//        {
//            Stage = Stages.Stage1;
//        }

//    }

//}
