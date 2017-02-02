namespace OmniGui
{
    using System;

    public interface IEventProcessor
    {
        IObservable<Point> Pointer { get; }
        void Invalidate();
    }
}