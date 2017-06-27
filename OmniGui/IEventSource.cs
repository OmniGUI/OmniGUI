namespace OmniGui
{
    using System;

    public interface IEventSource
    {
        IObservable<PointerInput> Pointer { get; }
        IObservable<TextInputArgs> TextInput { get; }
        IObservable<KeyArgs> KeyInput { get; }
        IObservable<ScrollWheelArgs> ScrollWheel { get; }
    }
}