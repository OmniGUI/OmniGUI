namespace OmniGui.Console
{
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

        public void DrawRectangle(Rect rect, Color fillColor, Color borderColor)
        {
            throw new System.NotImplementedException();
        }
    }
}