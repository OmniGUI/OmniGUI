using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using OmniGui.Geometry;

namespace OmniGui.Android
{
    public class AndroidEventSource : IEventSource
    {
        private readonly OmniGuiView view;
        private readonly Activity activity;

        public AndroidEventSource(OmniGuiView view, Activity activity)
        {
            this.view = view;
            this.activity = activity;
            var eventObs = Observable.FromEventPattern<View.TouchEventArgs>(view, "Touch")
                .Select(pattern =>
                {
                    var eventArgsEvent = pattern.EventArgs.Event;
                    return new Point(eventArgsEvent.RawX, eventArgsEvent.RawY);
                });

            view.Touchables.Add(view);


            Pointer = eventObs;
            KeyInput = view.TextInput.Select(sequence => new KeyInputArgs {Text = sequence.ToString()});
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
            activity.RunOnUiThread(() => view.Invalidate());            
        }

        public void ShowVirtualKeyboard()
        {
            var imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.ShowSoftInput(view, ShowFlags.Forced);
        }
    }
}