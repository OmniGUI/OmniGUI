namespace OmniGui
{
    using Space;
    using Zafiro.PropertySystem.Standard;

    public class Image : Layout
    {
        public readonly ExtendedProperty SourceProperty = PropertyEngine.RegisterProperty("Source", typeof(Image), typeof(Bitmap), new PropertyMetadata() { DefaultValue = null });

        public Image()
        {
        }

        public Bitmap Source
        {
            get { return (Bitmap)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawBitmap(Source, VisualBounds);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var original = new Size(Source.Width, Source.Height);
            var final = original.Constrain(availableSize);

            var height = final.Width * original.GetProportion();

            return new Size(final.Width, height);

        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var original = new Size(Source.Width, Source.Height);
            var final = original.Constrain(finalSize);

            var height = final.Width * original.GetProportion();

            return new Size(final.Width, height);
        }
    }
}