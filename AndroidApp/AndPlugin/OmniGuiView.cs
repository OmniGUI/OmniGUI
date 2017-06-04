using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using OmniGui.Geometry;

    public class OmniGuiView : View
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
    }
}