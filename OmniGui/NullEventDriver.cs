using System;
using System.Reactive.Linq;

namespace OmniGui
{
    public class NullEventDriver : IEventProcessor
    {
        public IObservable<Point> Pointer { get; } = Observable.Never<Point>();
        public void Invalidate()
        {
            
        }

        public IObservable<TextInputArgs> TextInput { get; set; }
        public IObservable<KeyInputArgs> KeyInput { get; }
    }
}