using System.Reactive;

namespace OmniGui
{
    using System;
    using System.Reactive.Linq;
    using Geometry;

    public class PointerEvents
    {
        public PointerEvents(Layout layout, IEventSource eventSource)
        {
            Down = eventSource.Pointer.Where(input => input.PrimaryButtonStatus == PointerStatus.Down && IsHit(input.Point, layout)).Select(input => input.Point);
            Up = eventSource.Pointer.Where(input => input.PrimaryButtonStatus == PointerStatus.Up && IsHit(input.Point, layout)).Select(input => input.Point);
            InsideLayout = eventSource.Pointer.Where(input => input.PrimaryButtonStatus == PointerStatus.Released).Select(input => IsHit(input.Point, layout)).DistinctUntilChanged();
            Enter = InsideLayout.Where(isInside => isInside).Select(_ => Unit.Default);
            Leave = InsideLayout.Where(isInside => !isInside).Select(_ => Unit.Default);
        }

        public IObservable<Unit> Leave { get; }

        public IObservable<Unit> Enter { get; }

        public IObservable<bool> InsideLayout { get; }


        public IObservable<Point> Up { get; }

        private static bool IsHit(Point point, Layout layout)
        {
            return layout.VisualBounds.Contains(point);
        }

        public IObservable<Point> Down { get; }
    }

    public enum PointerStatus
    {
        Unset,
        Down,
        Up,
        Released
    }
}