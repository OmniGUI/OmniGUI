namespace OmniGui
{
    using System;
    using System.Reactive.Linq;
    using Geometry;

    public class PointerEvents
    {
        public PointerEvents(Layout layout, IEventSource eventSource)
        {
            Down = eventSource.Pointer.Where(point => IsHit(point, layout));
        }

        private static bool IsHit(Point point, Layout layout)
        {
            return layout.VisualBounds.Contains(point);
        }

        public IObservable<Point> Down { get; }
    }
}