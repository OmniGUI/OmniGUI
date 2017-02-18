namespace OmniGui
{
    using System;

    public interface IPlatform
    {
        ITextEngine TextEngine { get; }
        IEventSource EventSource { get;  }
        IObservable<Layout> FocusedElement { get; }
        void SetFocusedElement(Layout layout);
    }
}