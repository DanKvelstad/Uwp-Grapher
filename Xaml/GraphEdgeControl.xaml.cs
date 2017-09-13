using System;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Grapher
{

    public sealed partial class GraphEdgeControl : UserControl
    {

        public GraphEdgeControl(GraphNodeControl from, GraphNodeControl to, String label)
        {
            this.InitializeComponent();
        }

    }

}
