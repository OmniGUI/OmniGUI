namespace UwpApp
{
    using System;
    using System.Reactive.Subjects;
    using Windows.UI.Xaml;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using OmniGui;
    using Plugin;

    public class UwpPlatform : IPlatform
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public UwpPlatform(FrameworkElement frameworkElement, CanvasControl canvasControl)
        {
            TextEngine = new Win2DTextEngine();
            EventSource = new UwpEventSource(frameworkElement, canvasControl);            
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