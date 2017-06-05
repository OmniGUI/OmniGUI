using System;
using System.Reactive.Linq;
using Android.App;
using Android.Content;
using Android.Hardware.Input;
using Android.InputMethodServices;
using Android.Views;
using Android.Views.InputMethods;
using OmniGui;

namespace AndroidApp.AndPlugin
{
    using System.Reactive.Subjects;
    using OmniGui.Geometry;

    public class AndroidEventSource : IEventSource
    {
        private readonly View view;
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
            //KeyInput = GetKeyInputObservable(view);
            SpecialKeys = GetSpecialKeysObservable(view);
        }

        //private IObservable<KeyInputArgs> GetKeyInputObservable(View v)
        //{
        //    var fromEventPattern = Observable.FromEventPattern<EventHandler<View.KeyEventArgs>, View.KeyEventArgs>(
        //        ev => v.KeyPress += ev,
        //        ev => v.KeyPress -= ev);
        //    return fromEventPattern.Select(ep => new KeyInputArgs() { Text = ep.EventArgs.KeyCode.ToOmniGui()});
        //}

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
            view.Invalidate();
        }

        public void ShowVirtualKeyboard()
        {
            var imm = (InputMethodManager) activity.GetSystemService(Context.InputMethodService);
            imm.ShowSoftInput(view, ShowFlags.Forced);
        }
    }
}