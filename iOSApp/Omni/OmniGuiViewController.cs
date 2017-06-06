using iOSApp.Omni;
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
}