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

namespace Grapher
{
    public sealed partial class Node : UserControl
    {

        public Node(String Name)
        {

            this.InitializeComponent();

            StateName.Text = Name;
            StateName.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            StateBorder.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Width   = StateBorder.DesiredSize.Width;
            Height  = StateBorder.DesiredSize.Height;
            
        }

        public Point Grid_Point;

        private Point _Center;
        public Point Center
        {
            get
            {
                return this._Center;
            }
            set
            {
                _Center            = value;
                AnchorLeft         = new Point(
                    _Center.X - Width / 2, 
                    _Center.Y                
                );
                AnchorTopLeft      = new Point(
                    _Center.X - Width  / 2 + StateBorder.CornerRadius.TopLeft * Math.Cos(Math.PI / 4),
                    _Center.Y - Height / 2 + StateBorder.CornerRadius.TopLeft * Math.Sin(Math.PI / 4)
                );
                AnchorTopCenter    = new Point(
                    _Center.X,
                    _Center.Y - Height / 2   
                );
                AnchorTopRight     = new Point(
                    _Center.X + Width  / 2 - StateBorder.CornerRadius.TopRight * Math.Cos(Math.PI / 4),
                    _Center.Y - Height / 2 + StateBorder.CornerRadius.TopRight * Math.Sin(Math.PI / 4)
                );
                AnchorRight        = new Point(
                    _Center.X + Width / 2, 
                    _Center.Y
                );
                AnchorBottomRight  = new Point(
                    _Center.X + Width  / 2 - StateBorder.CornerRadius.BottomRight * Math.Cos(Math.PI / 4),
                    _Center.Y + Height / 2 - StateBorder.CornerRadius.BottomRight * Math.Sin(Math.PI / 4)
                );
                AnchorBottomCenter = new Point(
                    _Center.X,
                    _Center.Y + Height / 2   
                );
                AnchorBottomLeft  = new Point(
                    _Center.X - Width  / 2 + StateBorder.CornerRadius.BottomLeft * Math.Cos(Math.PI / 4),
                    _Center.Y + Height / 2 - StateBorder.CornerRadius.BottomLeft * Math.Sin(Math.PI / 4)
                );
                
            }
        }

        internal int CompareTo(Node fromState)
        {
            return StateName.Text.CompareTo(fromState.StateName.Text);
        }

        public Point AnchorLeft;
        public Point AnchorTopLeft;
        public Point AnchorTopCenter;
        public Point AnchorTopRight;
        public Point AnchorRight;
        public Point AnchorBottomLeft;
        public Point AnchorBottomCenter;
        public Point AnchorBottomRight;

    }
}
