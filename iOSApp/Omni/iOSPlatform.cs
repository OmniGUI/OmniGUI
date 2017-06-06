using System;
using System.Reactive.Subjects;
using OmniGui;

namespace iOSApp.Omni
{
    public class iOSPlatform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public ITextEngine TextEngine { get; } = new iOSTextEngine();
        public IEventSource EventSource { get; } = new NullEventDriver();
        public IObservable<Layout> FocusedElement => focusedElementSubject;
        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }
    }
}