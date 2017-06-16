using System;
using System.Reactive.Linq;
using OmniGui.Geometry;

namespace OmniGui.Default
{
    public class DefaultEventSource : IEventSource
    {
        public IObservable<Point> Pointer { get; } = Observable.Never<Point>();
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
    }
}