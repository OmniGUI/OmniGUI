using System.Linq;
using Android.Graphics;
using Android.Util;
using OmniGui.Geometry;
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

        //public void DrawRectangle(OmniGui.Geometry.Rect rect, Pen pen)
        //{
        //    var paint = new Paint();
        //    paint.Color = pen.Brush.Color.ToAndroid();
        //    canvas.DrawRect(rect.ToAndroid(), paint);
        //}

        //public void DrawRoundedRectangle(OmniGui.Geometry.Rect rect, Pen pen, CornerRadius cornerRadius)
        //{
        //    var paint = new Paint();
        //    paint.SetStyle(Paint.Style.Stroke);
        //    paint.StrokeWidth = (float) pen.Thickness;
        //    paint.Color = pen.Brush.Color.ToAndroid();
        //    var rx = cornerRadius.BottomLeft;
        //    var ry = cornerRadius.BottomLeft;

        //    canvas.DrawRoundRect(rect.ToAndroid(), (float)rx, (float)ry, paint);
        //}

        //public void DrawText(FormattedText formattedText, Point point)
        //{
        //    var paint = new Paint();
        //    paint.TextSize = formattedText.FontSize;
        //    paint.Color = formattedText.Brush.Color.ToAndroid();
        //    canvas.DrawText(formattedText.Text, (float) point.X, (float) point.Y, paint);
        //}

        //public void FillRectangle(OmniGui.Geometry.Rect rect, Brush brush)
        //{
        //    var paint = new Paint();
        //    paint.Color = brush.Color.ToAndroid();
        //    canvas.DrawRect(rect.ToAndroid(), paint);

        //}

        //public void FillRoundedRectangle(OmniGui.Geometry.Rect rect, Brush brush, CornerRadius cornerRadius)
        //{
        //    var paint = new Paint();
        //    paint.Color = brush.Color.ToAndroid();
        //    var rx = cornerRadius.BottomLeft;
        //    var ry = cornerRadius.BottomLeft;

        //    canvas.DrawRoundRect(rect.ToAndroid(), (float)rx, (float)ry, paint);
        //}


        public void DrawRectangle(Rect rect, Pen pen)
        {
            throw new System.NotImplementedException();
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            canvas.DrawRect(rect.ToRectF(), paint);
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float)pen.Thickness;
            paint.Color = pen.Brush.Color.ToAndroid();
            var rx = cornerRadius.BottomLeft;
            var ry = cornerRadius.BottomLeft;

            canvas.DrawRoundRect(rect.ToRectF(), (float)rx, (float)ry, paint);
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            var paint = new Paint();
            paint.Color = brush.Color.ToAndroid();
            canvas.DrawRoundRect(rect.ToRectF(), (float)cornerRadius.BottomLeft, (float)cornerRadius.TopLeft, paint);
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            var paint = new Paint();
            paint.TextSize = formattedText.FontSize;
            paint.Color = formattedText.Brush.Color.ToAndroid();
            paint.AntiAlias = true;
            //var baseLineOffset = paint.Ascent() - paint.Descent();
            var offset = -paint.Ascent();
            canvas.DrawText(formattedText.Text, (float)point.X, (float) (point.Y + offset), paint);
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            var bitmap = AndroidBitmap.CreateBitmap(bmp.Width, bmp.Height, AndroidBitmap.Config.Argb8888);
            //var pixels = bmp.Bytes.Select(b => (int)b).ToArray();
            var pixels = bmp.Bytes.Select(b => 1).ToArray();
            bitmap.SetPixels(pixels, 0, bmp.Width, 0, 0, bmp.Width, bmp.Height);
            using (var b = BitmapFactory.DecodeStream(AndroidPlatform.Current.Assets.Open("mario.png")))
            {
                var r = new global::Android.Graphics.Rect(0,0, b.Width, b.Height);
                canvas.DrawBitmap(b, r, r, null);
            }            
        }

        public void DrawLine(Point startPoint, Point endPoint, Pen pen)
        {
            canvas.DrawLine((float) startPoint.X, (float) startPoint.Y, (float) endPoint.X, (float) endPoint.Y,
                new Paint() {Color = pen.Brush.Color.ToAndroid(), StrokeWidth = (float) pen.Thickness});
        }
    }
}