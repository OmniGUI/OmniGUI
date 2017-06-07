namespace OmniGui.Layouts
{
    using Geometry;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class Image : Layout
    {
        public static ExtendedProperty SourceProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Source", typeof(Image), typeof(Bitmap), new PropertyMetadata() { DefaultValue = null });
        public static ExtendedProperty StretchProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Stretch", typeof(Image), typeof(Stretch), new PropertyMetadata { DefaultValue = Stretch.Uniform });


        public Bitmap Source
        {
            get => (Bitmap)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }


        public override void Render(IDrawingContext drawingContext)
        {
            var viewPort = new Rect(VisualBounds.Point, VisualBounds.Size);
            var sourceSize = new Size(Source.Width, Source.Height);
            var scale = Stretch.CalculateScaling(Bounds.Size, sourceSize);
            var scaledSize = sourceSize * scale;
            var destRect = viewPort
                .CenterIn(new Rect(scaledSize))
                .Intersect(viewPort);
            var sourceRect = new Rect(sourceSize)
                .CenterIn(new Rect(destRect.Size / scale));

            drawingContext.DrawBitmap(Source, sourceRect, destRect);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Source == null)
            {
                return new Size();
            }

            var sourceSize = new Size(Source.Width, Source.Height);

            return Stretch.CalculateSize(availableSize, sourceSize);
        }

        public Image(FrameworkDependencies deps) : base(deps)
        {
        }
    }
}