namespace OmniGui.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Geometry;

    public class TestEventSource : IEventSource
    {
        private int invalidateCount;
        private readonly ISubject<KeyInputArgs> keyInputSubject = new Subject<KeyInputArgs>();
        public int InvalidateCount => invalidateCount;
        public IObservable<Point> Pointer => Observable.Never<Point>();
        public IObservable<TextInputArgs> TextInput => Observable.Never<TextInputArgs>();
        public IObservable<KeyInputArgs> KeyInput => keyInputSubject;
        public void Invalidate()
        {
            invalidateCount++;
        }

        public void ShowVirtualKeyboard()
        {
        }

        public void SendKey(char c)
        {
            keyInputSubject.OnNext(new KeyInputArgs
            {
                Text = new string(new[] { c })
            });
        }
    }
}