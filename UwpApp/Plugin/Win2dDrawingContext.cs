using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using OmniGui;

namespace UwpApp.Plugin
{
    using OmniGui.Geometry;

    internal class Win2DDrawingContext : IDrawingContext
    {
        private CanvasDrawingSession drawingSession;

        public void FillRectangle(Rect rect, Brush brush)
        {
            var wRect = rect.ToWin2D();
            var wBrush = brush.ToWin2D(drawingSession);
            drawingSession.FillRectangle(wRect, wBrush);
        }


        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            var winRect = rect.ToWin2D();

            var radius = (float)cornerRadius.BottomLeft;
            var color = pen.Brush.Color.ToWin2D();

            drawingSession.DrawRoundedRectangle(winRect, radius, radius, color);
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            var text = formattedText.Text;
            var vector2 = new System.Numerics.Vector2((float)point.X, (float)point.Y);
            var canvasSolidColorBrush = formattedText.Brush.ToWin2D(drawingSession);
            var canvasTextFormat = new CanvasTextFormat
            {
                FontSize = formattedText.FontSize,
                FontWeight = formattedText.FontWeight.ToWin2D(),
            };

            drawingSession.DrawText(text, vector2, canvasSolidColorBrush, canvasTextFormat);
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {            
            drawingSession.DrawImage(bmp.ToWin2D(drawingSession), rect.ToWin2D());
        }

        public void DrawLine(Point startPoint, Point endPoint, Pen pen)
        {
            
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            drawingSession.DrawRectangle(rect.ToWin2D(), pen.Brush.Color.ToWin2D());
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            var winRect = rect.ToWin2D();
            var radius = (float)cornerRadius.BottomLeft;

            drawingSession.FillRoundedRectangle(winRect, radius, radius, brush.ToWin2D(drawingSession));
        }

        public void SetDrawingSession(CanvasDrawingSession drawingSession)
        {
            this.drawingSession = drawingSession;
        }
    }
}