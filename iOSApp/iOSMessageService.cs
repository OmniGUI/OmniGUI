using System.Threading.Tasks;
using Common;
using UIKit;

namespace iOSApp
{
    public class iOSMessageService : IMessageService
    {
        public Task<int> ShowMessage(string message)
        {
            IUIAlertViewDelegate uiAlertViewDelegate = new UIAlertViewDelegate();
            UIAlertView error = new UIAlertView("My Title Text", "Error", uiAlertViewDelegate, "OK");
            error.Show();
            return Task.FromResult(1);
        }
    }
}