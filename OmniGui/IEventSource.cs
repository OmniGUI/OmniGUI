namespace OmniGui
{
    using System;
    using Geometry;

    public interface IEventSource
    {
        IObservable<Point> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; }
        IObservable<KeyArgs> KeyInput { get; }
        IObservable<ScrollWheelArgs> ScrollWheel { get; }
    }

    public class ScrollWheelArgs
    {
        public double Delta { get; set; }
    }
}