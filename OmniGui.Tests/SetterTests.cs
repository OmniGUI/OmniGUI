using OmniGui.Layouts;
using OmniGui.VisualStates;
using OmniGui.Xaml;
using OmniXaml;
using Xunit;

namespace OmniGui.Tests
{
    public class SetterTests
    {
        [Fact]
        public void CompatibleValue()
        {
            var sut = new Setter(new ComponentModelTypeConverterBasedSourceValueConverter());
            sut.PropertyName = "Content";
            sut.Value = "Pepito";
            var instance = new Button(new TestingPlatform());
            sut.Apply(instance);
            Assert.Equal("Pepito", instance.Content);
        }

        [Fact]
        public void IncompatibleValue()
        {
            var sut = new Setter(new ComponentModelTypeConverterBasedSourceValueConverter())
            {
                PropertyName = "VerticalAlignment",
                Value = "Top"
            };

            var instance = new Button(new TestingPlatform());
            sut.Apply(instance);
            Assert.Equal(VerticalAlignment.Top, instance.VerticalAlignment);
        }
    }
}