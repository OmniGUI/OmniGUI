using System;
using CoreGraphics;
using UIKit;

namespace iOSApp
{
    public class OmniGuiViewController : UIViewController
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View = new OmniGuiView();

            View.BackgroundColor = UIColor.White;
            Title = "My Custom View Controller";        
        }
    }

    public class OmniGuiView : UIView
    {
        public override void Draw(CGRect rect)
        {
            using (var context = UIGraphics.GetCurrentContext())
            {
                // Drawing code
                CGRect rectangle = new CGRect(0, 100, 320, 100);
                context.SetFillColor((nfloat)1.0, (nfloat)1.0, 0, (nfloat)0.0);
                context.SetStrokeColor((nfloat)0.0, (nfloat)0.0, (nfloat)0.0, (nfloat)0.5);
                context.FillRect(rectangle);
                context.StrokeRect(rectangle);
            }
        }
    }
}