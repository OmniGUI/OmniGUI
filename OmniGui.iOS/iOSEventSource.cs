using System;
using OmniGui.Geometry;
using UIKit;

namespace OmniGui.iOS
{
    internal class iOSEventSource : IEventSource
    {
        public iOSEventSource(UIView view)
        {
        }

        public IObservable<Point> Pointer { get; }
        public IObservable<KeyInputArgs> KeyInput { get; }
        public IObservable<SpecialKeysArgs> SpecialKeys { get; }
    }
}