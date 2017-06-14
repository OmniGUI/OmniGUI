namespace OmniGui
{
    using System;
    using Geometry;

    public interface IEventSource
    {
        IObservable<Point> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; }
        IObservable<KeyArgs> KeyInput { get; }
    }
}