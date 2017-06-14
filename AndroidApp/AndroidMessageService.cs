using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Common;

namespace AndroidApp
{
    public class AndroidMessageService : IMessageService
    {
        private readonly Context context;

        public AndroidMessageService(Context context)
        {
            this.context = context;
        }

        public Task<int> ShowMessage(string message)
        {
            Toast toast = Toast.MakeText(context, message, ToastLength.Short);
            toast.Show();
            return Task.FromResult(1);
        }
    }
}