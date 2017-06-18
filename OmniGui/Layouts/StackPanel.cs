namespace OmniGui.Layouts
{
    using System;
    using Geometry;

    public class StackPanel : Layout
    {
        public double Gap { get; set; }

        public Orientation Orientation { get; set; }

        protected override Size MeasureOverride(Size availableSize)
        {
            var childAvailableWidth = double.PositiveInfinity;
            var childAvailableHeight = double.PositiveInfinity;

            if (Orientation == Orientation.Vertical)
            {
                childAvailableWidth = availableSize.Width;

                if (!double.IsNaN(Width))
                {
                    childAvailableWidth = Width;
                }

                childAvailableWidth = Math.Min(childAvailableWidth, MaxWidth);
                childAvailableWidth = Math.Max(childAvailableWidth, MinWidth);
            }
            else
            {
                childAvailableHeight = availableSize.Height;

                if (!double.IsNaN(Height))
                {
                    childAvailableHeight = Height;
                }

                childAvailableHeight = Math.Min(childAvailableHeight, MaxHeight);
                childAvailableHeight = Math.Max(childAvailableHeight, MinHeight);
            }

            double measuredWidth = 0;
            double measuredHeight = 0;
            var gap = Gap;

            foreach (var child in Children)
            {
                child.Measure(new Size(childAvailableWidth, childAvailableHeight));
                var size = child.DesiredSize;

                if (Orientation == Orientation.Vertical)
                {
                    measuredHeight += size.Height + gap;
                    measuredWidth = Math.Max(measuredWidth, size.Width);
                }
                else
                {
                    measuredWidth += size.Width + gap;
                    measuredHeight = Math.Max(measuredHeight, size.Height);
                }
            }

            return new Size(measuredWidth, measuredHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var orientation = Orientation;
            var arrangedWidth = finalSize.Width;
            var arrangedHeight = finalSize.Height;
            double gap = Gap;

            if (Orientation == Orientation.Vertical)
            {
                arrangedHeight = 0;
            }
            else
            {
                arrangedWidth = 0;
            }

            foreach (var child in Children)
            {
                var childWidth = child.DesiredSize.Width;
                var childHeight = child.DesiredSize.Height;

                if (orientation == Orientation.Vertical)
                {
                    var width = Math.Max(childWidth, arrangedWidth);
                    var childFinal = new Rect(new Point(0, arrangedHeight), new Size(width, childHeight));
                    child.Arrange(childFinal);
                    arrangedWidth = Math.Max(arrangedWidth, childWidth);
                    arrangedHeight += childHeight + gap;
                }
                else
                {
                    var height = Math.Max(childHeight, arrangedHeight);
                    var childFinal = new Rect(new Point(arrangedWidth, 0), new Size(childWidth, height));
                    child.Arrange(childFinal);
                    arrangedWidth += childWidth + gap;
                    arrangedHeight = Math.Max(arrangedHeight, childHeight);
                }
            }

            if (orientation == Orientation.Vertical)

            {

                arrangedHeight = Math.Max(arrangedHeight - gap, finalSize.Height);
            }
            else
            {

                arrangedWidth = Math.Max(arrangedWidth - gap, finalSize.Width);
            }

            return new Size(arrangedWidth, arrangedHeight);
        }

        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.FillRectangle(VisualBounds, Background);
            base.Render(drawingContext);
        }

        public StackPanel(FrameworkDependencies deps) : base(deps)
        {
        }
    }
}