﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Grapher.Views.DataTemplates
{

    public class GraphStageSelector : DataTemplateSelector
    {

        public DataTemplate Stage1Template
        {
            get;
            set;
        }

        public DataTemplate Stage2Template
        {
            get;
            set;
        }

        public DataTemplate Stage3Template
        {
            get;
            set;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item is ViewModels.Graphs.Stage1GraphViewModel)
            {
                return Stage1Template;
            }
            else if (item is ViewModels.Graphs.Stage2GraphViewModel)
            {
                return Stage2Template;
            }
            else if (item is ViewModels.Graphs.Stage3GraphViewModel)
            {
                return Stage3Template;
            }
            else
            {
                return base.SelectTemplateCore(item, container);
            }

        }
    }

}
