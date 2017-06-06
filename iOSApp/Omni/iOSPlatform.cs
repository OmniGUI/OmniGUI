using System;
using OmniGui;

namespace iOSApp.Omni
{
    public class iOSPlatform : IPlatform
    {
        public ITextEngine TextEngine { get; } = new iOSTextEngine();
        public IEventSource EventSource { get; } = new NullEventDriver();
        public IObservable<Layout> FocusedElement { get; }
        public void SetFocusedElement(Layout layout)
        {            
        }
    }
}