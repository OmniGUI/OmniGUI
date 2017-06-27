using OmniGui.Default;

namespace OmniGui.Tests
{
    public class TestingPlatform : Platform
    {
        public TestingPlatform() : base((IEventSource) new DefaultEventSource(), (IRenderSurface) new DefaultRenderSurface(), (ITextEngine) new DefaultTextEngine())
        {
        }
    }
}