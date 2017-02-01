namespace OmniGui
{
    public class Platform
    {
        public ITextEngine TextEngine { get; set; }
        public static Platform Current { get; set; } = new Platform();
        public IEventProcessor EventDriver { get; set; } = new NullEventDriver();
    }
}