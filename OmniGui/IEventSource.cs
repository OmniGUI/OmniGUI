namespace OmniGui
{
    using System;
    using Geometry;

    public interface IEventSource
    {
        IObservable<Point> Pointer { get; }
        IObservable<KeyInputArgs> KeyInput { get; }
        IObservable<SpecialKeysArgs> SpecialKeys { get; }
        void Invalidate();
        void ShowVirtualKeyboard();
    }
}