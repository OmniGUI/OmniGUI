using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.Views;
using OmniGui.Geometry;

namespace OmniGui.Android
{
    public class AndroidEventSource : IEventSource
    {

        public AndroidEventSource(OmniGuiView view)
        {
            var eventObs = Observable.FromEventPattern<View.TouchEventArgs>(view, "Touch")
                .Select(pattern =>
                {
                    var eventArgsEvent = pattern.EventArgs.Event;
                    return new Point(eventArgsEvent.RawX, eventArgsEvent.RawY);
                });

            view.Touchables.Add(view);
            
            Pointer = eventObs;
            TextInput = view.TextInput.Select(sequence => new TextInputArgs {Text = sequence.ToString()});
            KeyInput = KeyInputObservable(view);
        }
    
        private IObservable<KeyArgs> KeyInputObservable(View element)
        {
            var fromKeyPress = Observable.FromEventPattern<EventHandler<View.KeyEventArgs>, View.KeyEventArgs>(
                    ev => element.KeyPress += ev,
                    ev => element.KeyPress -= ev)
                .Where(pattern => pattern.EventArgs.Event.Action == KeyEventActions.Down)
                .Select(ep => new KeyArgs(ep.EventArgs.KeyCode.ToOmniGui()));

 
            return fromKeyPress;
        }

        public IObservable<Point> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; } = new Subject<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; }
    }
}