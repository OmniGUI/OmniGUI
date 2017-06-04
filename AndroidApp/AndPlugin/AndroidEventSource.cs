using System;
using System.Reactive.Linq;
using Android.Views;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using System.Reactive.Subjects;
    using OmniGui.Geometry;

    public class AndroidEventSource : IEventSource
    {
        public AndroidEventSource(View view)
        {
            var eventObs = Observable.FromEventPattern<View.TouchEventArgs>(view, "Touch")
                .Select(pattern =>
                {
                    var eventArgsEvent = pattern.EventArgs.Event;
                    return new Point(eventArgsEvent.RawX, eventArgsEvent.RawY);
                });

            view.Touchables.Add(view);


            Pointer = eventObs;
            SpecialKeys = GetSpecialKeysObservable(view);
        }

        private IObservable<SpecialKeysArgs> GetSpecialKeysObservable(View element)
        {
            var fromEventPattern = Observable.FromEventPattern<EventHandler<View.KeyEventArgs>, View.KeyEventArgs>(
                ev => element.KeyPress += ev,
                ev => element.KeyPress -= ev);
            return fromEventPattern.Select(ep => new SpecialKeysArgs(ep.EventArgs.KeyCode.ToOmniGui()));
        }

        public IObservable<Point> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; } = new Subject<TextInputArgs>();
        public IObservable<KeyInputArgs> KeyInput { get; } = new Subject<KeyInputArgs>();
        public IObservable<SpecialKeysArgs> SpecialKeys { get; }

        public void Invalidate()
        {            
        }

        public void ShowVirtualKeyboard()
        {            
        }
    }
}