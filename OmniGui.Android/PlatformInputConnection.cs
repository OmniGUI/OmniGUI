using System;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;

namespace OmniGui.Android
{
    internal class PlatformInputConnection : BaseInputConnection
    {
        private readonly IObserver<ICharSequence> textObserver;

        public PlatformInputConnection(View targetView, bool fullEditor, IObserver<ICharSequence> textObserver) : base(targetView, fullEditor)
        {
            this.textObserver = textObserver;
        }

        public override bool CommitText(ICharSequence text, int newCursorPosition)
        {
            textObserver.OnNext(text);
            return true;
        }        
    }
}