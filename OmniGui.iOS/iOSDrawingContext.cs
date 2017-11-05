using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using CoreGraphics;
using CoreText;
using Foundation;
using OmniGui.Geometry;
using UIKit;

namespace OmniGui.iOS
{
    public class iOSDrawingContext : IDrawingContext
    {
        private readonly CGContext context;

        public iOSDrawingContext(CGContext context)
        {
            this.context = context;
        }

        public void DrawRectangle(Pen pen, Rect rect)
        {
            context.SetFillColor(pen.Brush.Color.ToiOS());
            context.StrokeRectWithWidth(rect.ToiOS(), (nfloat) pen.Thickness);
        }

        public void FillRectangle(Brush brush, Rect rect)
        {
            var cgRect = rect.ToiOS();

            context.SetFillColor(brush.Color.ToiOS());
            context.FillRect(cgRect);
        }

        public void DrawRoundedRectangle(Pen pen, Rect rect, CornerRadius cornerRadius)
        {
            var rectanglePath = UIBezierPath.FromRoundedRect(rect.ToiOS(), (nfloat)cornerRadius.BottomLeft);
            context.SetStrokeColor(pen.Brush.Color.ToiOS());

            rectanglePath.LineWidth = (nfloat) pen.Thickness;
            rectanglePath.Stroke();
        }

        public void FillRoundedRectangle(Brush brush, Rect rect, CornerRadius cornerRadius)
        {
            var rectanglePath = UIBezierPath.FromRoundedRect(rect.ToiOS(), (nfloat) cornerRadius.BottomLeft);
            context.SetFillColor(brush.Color.ToiOS());
            rectanglePath.Fill();
        }

        public void DrawText(FormattedText formattedText, Point point, Rect? clipRect)
        {
            context.SaveState();
            context.ScaleCTM(1, -1); 
            context.SetFillColor(formattedText.Brush.Color.ToiOS());
   
            var sizeOfText = formattedText.DesiredSize;

            var ctFont = new CTFont(formattedText.FontName, formattedText.FontSize);
            var attributedString = new NSAttributedString(formattedText.Text,
                new CTStringAttributes
                {
                    ForegroundColor = formattedText.Brush.Color.ToiOS(),
                    Font = ctFont
                });

            context.TextPosition = new CGPoint(point.X, -(point.Y + sizeOfText.Height - ctFont.DescentMetric));

            using (var textLine = new CTLine(attributedString))
            {               
                textLine.Draw(context);
            }
            
            context.RestoreState();
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            context.SaveState();
            var invertedRect = new Rect(rect.X, - (rect.Y + rect.Height), rect.Width, rect.Height);
            context.ScaleCTM(1, -1);
            context.DrawImage(invertedRect.ToiOS(), bmp.ToiOS());
            context.RestoreState();
        }

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            context.SetLineWidth((nfloat) pen.Thickness);
            context.SetStrokeColor(pen.Brush.Color.ToiOS());
            context.MoveTo((nfloat) startPoint.X, (nfloat) startPoint.Y);
            context.AddLineToPoint((nfloat) endPoint.X, (nfloat) endPoint.Y);
            context.StrokePath();
        }

        public void PushClip(Rect rect)
        {
            throw new NotImplementedException();
        }

        public void Pop()
        {
            throw new NotImplementedException();
        }
    }
}