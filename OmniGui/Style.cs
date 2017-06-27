using System.Collections.Generic;
using OmniXaml.Attributes;

namespace OmniGui
{
    public class Style
    {
        public string Selector { get; set; }

        [Content]
        public List<NewSetter> Setters { get; set; } = new List<NewSetter>();

        public bool IsApplicable(string selector)
        {
            return selector.Equals(Selector);
        }

        public void Apply(Layout layout)
        {
            foreach (var setter in Setters)
            {
                setter.Apply(layout);
            }
        }
    }
}