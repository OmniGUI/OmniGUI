using System;
using System.Windows;
using System.Windows.Media;

namespace OmniGui.Wpf
{
    public class WpfDrawingContext : IDrawingContext
    {
        private readonly DrawingContext context;

        public WpfDrawingContext(DrawingContext context)
        {
            this.context = context;
        }

        public void DrawRectangle(Rect rect, Color fillColor)
        {
            DrawRoundedRectangle(rect, fillColor, Color.Transparent, new CornerRadius(10));
            //context.DrawRectangle(new SolidColorBrush(fillColor.ToWpf()), null, rect.ToWpf());
        }

        public void DrawRoundedRectangle(Rect rect, Color fillColor, Color borderColor, CornerRadius cornerRadius)
        {
            var pen = new Pen();
            var brush = new SolidColorBrush(fillColor.ToWpf());
            var geometry = new StreamGeometry();
            using (var context = geometry.Open())
            {
                bool isStroked = pen != null;
                const bool isSmoothJoin = true;

                context.BeginFigure((rect.TopLeft + new Vector(0, cornerRadius.TopLeft)).ToWpf(), brush != null, true);

                context.ArcTo(new Point(rect.TopLeft.X + cornerRadius.TopLeft, rect.TopLeft.Y).ToWpf(),
                    new Size(cornerRadius.TopLeft, cornerRadius.TopLeft).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

                context.LineTo((rect.TopRight - new Vector(cornerRadius.TopRight, 0)).ToWpf(), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.TopRight.X, rect.TopRight.Y + cornerRadius.TopRight).ToWpf(),
                    new Size(cornerRadius.TopRight, cornerRadius.TopRight).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                context.LineTo((rect.BottomRight - new Vector(0, cornerRadius.BottomRight)).ToWpf(), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.BottomRight.X - cornerRadius.BottomRight, rect.BottomRight.Y).ToWpf(),
                    new Size(cornerRadius.BottomRight, cornerRadius.BottomRight).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                context.LineTo((rect.BottomLeft + new Vector(cornerRadius.BottomLeft, 0)).ToWpf(), isStroked, isSmoothJoin);
                context.ArcTo(new Point(rect.BottomLeft.X, rect.BottomLeft.Y - cornerRadius.BottomLeft).ToWpf(),
                    new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft).ToWpf(), 90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

                context.Close();
            }

            context.DrawGeometry(brush, pen, geometry);
        }
    }
}