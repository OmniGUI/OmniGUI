using System;
using CoreGraphics;
using OmniGui;
using OmniGui.Geometry;

namespace iOSApp.Omni
{
    public class iOSDrawingContext : IDrawingContext
    {
        private readonly CGContext context;

        public iOSDrawingContext(CGContext context)
        {
            this.context = context;
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            throw new System.NotImplementedException();
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            //var paint = new Paint();
            //paint.Color = brush.Color.ToAndroid();
            //canvas.DrawRect(rect.ToAndroid(), paint);
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            var cgRect = rect.ToiOS();

            context.SetStrokeColor(pen.Brush.Color.ToiOS());
            context.StrokeRectWithWidth(cgRect, (nfloat) pen.Thickness);

            //CGRect rectangle = rect.ToiOS();
            //context.SetFillColor((nfloat)1.0, (nfloat)1.0, 0, (nfloat)0.0);
            //context.SetStrokeColor((nfloat)0.0, (nfloat)0.0, (nfloat)0.0, (nfloat)0.5);
            //context.FillRect(rectangle);
            //context.StrokeRect(rectangle);
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            var cgRect = rect.ToiOS();

            context.SetFillColor(brush.Color.ToiOS());
            context.FillRect(cgRect);
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            //var paint = new Paint();
            //paint.TextSize = formattedText.FontSize;
            //paint.Color = formattedText.Brush.Color.ToAndroid();
            //paint.AntiAlias = true;
            //var baseLineOffset = -paint.Descent() + paint.Ascent();
            //canvas.DrawText(formattedText.Text, (float)point.X, (float)point.Y - baseLineOffset, paint);
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            throw new System.NotImplementedException();
        }

        public void DrawLine(Point startPoint, Point endPoint, Pen pen)
        {
            //CGRect rectangle = rect.ToiOS();
            //context.SetFillColor((nfloat)1.0, (nfloat)1.0, 0, (nfloat)0.0);
            //context.SetStrokeColor((nfloat)0.0, (nfloat)0.0, (nfloat)0.0, (nfloat)0.5);
            //context.FillRect(rectangle);
            
        }
    }
}