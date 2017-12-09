using System;
using System.Collections.Generic;
using System.Linq;

namespace OmniGui
{
    public class StyleWatcher
    {
        private IEnumerable<Style> styles;

        public StyleWatcher(IEnumerable<Style> styles)
        {
            this.styles = styles;
        }

        public void Watch(Layout layout)
        {
            var selectorChanged = layout.GetChangedObservable(Layout.StyleProperty);
            selectorChanged.Subscribe(str => OnStyleChanged(layout, (string) str));
        }

        private void OnStyleChanged(Layout layout, string selector)
        {
            var style = styles.FirstOrDefault(s => s.IsApplicable(selector));

            style?.Apply(layout);
        }
    }
}