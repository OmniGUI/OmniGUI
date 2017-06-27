using System;
using System.Reactive.Linq;
using Gdk;
using Gtk;
using Point = OmniGui.Geometry.Point;

namespace OmniGui.Gtk
{
    internal class GtkEventSource : IEventSource
    {
        private readonly Widget widget;

        public GtkEventSource(Widget widget)
        {
            this.widget = widget;

            widget.AddEvents((int)(EventMask.ButtonPressMask | EventMask.PointerMotionMask));

            Pointer = GetPointerObservable();
        }

        private IObservable<PointerInput> GetPointerObservable()
        {
            var down = Observable.FromEventPattern<ButtonPressEventHandler, ButtonPressEventArgs>(
                ev => widget.ButtonPressEvent += ev,
                ev => widget.ButtonPressEvent -= ev).Select(ev =>
            {
                var x = ev.EventArgs.Event.X;
                var y = ev.EventArgs.Event.Y;

                var point = new Point(x, y);
                return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Down};
            });

            var up = Observable.FromEventPattern<ButtonReleaseEventHandler, ButtonReleaseEventArgs>(
                ev => widget.ButtonReleaseEvent += ev,
                ev => widget.ButtonReleaseEvent -= ev).Select(ev =>
            {
                var x = ev.EventArgs.Event.X;
                var y = ev.EventArgs.Event.Y;

                var point = new Point(x, y);
                return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Up };
            });

            var move = Observable.FromEventPattern<MotionNotifyEventHandler, MotionNotifyEventArgs>(
                ev => widget.MotionNotifyEvent += ev,
                ev => widget.MotionNotifyEvent -= ev).Select(ev =>
            {
                var x = ev.EventArgs.Event.X;
                var y = ev.EventArgs.Event.Y;

                var point = new Point(x, y);
                return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Released };
            });

            return down.Merge(up).Merge(move);
        }

        public IObservable<PointerInput> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
        public IObservable<ScrollWheelArgs> ScrollWheel { get; } = Observable.Never<ScrollWheelArgs>();
    }
}