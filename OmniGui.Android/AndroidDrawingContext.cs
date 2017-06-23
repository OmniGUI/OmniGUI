using Android.Graphics;
using Zafiro.Core;
using Point = OmniGui.Geometry.Point;
using Rect = OmniGui.Geometry.Rect;
using AndroidBitmap = Android.Graphics.Bitmap;

namespace OmniGui.Android
{
    public class AndroidDrawingContext : IDrawingContext
    {
        private readonly Canvas canvas;

        public AndroidDrawingContext(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void DrawRectangle(Pen pen, Rect rect)
        {
            var paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float)pen.Thickness;
            paint.Color = pen.Brush.Color.ToAndroid();

            canvas.DrawRect(rect.ToRectF(), paint);
        }

        public void FillRectangle(Brush brush, Rect rect)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            canvas.DrawRect(rect.ToRectF(), paint);
        }

        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float)pen.Thickness;
            paint.Color = pen.Brush.Color.ToAndroid();
            var rx = cornerRadius.BottomLeft;
            var ry = cornerRadius.BottomLeft;

            canvas.DrawRoundRect(rect.ToRectF(), (float)rx, (float)ry, paint);
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            canvas.DrawRoundRect(rect.ToRectF(), (float)cornerRadius.BottomLeft, (float)cornerRadius.TopLeft, paint);
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRect)
        {
            var paint = new Paint();
            paint.TextSize = formattedText.FontSize;
            paint.Color = formattedText.Brush.Color.ToAndroid();
            paint.AntiAlias = true;
            //var baseLineOffset = paint.Ascent() - paint.Descent();
            var offset = -paint.Ascent();
            canvas.DrawText(formattedText.Text, (float)point.X, (float)(point.Y + offset), paint);
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            var bitmap = AndroidBitmap.CreateBitmap(bmp.Width, bmp.Height, AndroidBitmap.Config.Argb8888);
            bitmap.SetPixels(bmp.Bytes.ToIntArray(), 0, bmp.Width, 0, 0, bmp.Width, bmp.Height);
            canvas.DrawBitmap(bitmap, sourceRect.ToRect(), rect.ToRectF(), null);
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            canvas.DrawLine((float)startPoint.X, (float)startPoint.Y, (float)endPoint.X, (float)endPoint.Y,
                new Paint { Color = pen.Brush.Color.ToAndroid(), StrokeWidth = (float)pen.Thickness });
        }
    }
}