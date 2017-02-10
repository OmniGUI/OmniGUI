namespace OmniGui
{
    using System;

    public interface IEventProcessor
    {
        IObservable<Point> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; }
        IObservable<KeyInputArgs> KeyInput { get; }
        void Invalidate();
    }
}