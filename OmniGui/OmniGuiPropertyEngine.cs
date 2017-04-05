namespace OmniGui
{
    using Zafiro.PropertySystem;

    public class OmniGuiPropertyEngine : PropertyEngine
    {
        public OmniGuiPropertyEngine() : base(o => ((IChild)o).Parent)
        {
        }
    }
}