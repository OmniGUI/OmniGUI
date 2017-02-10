using System;
using System.Reactive.Linq;
using Android.Views;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using System.Reactive.Subjects;
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
        IObservable<Point> IEventProcessor.Pointer
        {
            get { return Pointer; }
        }

        public IObservable<TextInputArgs> TextInput { get; } = new Subject<TextInputArgs>();
        public IObservable<KeyInputArgs> KeyInput { get; } = new Subject<KeyInputArgs>();
        public void Invalidate()
        {
            
        }
    }
}