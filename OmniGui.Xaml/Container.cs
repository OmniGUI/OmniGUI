namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using Templates;

    public class Container
    {
        public ICollection<ControlTemplate> ControlTemplates { get; set; } = new List<ControlTemplate>();
    }
}