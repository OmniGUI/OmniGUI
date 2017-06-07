namespace OmniGui
{
    public class FrameworkDependencies
    {
        public IEventSource EventSource { get; }
        public IRenderSurface RenderSurface { get; }

        public FrameworkDependencies(IEventSource eventSource, IRenderSurface renderSurface)
        {
            EventSource = eventSource;
            RenderSurface = renderSurface;
        }
    }
}