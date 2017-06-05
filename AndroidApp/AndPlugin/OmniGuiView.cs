using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;
using OmniGui;
using Color = Android.Graphics.Color;

namespace AndroidApp.AndPlugin
{
    using OmniGui.Geometry;

    public sealed class OmniGuiView : View
    {
        public Layout Layout { get; set; }

        public OmniGuiView(Context context) : base(context)
        {            
        }

        public override void Draw(Canvas canvas)
        {
            var context = new AndroidDrawingContext(canvas);
            var availableSize = new Size(canvas.Width, canvas.Height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Rect(Point.Zero, availableSize));
            Layout.Render(context);
        }

        public override IInputConnection OnCreateInputConnection(EditorInfo outAttrs)
        {
            return new MyInputConnection(this, true);
        }
    }

    public class MyInputConnection : BaseInputConnection
    {
        public MyInputConnection(OmniGuiView omniGuiView, bool fullEditor) : base(omniGuiView, fullEditor)
        {            
        }

        public override bool CommitText(ICharSequence text, int newCursorPosition)
        {
            return base.CommitText(text, newCursorPosition);
        }
    }
}