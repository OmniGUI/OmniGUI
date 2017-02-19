using System;
using System.Reactive.Linq;

namespace OmniGui
{
    using Geometry;

    public class NullEventDriver : IEventSource
    {
        public IObservable<Point> Pointer { get; } = Observable.Never<Point>();
        public IObservable<SpecialKeysArgs> SpecialKeys { get; }

        public void Invalidate()
        {            
        }

        public void ShowVirtualKeyboard()
        {            
        }

        public IObservable<TextInputArgs> TextInput => Observable.Never<TextInputArgs>();
        public IObservable<KeyInputArgs> KeyInput => Observable.Never<KeyInputArgs>();
    }
}