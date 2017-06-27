namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using Templates;

    public class ResourceStore
    {
        public ICollection<Style> Styles { get; set; } = new List<Style>();
        public ICollection<ControlTemplate> ControlTemplates { get; } = new List<ControlTemplate>();
        private ITemplateInflator Inflator => new TemplateInflator();

        public void Inflate(Layout layoutToInflate)
        {
            Inflator.Inflate(layoutToInflate, ControlTemplates);
        }
    }
}