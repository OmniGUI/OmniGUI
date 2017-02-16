namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using Templates;

    public interface ITemplateInflator
    {
        void Inflate(Layout layout, ICollection<ControlTemplate> controlTemplates);
    }
}