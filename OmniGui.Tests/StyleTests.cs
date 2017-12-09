using System.Collections.Generic;
using OmniGui.Layouts;
using OmniXaml;
using Xunit;

namespace OmniGui.Tests
{
    public class StyleTests
    {
        [Fact]
        public void Watch()
        {
            var converter = new ComponentModelTypeConverterBasedSourceValueConverter();

            var styleWatcher = new StyleWatcher(new List<Style>()
            {
                new Style() {Selector = "Button", Setters = new List<Setter>()
                {
                    new Setter(converter) {  PropertyName = "VerticalAlignment", Value = "Center"},
                }},
                new Style() {Selector = "Button:hover", Setters = new List<Setter>()
                {
                    new Setter(converter) {  PropertyName = "VerticalAlignment", Value = "Bottom"},
                }},
            });

            var button = new Button(new TestingPlatform());
            styleWatcher.Watch(button);
            button.Style = "Button";
            Assert.Equal(VerticalAlignment.Center, button.VerticalAlignment);
            button.Style = "Button:hover";
            Assert.Equal(VerticalAlignment.Bottom, button.VerticalAlignment);
        }
    }
}