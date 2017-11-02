using Grapher.ViewModels;

namespace Grapher.Models
{

    public class NodeModel
    {

        public string Label;
        public double CornerRadius;
        public double MinWidth;
        public double Width;
        public double MinHeight;
        public double Height;
        public double Left;
        public double Top;
        public AnchorViewModel AnchorLeft;
        public AnchorViewModel AnchorTopLeft;
        public AnchorViewModel AnchorTop;
        public AnchorViewModel AnchorTopRight;
        public AnchorViewModel AnchorRight;
        public AnchorViewModel AnchorBottomRight;
        public AnchorViewModel AnchorBottom;
        public AnchorViewModel AnchorBottomLeft;

    }

}
