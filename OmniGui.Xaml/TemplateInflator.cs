namespace OmniGui.Xaml
{
    using System.Collections.Generic;
    using System.Linq;
    using Templates;

    public class TemplateInflator
    {
        public static void Inflate(Layout layout, ICollection<ControlTemplate> controlTemplates)
        {
            Apply(controlTemplates, layout);

            var originalChildren = layout.Children.ToList();

            foreach (var child in originalChildren)
            {
                Inflate(child, controlTemplates);
            }
        }

        private static void Apply(IEnumerable<ControlTemplate> controlTemplates, Layout child)
        {
            var controlTemplate = controlTemplates.SingleOrDefault(template => template.Target == child.GetType().Name);
            if (controlTemplate != null)
            {
                child.AddChild(controlTemplate.ApplyTo(child));
            }
        }
    }
}