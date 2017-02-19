namespace WpfApp
{
    using System.Threading.Tasks;
    using System.Windows;
    using Common;
    using TCD.Controls;

    public class WpfMessageBoxService : IMessageService
    {
        public async Task<int> ShowMessage(string message)
        {
            return await CustomMessageBox.ShowAsync("Mensaje", message, MessageBoxImage.Information, 0, "Aceptar");
        }
    }
}