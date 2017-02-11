namespace WpfApp
{
    using System.Windows;
    using Common;

    public class WpfMessageBoxService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}