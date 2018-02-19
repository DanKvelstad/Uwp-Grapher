using System.Collections.ObjectModel;

namespace Grapher.Models
{

    public class MainModel
    {

        public ObservableCollection<GraphModel> GraphModels
        {
            get
            {
                if(null==graphModels)
                {
                    graphModels = new ObservableCollection<GraphModel>();
                }
                return graphModels;
            }
            set
            {
                graphModels = value;
            }
        }
        private ObservableCollection<GraphModel> graphModels;
        
    }

}
