using System;
using System.Linq;
using OmniGui.Geometry;
using Zafiro.PropertySystem.Standard;

namespace OmniGui.Layouts
{
    public class ScrollContentPresenter : ContentPresenter
    {
        public static readonly ExtendedProperty OffsetProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty(
            "Offset", typeof(ScrollContentPresenter), typeof(Vector), new PropertyMetadata()
            {
                DefaultValue = new Vector(0, 0)
            });

        public ScrollContentPresenter(Platform platform) : base(platform)
        {
            NotifyRenderAffectedBy(OffsetProperty);
            Platform.EventSource.ScrollWheel.Subscribe(args =>
            {
                var desired = new Vector(0, Offset.Y - args.Delta / 2);
                var offset = CoerceOffset(desired);
                Offset = offset;
            });            
        }

        private Vector CoerceOffset(Vector desired)
        {
            var y = Math.Max(desired.Y, 0);
            y = Math.Min(Extent.Height - DesiredSize.Height, y);

            return new Vector(desired.X, y);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var child = Children.First();
            if (child == null)
            {
                return Size.Zero;
            }

            child.Measure(availableSize.WithHeight(double.PositiveInfinity));
            Extent = child.DesiredSize;

            return availableSize;
        }

        public Size Extent { get; set; }

        public Layout Child => Children.First();

        protected override Size ArrangeOverride(Size finalSize)
        {
            Viewport = finalSize;
            var size = new Size(
                Math.Max(finalSize.Width, Child.DesiredSize.Width),
                Math.Max(finalSize.Height, Child.DesiredSize.Height));

            Child.Arrange(new Rect((Point)(-Offset), size));

            return finalSize;
        }

        public Vector Offset
        {
            get => (Vector) GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public Size Viewport { get; set; }

        public override void Render(IDrawingContext drawingContext)
        {
            using (new ClippingArea(drawingContext, Bounds))
            {
                base.Render(drawingContext);
            }
        }
    }
}