namespace OmniGui
{
    using System;
    using Geometry;

    public interface IEventProcessor
    {
        IObservable<Point> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; }
        IObservable<KeyInputArgs> KeyInput { get; }
        void Invalidate();
    }
}