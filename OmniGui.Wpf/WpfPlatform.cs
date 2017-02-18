namespace WpfApp
{
    using System;
    using System.Reactive.Subjects;
    using System.Windows;
    using OmniGui;
    using OmniGui.Wpf;

    public class WpfPlatform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public WpfPlatform(FrameworkElement element)
        {
            TextEngine = new WpfTextEngine();
            EventSource = new WpfEventSource(element);
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