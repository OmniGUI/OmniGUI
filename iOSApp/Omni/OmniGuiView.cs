using CoreGraphics;
using OmniGui;
using OmniGui.Geometry;
using OmniGui.Layouts;
using UIKit;

namespace iOSApp.Omni
{
    public class OmniGuiView : UIView
    {
        public Layout Layout { get; set; }

        public OmniGuiView()
        {
            Layout = new Border { Background = new Brush(Colors.Blue), BorderBrush = new Brush(Colors.Red), BorderThickness = 5};
        }

        public override void Draw(CGRect rect)
        {
            Layout.Measure(new Size(30, 30));
            Layout.Arrange(rect.ToOmniGui());
            using (var ctx = UIGraphics.GetCurrentContext())
            {
                Layout.Render(new iOSDrawingContext(ctx));
            }

            //var drawingContext = new iOSDrawingContext(UIGraphics.GetCurrentContext());

            //using (var context = UIGraphics.GetCurrentContext())
            //{
            //    // Drawing code
            //    CGRect rectangle = new CGRect(0, 100, 320, 100);
            //    context.SetFillColor((nfloat)1.0, (nfloat)1.0, 0, (nfloat)0.0);
            //    context.SetStrokeColor((nfloat)0.0, (nfloat)0.0, (nfloat)0.0, (nfloat)0.5);
            //    context.FillRect(rectangle);
            //    context.StrokeRect(rectangle);
            //}
        }
    }
}