using System;
using System.Reactive.Linq;
using OmniGui.Geometry;

namespace OmniGui.Default
{
    public class DefaultEventSource : IEventSource
    {
        public IObservable<PointerInput> Pointer { get; } = Observable.Never<PointerInput>();
        public IObservable<TextInputArgs> TextInput { get; } = Observable.Never<TextInputArgs>();
        public IObservable<KeyArgs> KeyInput { get; } = Observable.Never<KeyArgs>();
        public IObservable<ScrollWheelArgs> ScrollWheel { get; } = Observable.Never<ScrollWheelArgs>();
    }
}