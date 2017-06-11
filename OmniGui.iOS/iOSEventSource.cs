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
        public IObservable<KeyInputArgs> KeyInput { get; } = Observable.Never<KeyInputArgs>();
        public IObservable<SpecialKeysArgs> SpecialKeys { get; } = Observable.Never<SpecialKeysArgs>();
    }
}