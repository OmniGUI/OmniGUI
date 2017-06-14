using Android.Views;

namespace OmniGui.Android
{
    public class ActivityKeyArgs
    {
        public Keycode KeyCode { get; }
        public KeyEvent KeyEvent { get; }

        public ActivityKeyArgs(Keycode keyCode, KeyEvent keyEvent)
        {
            KeyCode = keyCode;
            KeyEvent = keyEvent;
        }
    }
}