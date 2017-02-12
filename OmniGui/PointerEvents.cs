namespace OmniGui
{
    using System;
    using System.Reactive.Linq;
    using Space;

    public class PointerEvents
    {
        public PointerEvents(Layout layout, IEventProcessor eventProcessor)
        {
            Down = eventProcessor.Pointer.Where(point => IsHit(point, layout));
        }

        private static bool IsHit(Point point, Layout layout)
        {
            return layout.VisualBounds.Contains(point);
        }

        public IObservable<Point> Down { get; }
    }
}