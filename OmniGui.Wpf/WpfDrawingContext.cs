using System;
using System.Windows.Media;

namespace OmniGui.Wpf
{
    public class WpfDrawingContext : IDrawingContext
    {
        private readonly DrawingContext context;

        public WpfDrawingContext(DrawingContext context)
        {
            this.context = context;
        }

        public void DrawRectangle(Rect rect, Color fillColor)
        {
            context.DrawRectangle(new SolidColorBrush(fillColor.ToWpf()), null, rect.ToWpf());
        }

        void IDrawingContext.DrawRectangle(Rect rect, Color fillColor, Color borderColor)
        {
            throw new NotImplementedException();
        }
    }
}