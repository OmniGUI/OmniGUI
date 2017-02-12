namespace OmniGui
{
    using Space;
    using Zafiro.PropertySystem.Standard;

    public class Image : Layout
    {
        public readonly ExtendedProperty SourceProperty = PropertyEngine.RegisterProperty("Source", typeof(Image), typeof(Bitmap), new PropertyMetadata() { DefaultValue = null });
        public readonly ExtendedProperty StretchProperty = PropertyEngine.RegisterProperty("Stretch", typeof(Image), typeof(Stretch), new PropertyMetadata { DefaultValue = Stretch.Uniform });

        public Image()
        {
        }

        public Bitmap Source
        {
            get { return (Bitmap)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
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

            if (double.IsInfinity(availableSize.Width) || double.IsInfinity(availableSize.Height))
            {
                return sourceSize;
            }

            return Stretch.CalculateSize(availableSize, sourceSize);
        }       
    }
}