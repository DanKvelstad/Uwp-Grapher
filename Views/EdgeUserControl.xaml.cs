using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Grapher.Views
{
    public sealed partial class EdgeUserControl : UserControl
    {

        private EdgeViewModel viewModel;

        public EdgeUserControl(EdgeViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.InitializeComponent();
        }

    }
}
