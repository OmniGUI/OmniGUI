using System;
using System.Reactive.Linq;
using OmniGui.Geometry;
using UIKit;

namespace OmniGui.iOS
{
    internal class iOSEventSource : IEventSource
    {
        private readonly UIView view;

        public iOSEventSource(UIView view)
        {
            this.view = view;
        }

        public IObservable<Point> Pointer { get; } = Observable.Never<Point>();
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
    }
}