using OmniGui.Xaml.Templates;

namespace OmniGui.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    public class Inflator
    {
        public static void Inflate(Layout layout, ControlTemplate template)
        {
            layout.AddChild(template.BuildFor(layout));
        }

        public void Inflate(Layout layout, ICollection<ControlTemplate> controlTemplates)
        {
            Apply(controlTemplates, layout);

            foreach (var child in layout.Children)
            {
                Apply(controlTemplates, child);
            }
        }

        private static void Apply(IEnumerable<ControlTemplate> controlTemplates, Layout child)
        {
            var applicable = controlTemplates.SingleOrDefault(template => template.Target == child.GetType().Name);
            if (applicable != null)
            {
                Inflate(child, applicable);
            }
        }
    }
}