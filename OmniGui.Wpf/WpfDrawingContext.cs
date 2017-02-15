using System.Windows.Media;

namespace OmniGui.Wpf
{
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using Space;

    public class WpfDrawingContext : IDrawingContext
    {
        private readonly DrawingContext context;

        public WpfDrawingContext(DrawingContext context)
        {
            this.context = context;
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            context.DrawRectangle(null, pen.ToWpf(), rect.ToWpf());
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            DrawWpfRoundedRectangle(rect, brush, null, cornerRadius);
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            DrawWpfRoundedRectangle(rect, null, pen, cornerRadius);
        }

        private void DrawWpfRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius)
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
                    new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft).ToWpf(), 90, false, SweepDirection.Clockwise,
                    isStroked, isSmoothJoin);

                gc.Close();
            }

            context.DrawGeometry(brush.ToWpf(), pen.ToWpf(), geometry);
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            context.DrawText(formattedText.ToWpf(), point.ToWpf());
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            var width = bmp.Width;
            var height = bmp.Height;
            var dpiX = 96d;
            var dpiY = 96d;
            var pixelFormat = PixelFormats.Bgra32; 

            var bitmap = BitmapSource.Create(width, height, dpiX, dpiY,
                pixelFormat, null, bmp.Bytes, bmp.Width * 4);

            context.DrawImage(bitmap, rect.ToWpf());
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            context.DrawRectangle(brush.ToWpf(), null, rect.ToWpf());
        }
    }
}