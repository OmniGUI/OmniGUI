namespace AndroidApp
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using AndPlugin;
    using OmniGui;

    public class AndroidPlatform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public AndroidPlatform(OmniGuiView view)
        {
            TextEngine = new AndroidTextEngine();
            EventSource = new AndroidEventProcessor(view);            
        }
        public ITextEngine TextEngine { get; }
        public IEventSource EventSource { get; }
        public IObservable<Layout> FocusedElement => focusedElementSubject.DistinctUntilChanged();
        public void SetFocusedElement(Layout layout)
        {         
            focusedElementSubject.OnNext(layout);
        }
    }
}