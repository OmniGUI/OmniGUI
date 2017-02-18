namespace OmniGui.Tests
{
    using System;
    using System.Reactive.Subjects;
    using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Tracing;

    public class TestPlatform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public TestPlatform()
        {
            TextEngine = new TestTextEngine();
            EventSource = new TestEventSource();
        }

        public ITextEngine TextEngine { get; }
        public IEventSource EventSource { get; }
        public IObservable<Layout> FocusedElement => focusedElementSubject;
        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }
    }
}