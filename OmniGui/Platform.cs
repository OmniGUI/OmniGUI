namespace OmniGui
{
    public class Platform
    {
        public IEventSource EventSource { get; }
        public IRenderSurface RenderSurface { get; }
        public ITextEngine TextEngine { get; }

        public Platform(IEventSource eventSource, IRenderSurface renderSurface, ITextEngine textEngine)
        {
            EventSource = eventSource;
            RenderSurface = renderSurface;
            TextEngine = textEngine;
        }
    }
}