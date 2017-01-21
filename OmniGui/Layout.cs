using System;
using OmniXaml.Attributes;

namespace OmniGui
{
    public abstract class Layout : IChild
    {
        protected Layout()
        {
            Children = new OwnedList<Layout>(this);
        }

        public Color Background { get; set; } = Color.Transparent;
        public object Parent { get; set; }
        public Size RequestedSize { get; set; } = Size.Unspecified;
        public Size DesiredSize { get; set; }
        [Content]
        public OwnedList<Layout> Children { get; }
        public Rect Bounds { get; set; }

        public Rect VisualBounds
        {
            get
            {
                if (Parent == null)
                {
                    return Bounds;
                }
                else
                {
                    var parent = (Layout)Parent;
                    var offset = parent.VisualBounds.Point.Offset(Bounds.Point);
                    return new Rect(offset, Bounds.Size);
                }
            }
        }

        public void Measure(Size availableSize)
        {
            if (double.IsNaN(availableSize.Width) || double.IsNaN(availableSize.Height))
            {
                throw new InvalidOperationException("Cannot call Measure using a size with NaN values.");
            }

            var desiredSize = MeasureCore(availableSize).Constrain(availableSize);

            if (IsInvalidSize(desiredSize))
            {
                throw new InvalidOperationException("Invalid size returned for Measure.");
            }

            DesiredSize = desiredSize;
        }

        private static bool IsInvalidSize(Size size)
        {
            return size.Width < 0 || size.Height < 0 ||
                double.IsInfinity(size.Width) || double.IsInfinity(size.Height) ||
                double.IsNaN(size.Width) || double.IsNaN(size.Height);
        }

        private Size MeasureCore(Size availableSize)
        {
            var margin = Margin;

            var constrained = LayoutHelper
                .ApplyLayoutConstraints(this, availableSize)
                .Deflate(margin);

            var measured = MeasureOverride(constrained);

            var width = measured.Width;
            var height = measured.Height;

            if (!double.IsNaN(Width))
            {
                width = Width;
            }

            width = Math.Min(width, MaxWidth);
            width = Math.Max(width, MinWidth);

            if (!double.IsNaN(Height))
            {
                height = Height;
            }

            height = Math.Min(height, MaxHeight);
            height = Math.Max(height, MinHeight);


            return NonNegative(new Size(width, height).Inflate(margin));
        }

        private static Size NonNegative(Size size)
        {
            return new Size(Math.Max(size.Width, 0), Math.Max(size.Height, 0));
        }

        protected abstract Size MeasureOverride(Size availableSize);

        public void Arrange(Rect rect)
        {
            if (IsInvalidRect(rect))
            {
                throw new InvalidOperationException("Invalid Arrange rectangle.");
            }

            ArrangeCore(rect);
        }

        private void ArrangeCore(Rect finalRect)
        {
            if (true)
            {
                var margin = Margin;
                var originX = finalRect.X + margin.Left;
                var originY = finalRect.Y + margin.Top;
                var availableSizeMinusMargins = new Size(
                    Math.Max(0, finalRect.Width - margin.Left - margin.Right),
                    Math.Max(0, finalRect.Height - margin.Top - margin.Bottom));
                var horizontalAlignment = HorizontalAlignment;
                var verticalAlignment = VerticalAlignment;
                var size = availableSizeMinusMargins;

                if (horizontalAlignment != HorizontalAlignment.Stretch)
                {
                    size = size.WithWidth(Math.Min(size.Width, DesiredSize.Width - margin.Left - margin.Right));
                }

                if (verticalAlignment != VerticalAlignment.Stretch)
                {
                    size = size.WithHeight(Math.Min(size.Height, DesiredSize.Height - margin.Top - margin.Bottom));
                }

                size = LayoutHelper.ApplyLayoutConstraints(this, size);

                size = ArrangeOverride(size).Constrain(size);

                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Center:
                    case HorizontalAlignment.Stretch:
                        originX += (availableSizeMinusMargins.Width - size.Width) / 2;
                        break;
                    case HorizontalAlignment.Right:
                        originX += availableSizeMinusMargins.Width - size.Width;
                        break;
                }

                switch (verticalAlignment)
                {
                    case VerticalAlignment.Center:
                    case VerticalAlignment.Stretch:
                        originY += (availableSizeMinusMargins.Height - size.Height) / 2;
                        break;
                    case VerticalAlignment.Bottom:
                        originY += availableSizeMinusMargins.Height - size.Height;
                        break;
                }


                Bounds = new Rect(originX, originY, size.Width, size.Height);
            }
        }

        private static bool IsInvalidRect(Rect rect)
        {
            return rect.Width < 0 || rect.Height < 0 ||
                double.IsInfinity(rect.X) || double.IsInfinity(rect.Y) ||
                double.IsInfinity(rect.Width) || double.IsInfinity(rect.Height) ||
                double.IsNaN(rect.X) || double.IsNaN(rect.Y) ||
                double.IsNaN(rect.Width) || double.IsNaN(rect.Height);
        }

        public VerticalAlignment VerticalAlignment { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public Thickness Margin { get; set; }
        public double Width => RequestedSize.Width;
        public double Height => RequestedSize.Height;
        public double MaxWidth => MaxSize.Width;
        public Size MaxSize { get; set; } = Size.Maximum;

        public double MaxHeight => MaxSize.Height;
        public double MinWidth => MinSize.Width;
        public Size MinSize { get; set; } = Size.Zero;

        public double MinHeight => MinSize.Height;

        protected abstract Size ArrangeOverride(Size size);

        public void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(VisualBounds, Background);
            foreach (var child in Children)
            {
                child.Render(drawingContext);
            }
        }
    }
}