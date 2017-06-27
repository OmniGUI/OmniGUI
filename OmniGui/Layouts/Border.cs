namespace OmniGui.Layouts
{
    using System.Linq;
    using Geometry;
    using Zafiro.PropertySystem.Standard;

    public class Border : Layout
    {
        public static readonly ExtendedProperty BorderBrushProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("BorderBrush", typeof(Border), typeof(Brush), new PropertyMetadata() { DefaultValue = new Brush(Colors.Black) });

        protected override Size MeasureOverride(Size availableSize)
        {
            var padding = Padding + new Thickness(BorderThickness);

            if (Child != null)
            {
                Child.Measure(availableSize.Deflate(padding));
                return Child.DesiredSize.Inflate(padding);
            }
            else
            {
                return new Size(padding.Left + padding.Right, padding.Bottom + padding.Top);
            }
        }

        public Thickness Padding { get; set; }

        public Layout Child
        {
            get { return Children.FirstOrDefault(); }
            set
            {
                Children.Clear();
                Children.Add(value);
            }
        }

        /// <summary>
        /// Arranges the control's child.
        /// </summary>
        /// <param name="finalSize">The size allocated to the control.</param>
        /// <returns>The space taken.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var child = Child;

            if (child != null)
            {
                var padding = Padding + new Thickness(BorderThickness);
                child.Arrange(new Rect(finalSize).Deflate(padding));
            }

            return finalSize;
        }

        public double BorderThickness { get; set; }

        public double CornerRadius { get; set; }

        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.FillRoundedRectangle(Background, VisualBounds, new CornerRadius(CornerRadius));
            drawingContext.DrawRoundedRectangle(new Pen(BorderBrush, BorderThickness), VisualBounds, new CornerRadius(CornerRadius));
            
            base.Render(drawingContext);
        }

        public Brush BorderBrush
        {
            get { return (Brush) GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        public Border(Platform platform) : base(platform)
        {
        }
    }
}