namespace OmniGui.Uwp
{
    using Microsoft.Graphics.Canvas.Brushes;
    using Geometry;
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Text;
    using OmniGui;

    internal class UwpDrawingContext : IDrawingContext
    {
        public UwpDrawingContext(CanvasDrawingSession drawingSession)
        {
            this.drawingSession = drawingSession;
        }

        private readonly CanvasDrawingSession drawingSession;

        public void FillRectangle(Brush brush, Rect rect)
        {
            var wRect = rect.ToWin2D();
            var wBrush = brush.ToWin2D(drawingSession);
            drawingSession.FillRectangle(wRect, wBrush);
        }


        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
            var winRect = rect.ToWin2D();

            var radius = (float)cornerRadius.BottomLeft;
            var color = pen.Brush.Color.ToWin2D();

            drawingSession.DrawRoundedRectangle(winRect, radius, radius, color, (float)pen.Thickness);
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRect)
        {
            var text = formattedText.Text;
            
            var vector = new System.Numerics.Vector2((float)point.X, (float)point.Y);
            var brush = formattedText.Brush.ToWin2D(drawingSession);
            var canvasTextFormat = new CanvasTextFormat
            {
                FontSize = formattedText.FontSize,
                FontWeight = formattedText.FontWeight.ToWin2D(),
                FontFamily = formattedText.FontName,
            };

            CanvasActiveLayer layer = null;
            
            if (clipRect != null)
            {
                layer = drawingSession.CreateLayer(new CanvasSolidColorBrush(drawingSession, Colors.Black.ToWin2D()), clipRect.Value.ToWin2D());
            }

            drawingSession.DrawText(text, vector, brush, canvasTextFormat);

            layer?.Dispose();
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            drawingSession.DrawImage(bmp.ToWin2D(drawingSession), rect.ToWin2D());
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            var canvasSolidColorBrush = new CanvasSolidColorBrush(drawingSession, pen.Brush.Color.ToWin2D());
            var start = ((Vector)startPoint).ToWin2D();
            var end = ((Vector)endPoint).ToWin2D();
            var penThickness = (float)pen.Thickness;
            drawingSession.DrawLine(start, end, canvasSolidColorBrush, penThickness);
        }

        public void PushClip(Rect rect)
        {
            throw new System.NotImplementedException();
        }

        public void Pop()
        {
            throw new System.NotImplementedException();
        }

        public void DrawRectangle(Pen pen, Rect rect)
        {
            drawingSession.DrawRectangle(rect.ToWin2D(), pen.Brush.Color.ToWin2D());
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {
            var winRect = rect.ToWin2D();
            var radius = (float)cornerRadius.BottomLeft;

            drawingSession.FillRoundedRectangle(winRect, radius, radius, brush.ToWin2D(drawingSession));
        }
    }
}