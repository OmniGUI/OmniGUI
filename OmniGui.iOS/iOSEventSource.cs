using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using OmniGui.Geometry;
using UIKit;

namespace OmniGui.iOS
{
    internal class iOSEventSource : IEventSource
    {
        private readonly OmniGuiView view;
        private readonly ISubject<Point> pointSubject = new Subject<Point>();

        public iOSEventSource(OmniGuiView view)
        {
            this.view = view;     
            this.view.AddGestureRecognizer(new UITapGestureRecognizer(Action));
            
            Pointer = pointSubject.AsObservable();
            TextInput = view.TextInput;
            KeyInput = view.KeyInput;
        }

        private void Action(UITapGestureRecognizer gestureRecognizer)
        {
            var p = gestureRecognizer.LocationOfTouch(0, view);
            pointSubject.OnNext(new Point(p.X, p.Y));
        }

        public IObservable<Point> Pointer { get; } 
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
        public IObservable<ScrollWheelArgs> ScrollWheel { get; } = Observable.Never<ScrollWheelArgs>();
    }
}