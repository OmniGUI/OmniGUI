using System;
using System.Reactive.Linq;

namespace OmniGui
{
    public class NullEventDriver : IEventProcessor
    {
        public IObservable<Point> Pointer { get; } = Observable.Never<Point>();
    }
}