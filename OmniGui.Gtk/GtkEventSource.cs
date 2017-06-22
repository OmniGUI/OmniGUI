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

            widget.AddEvents((int)EventMask.ButtonPressMask);

            Pointer = GetPointerObservable();
        }

        private IObservable<Point> GetPointerObservable()
        {
            var fromEventPattern = Observable.FromEventPattern<ButtonPressEventHandler, ButtonPressEventArgs>(
                ev => widget.ButtonPressEvent += ev,
                ev => widget.ButtonPressEvent -= ev);
            return fromEventPattern.Select(payload =>
            {
                var x = payload.EventArgs.Event.X;
                var y = payload.EventArgs.Event.Y;

                var point = new Point(x, y);
                return point;
            });
        }

        public IObservable<Point> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
        public IObservable<ScrollWheelArgs> ScrollWheel { get; } = Observable.Never<ScrollWheelArgs>();
    }
}