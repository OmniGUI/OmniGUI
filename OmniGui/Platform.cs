namespace OmniGui
{
    using System;
    using System.Reactive.Subjects;

    public class Platform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();
        public ITextEngine TextEngine { get;  } = new NullTextEngine();
        public static IPlatform Current { get; set; } = new Platform();
        public IEventSource EventSource { get; } = new NullEventDriver();
        public IObservable<Layout> FocusedElement => focusedElementSubject;
        
        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }        
    }
}