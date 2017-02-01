using System;
using System.Reactive.Linq;
using Android.Views;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using Point = Point;

    public class AndroidEventProcessor : IEventProcessor
    {
        public AndroidEventProcessor(View view)
        {
            var eventObs = Observable.FromEventPattern<View.TouchEventArgs>(view, "Touch")
                .Select(pattern =>
                {
                    var eventArgsEvent = pattern.EventArgs.Event;
                    return new Point(eventArgsEvent.RawX, eventArgsEvent.RawY);
                });

            view.Touchables.Add(view);


            Pointer = eventObs;
        }

        public IObservable<Point> Pointer { get; }
    }
}