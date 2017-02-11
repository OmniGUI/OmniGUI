namespace OmniGui
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using OmniXaml.Attributes;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Attached;
    using Zafiro.PropertySystem.Standard;

    public abstract class Layout : IChild
    {
        protected static readonly PropertyEngine PropertyEngine = new PropertyEngine(o => ((IChild) o).Parent);

        public static readonly ExtendedProperty DataContextProperty = PropertyEngine.RegisterProperty("DataContext",
            typeof(Layout), typeof(object), new PropertyMetadata());

        protected Layout()
        {
            Children = new OwnedList<Layout>(this);
            Children.OnChildAdded(layout =>
            {
                layout.DataContext = DataContext;
                this.GetChangedObservable(DataContextProperty).Subscribe(o => layout.DataContext = o);
            });
            Pointer = new PointerEvents(this, Platform.Current.EventDriver);
            Keyboard = new KeyboardEvents(this, Platform.Current.EventDriver, Platform.Current.FocusedElement);
        }

        public object DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        public KeyboardEvents Keyboard { get; private set; }

        protected void NotifyRenderAffectedBy(params ExtendedProperty[] properties)
        {
            var observables = properties
                .Select(GetChangedObservable);

            var ticks = observables.Merge();
            ticks.Subscribe(_ => InvalidateRender());
        }

        private void InvalidateRender()
        {
            Platform.Current.EventDriver.Invalidate();
        }

        public PointerEvents Pointer { get; set; }

        public Brush Background { get; set; } = new Brush(Color.Transparent);
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
                var parent = (Layout) Parent;
                var offset = parent.VisualBounds.Point.Offset(Bounds.Point);
                return new Rect(offset, Bounds.Size);
            }
        }

        public VerticalAlignment VerticalAlignment { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public Thickness Margin { get; set; }
        public double Width => RequestedSize.Width;
        public double Height => RequestedSize.Height;
        public double MaxWidth => MaxSize.Width;
        public Size MaxSize { get; set; } = Size.Infinite;

        public double MaxHeight => MaxSize.Height;
        public double MinWidth => MinSize.Width;
        public Size MinSize { get; set; } = Size.Zero;

        public double MinHeight => MinSize.Height;
        public object Parent { get; set; }

        public void SetValue(AttachedProperty property, object value)
        {
            PropertyEngine.SetValue(property, this, value);
        }

        public void SetValue(ExtendedProperty property, object value)
        {
            PropertyEngine.SetValue(property, this, value);
        }

        public object GetValue(AttachedProperty property)
        {
            return PropertyEngine.GetValue(property, this);
        }

        public object GetValue(ExtendedProperty property)
        {
            return PropertyEngine.GetValue(property, this);
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

        protected virtual Size MeasureOverride(Size availableSize)
        {
            double width = 0;
            double height = 0;

            foreach (var child in Children)
            {
                child.Measure(availableSize);
                width = Math.Max(width, child.DesiredSize.Width);
                height = Math.Max(height, child.DesiredSize.Height);
            }

            return new Size(width, height);
        }

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

        protected virtual Size ArrangeOverride(Size finalSize)
        {
            foreach (var child in Children)
            {
                child.Arrange(new Rect(finalSize));
            }

            return finalSize;
        }

        public virtual void Render(IDrawingContext drawingContext)
        {
            foreach (var layout in Children)
            {
                layout.Render(drawingContext);
            }
        }

        public IObservable<object> GetChangedObservable(ExtendedProperty property)
        {
            return PropertyEngine.GetChangedObservable(property, this);
        }

        public IObserver<object> GetObserver(ExtendedProperty property)
        {
            return PropertyEngine.GetObserver(property, this);
        }

        public ExtendedProperty GetProperty(string propertyName)
        {
            return PropertyEngine.GetProperty(propertyName, GetType());
        }
    }
}