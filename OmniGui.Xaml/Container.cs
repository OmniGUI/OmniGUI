namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using Templates;

    public class Container
    {
        public ICollection<ControlTemplate> ControlTemplates { get; set; } = new List<ControlTemplate>();
        public ICollection<Style> Styles { get; set; } = new List<Style>();
    }
}