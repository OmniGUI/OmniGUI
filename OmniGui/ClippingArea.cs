using System;
using OmniGui.Geometry;

namespace OmniGui
{
    public class ClippingArea : IDisposable
    {
        private readonly IDrawingContext context;

        public ClippingArea(IDrawingContext context, Rect rect)
        {
            this.context = context;
            context.PushClip(rect);
        }

        public void Dispose()
        {
            context.Pop();
        }
    }
}