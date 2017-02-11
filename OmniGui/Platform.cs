namespace OmniGui
{
    using System;
    using System.Reactive.Subjects;

    public class Platform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();
        public ITextEngine TextEngine { get; set; } = new NullTextEngine();
        public static Platform Current { get; set; } = new Platform();
        public IEventProcessor EventDriver { get; set; } = new NullEventDriver();
        public IObservable<Layout> FocusedElement => focusedElementSubject;

        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }
    }
}