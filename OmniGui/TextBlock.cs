namespace OmniGui
{
    public class TextBlock : Layout
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Text == null)
            {
                return Size.Empty;
            }

            return Platform.Current.TextEngine.MeasureText(Text);
        }

        protected override Size ArrangeOverride(Size size)
        {
            return DesiredSize;
        }

        public override void Render(IDrawingContext drawingContext)
        {
            if (Text == null)
            {
                return;
            }

            drawingContext.DrawText(VisualBounds.Point, Foreground, Text);
        }

        public Brush Foreground { get; set; } = new Brush(Colors.Black);

        public string Text { get; set; }
    }
}