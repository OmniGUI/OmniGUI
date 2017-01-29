namespace UwpApp
{
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Geometry;
    using Microsoft.Graphics.Canvas.Text;
    using OmniGui;
    using Color = Windows.UI.Color;

    internal class Win2DDrawingContext : IDrawingContext
    {
        private readonly CanvasDrawingSession drawingSession;

        public Win2DDrawingContext(CanvasDrawingSession drawingSession)
        {
            this.drawingSession = drawingSession;
        }

        public void DrawRectangle(Rect rect, Brush brush, Pen pen)
        {
            var wRect = rect.ToWin2D();
            var wBrush = brush.ToWin2D(drawingSession);
            drawingSession.FillRectangle(wRect, wBrush);
        }

        public void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius)
        {
            var winRect = rect.ToWin2D();

            var radius = (float)cornerRadius.BottomLeft;
            var color = brush.Color.ToWin2D();

            drawingSession.FillRoundedRectangle(winRect, radius, radius, color);
        }

        public void DrawText(Point point, Brush brush, string text)
        {
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            var text = formattedText.Text;
            var vector2 = new System.Numerics.Vector2((float)point.X, (float)point.Y);
            var canvasSolidColorBrush = formattedText.Brush.ToWin2D(drawingSession);
            var canvasTextFormat = new CanvasTextFormat();

            drawingSession.DrawText(text, vector2, canvasSolidColorBrush, canvasTextFormat);
        }
    }
}