using System.Threading.Tasks;
using Common;
using Gtk;

namespace GtkApp
{
    internal class GtkMessageService : IMessageService
    {
        private readonly Window window;

        public GtkMessageService(Window window)
        {
            this.window = window;
        }

        public async Task<int> ShowMessage(string message)
        {
            MessageDialog md = new MessageDialog(window, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, message);
            md.Run();
            md.Destroy();
            return 0;
        }
    }
}