namespace OmniGui
{
    public class FrameworkDependencies
    {
        public IEventSource EventSource { get; }
        public IRenderSurface RenderSurface { get; }
        public ITextEngine TextEngine { get; }

        public FrameworkDependencies(IEventSource eventSource, IRenderSurface renderSurface, ITextEngine textEngine)
        {
            EventSource = eventSource;
            RenderSurface = renderSurface;
            TextEngine = textEngine;
        }
    }
}