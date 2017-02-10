namespace OmniGui.Tests
{
    using OmniGui.Xaml.Templates;
    using Xunit;

    public class InflateTests
    {
        [Fact]
        public void Inflate1()
        {
            var inflatable = new Inflatable();
            var inflator = new Inflator();
            var inflateDef = new ControlTemplate();

            inflator.Inflate(inflatable, inflateDef);
        }
    }
}