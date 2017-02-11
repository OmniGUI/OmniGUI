using OmniGui.Xaml.Templates;

namespace OmniGui.Tests
{
    public class Inflator
    {
        public void Inflate(Layout layout, ControlTemplate template)
        {
            layout.AddChild(template.BuildFor(layout));
        }
    }
}