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
            UIAlertView error = new UIAlertView(message, "", uiAlertViewDelegate, "OK");
            error.Show();
            return Task.FromResult(1);
        }
    }
}