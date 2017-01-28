namespace UwpApp
{
    using Microsoft.Graphics.Canvas;
    using OmniGui;

    internal class Win2DDrawingContext : IDrawingContext
    {
        public Win2DDrawingContext(CanvasDrawingSession drawingSession)
        {            
        }

        public void DrawRectangle(Rect rect, Brush brush, Pen pen)
        {
            
        }

        public void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius)
        {
        }

        public void DrawText(Point point, Brush brush, string text)
        {
        }

        public void DrawText(FormattedText text, Point point)
        {
        }
    }
}