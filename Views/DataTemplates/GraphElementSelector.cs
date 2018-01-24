using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher.Views.DataTemplates
{
    public class GraphElementSelector : DataTemplateSelector
    {

        public DataTemplate NodeTemplate
        {
            get;
            set;
        }

        public DataTemplate EdgeTemplate
        {
            get;
            set;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item is ViewModels.NodeViewModel)
            {
                return NodeTemplate;
            }
            else if (item is ViewModels.EdgeViewModel)
            {
                return EdgeTemplate;
            }
            else
            {
                return base.SelectTemplateCore(item, container);
            }

        }
    }

}
