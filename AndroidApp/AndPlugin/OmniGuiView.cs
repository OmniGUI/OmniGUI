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
        private readonly Layout layout;

        public OmniGuiView(Context context, Layout layout) : base(context)
        {
            this.layout = layout;
            this.layout = layout;
        }

        public override void Draw(Canvas canvas)
        {
            var availableSize = new Size(canvas.Width, canvas.Height);
            layout.Measure(availableSize);
            layout.Arrange(new Rect(Point.Zero, availableSize));
            var context = new AndroidDrawingContext(canvas);
            layout.Render(context);
        }
    }
}