namespace OmniGui.Console
{
    using Space;

    public class ConsoleAdapter : IDrawingContext
    {
        public void DrawRectangle(Rect rect, Color fillColor)
        {
            if (fillColor.Equals(Color.Transparent) || rect.IsEmpty)
            {
                return;
            }

            try
            {
                System.Console.ForegroundColor = ColorExtensions.ClosestConsoleColor(fillColor);
                for (var y = rect.Point.Y; y < rect.Point.Y + rect.Size.Height; y++)
                {
                    for (var x = rect.Point.X; x < rect.Point.X + rect.Size.Width; x++)
                    {
                        Plot(x, y);
                    }
                }
            }
            catch
            {
            }
        }

        private static void Plot(double x, double y)
        {
            var left = (int)x;
            var top = (int)y;

            System.Console.SetCursorPosition(left, top);
            System.Console.Write("█");
        }

        public void DrawRoundedRectangle(Rect rect, Brush brush, Pen pen, CornerRadius cornerRadius)
        {
            throw new System.NotImplementedException();
        }

        public void DrawRectangle(Rect rect, Pen pen)
        {
            throw new System.NotImplementedException();
        }

        public void DrawText(Point point, Brush brush, string text)
        {
            throw new System.NotImplementedException();
        }

        public void DrawText(FormattedText formattedText, Point point)
        {
            throw new System.NotImplementedException();
        }

        public void DrawBitmap(Bitmap bmp, Rect sourceRect, Rect rect)
        {
            throw new System.NotImplementedException();
        }

        public void FillRectangle(Rect rect, Brush brush)
        {
            throw new System.NotImplementedException();
        }

        public void DrawRoundedRectangle(Rect rect, Pen pen, CornerRadius cornerRadius)
        {
            throw new System.NotImplementedException();
        }

        public void FillRoundedRectangle(Rect rect, Brush brush, CornerRadius cornerRadius)
        {
            throw new System.NotImplementedException();
        }
    }
}