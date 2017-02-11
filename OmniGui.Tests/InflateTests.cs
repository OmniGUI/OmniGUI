namespace OmniGui.Tests
{
    using System.Collections.Generic;
    using System.Reflection;
    using Xaml.Templates;
    using Xaml;
    using Xunit;

    public class InflateTests
    {
        [Fact]
        public void Inflate1()
        {
            var layout = new Inflatable();
            var inflator = new Inflator();
            var omniGuiXamlLoader = GetLoader();
            var controlTemplate = (ControlTemplate) omniGuiXamlLoader.Load(@"<ControlTemplate xmlns=""root""><Border><TextBlock Text=""{TemplateBind Text}"" /></Border></ControlTemplate>").Instance;

            inflator.Inflate(layout, controlTemplate);
        }

        private static OmniGuiXamlLoader GetLoader()
        {
            var omniGuiXamlLoader = new OmniGuiXamlLoader(new List<Assembly>
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("OmniGui.Tests"))
            });
            return omniGuiXamlLoader;
        }
    }
}