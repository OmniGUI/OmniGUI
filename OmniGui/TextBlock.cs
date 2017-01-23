namespace OmniGui
{
    public class TextBlock : Layout
    {
        private string text;
        private Brush foreground;

        public TextBlock()
        {
            Foreground = new Brush(Colors.Black);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Text == null)
            {
                return Size.Empty;
            }

            if (TextWrapping == TextWrapping.Wrap)
            {
                FormattedText.Constraint = new Size(availableSize.Width, double.PositiveInfinity);
            }
            else
            {
                FormattedText.Constraint = Size.Infinite;
            }

            return FormattedText.Measure();
        }

        public override void Render(IDrawingContext drawingContext)
        {
            if (Text == null)
            {
                return;
            }

            drawingContext.DrawText(FormattedText, VisualBounds.Point);
        }

        public Brush Foreground
        {
            get { return foreground; }
            set
            {
                foreground = value;
                this.FormattedText.Brush = value;
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                FormattedText.Text = value;
            }
        }

        private FormattedText FormattedText { get; set; } = new FormattedText();

        public TextWrapping TextWrapping { get; set; }
    }
}