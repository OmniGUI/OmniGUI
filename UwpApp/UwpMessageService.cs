namespace UwpApp
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Popups;
    using Common;

    internal class UwpMessageService : IMessageService
    {
        public async Task<int> ShowMessage(string message)
        {
            await new MessageDialog(message).ShowAsync();
            return 0;
        }
    }
}