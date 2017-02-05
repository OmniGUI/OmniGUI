namespace OmniGui
{
    using System;

    public interface IEventProcessor
    {
        IObservable<Point> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; set; }
        void Invalidate();
    }
}