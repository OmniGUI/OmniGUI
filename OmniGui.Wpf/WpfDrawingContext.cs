using System.Globalization;
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

        public void DrawRectangle(Rect rect, Brush brush, Pen pen)
        {
            context.DrawRectangle(brush.ToWpf(), pen.ToWpf(), rect.ToWpf());
        }

        public void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius)
        {
            var geometry = new StreamGeometry();
            using (var gc = geometry.Open())
            {
                bool isStroked = pen != null;
                const bool isSmoothJoin = true;

                gc.BeginFigure((rect.TopLeft + new Vector(0, cornerRadius.TopLeft)).ToWpf(), brush != null, true);

                gc.ArcTo(new Point(rect.TopLeft.X + cornerRadius.TopLeft, rect.TopLeft.Y).ToWpf(),
                    new Size(cornerRadius.TopLeft, cornerRadius.TopLeft).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

                gc.LineTo((rect.TopRight - new Vector(cornerRadius.TopRight, 0)).ToWpf(), isStroked, isSmoothJoin);
                gc.ArcTo(new Point(rect.TopRight.X, rect.TopRight.Y + cornerRadius.TopRight).ToWpf(),
                    new Size(cornerRadius.TopRight, cornerRadius.TopRight).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                gc.LineTo((rect.BottomRight - new Vector(0, cornerRadius.BottomRight)).ToWpf(), isStroked, isSmoothJoin);
                gc.ArcTo(new Point(rect.BottomRight.X - cornerRadius.BottomRight, rect.BottomRight.Y).ToWpf(),
                    new Size(cornerRadius.BottomRight, cornerRadius.BottomRight).ToWpf(),
                    90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);
                gc.LineTo((rect.BottomLeft + new Vector(cornerRadius.BottomLeft, 0)).ToWpf(), isStroked, isSmoothJoin);
                gc.ArcTo(new Point(rect.BottomLeft.X, rect.BottomLeft.Y - cornerRadius.BottomLeft).ToWpf(),
                    new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft).ToWpf(), 90, false, SweepDirection.Clockwise, isStroked, isSmoothJoin);

                gc.Close();
            }

            context.DrawGeometry(brush.ToWpf(), pen.ToWpf(), geometry);
        }

        public void DrawText(Point point, Brush brush, string text)
        {
            var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 15, brush.ToWpf(), new NumberSubstitution(), 3D);
            context.DrawText(formattedText, point.ToWpf());
        }
    }
}