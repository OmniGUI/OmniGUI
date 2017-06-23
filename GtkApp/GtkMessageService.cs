using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace GtkApp
{
    internal class GtkMessageService : IMessageService
    {
        public async Task<int> ShowMessage(string message)
        {
            MessageBox.Show(message);
            return 1;
        }
    }
}