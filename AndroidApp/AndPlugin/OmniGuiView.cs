using System;
using System.Reactive.Subjects;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;
using Layout = OmniGui.Layout;

namespace AndroidApp.AndPlugin
{
    using OmniGui.Geometry;

    public sealed class OmniGuiView : View
    {
        public Layout Layout { get; set; }
        public ISubject<ICharSequence> TextInput { get; } = new Subject<ICharSequence>();

        public OmniGuiView(Context context) : base(context)
        {
            FocusableInTouchMode = true;
        }

        public override void Draw(Canvas canvas)
        {
            var context = new AndroidDrawingContext(canvas);
            var availableSize = new Size(canvas.Width, canvas.Height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Rect(Point.Zero, availableSize));
            Layout.Render(context);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }

            return true;
        }

        public override IInputConnection OnCreateInputConnection(EditorInfo outAttrs)
        {
            return new PlatformInputConnection(this, true, TextInput);
        }

        private class PlatformInputConnection : BaseInputConnection
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


}