using System;
using CoreGraphics;
using UIKit;

namespace iOSApp
{
    public class OmniGuiWindow : UIWindow
    {
        public OmniGuiWindow(CGRect mainScreenBounds)
        {            
            
        }

        public override void Draw(CGRect rect)
        {
            using (var context = UIGraphics.GetCurrentContext())
            {
                // Drawing code
                CGRect rectangle = new CGRect(0, 100, 320, 100);
                context.SetFillColor((nfloat) 1.0, (nfloat) 1.0, 0,  (nfloat) 0.0);
                context.SetStrokeColor((nfloat) 0.0, (nfloat) 0.0, (nfloat) 0.0, (nfloat) 0.5);
                context.FillRect(rectangle);
                context.StrokeRect(rectangle);
            }            
        }
    }
}