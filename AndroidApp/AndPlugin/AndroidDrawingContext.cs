using Android.Graphics;
using OmniGui;
using Point = OmniGui.Point;
using Rect = OmniGui.Rect;

namespace AndroidApp.AndPlugin
{
    public class AndroidDrawingContext : IDrawingContext
    {
        private readonly Canvas canvas;

        public AndroidDrawingContext(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            var paint = new Paint();
            paint.Color = pen.Brush.Color.ToAndroid();
            canvas.DrawRect(rect.ToAndroid(), paint);
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float) pen.Thickness;
            paint.Color = pen.Brush.Color.ToAndroid();
            var rx = cornerRadius.BottomLeft;
            var ry = cornerRadius.BottomLeft;

            canvas.DrawRoundRect(rect.ToAndroid(), (float)rx, (float)ry, paint);
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            var paint = new Paint();
            paint.TextSize = formattedText.FontSize;
            paint.Color = formattedText.Brush.Color.ToAndroid();
            canvas.DrawText(formattedText.Text, (float) point.X, (float) point.Y, paint);
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            canvas.DrawRect(rect.ToAndroid(), paint);

        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            var rx = cornerRadius.BottomLeft;
            var ry = cornerRadius.BottomLeft;

            canvas.DrawRoundRect(rect.ToAndroid(), (float)rx, (float)ry, paint);
        }
    }
}