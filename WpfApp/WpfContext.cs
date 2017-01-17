using System;
using System.Windows.Media;
using OmniGui;
using Color = OmniGui.Color;

namespace WpfApp
{
    public class WpfContext : IDrawingContext
    {
        private readonly DrawingContext context;

        public WpfContext(DrawingContext context)
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