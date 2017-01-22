namespace OmniGui
{
    public class Platform
    {
        public ITextEngine TextEngine { get; set; }
        public static Platform Current { get; set; } = new Platform();
    }
}