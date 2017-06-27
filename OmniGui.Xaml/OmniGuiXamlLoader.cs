using System;

namespace OmniGui.Xaml
{
    using OmniXaml;
    using System.Collections.Generic;
    using System.Reflection;
    using OmniXaml.Services;

    public class OmniGuiXamlLoader : ExtendedXamlLoader
    {

        public OmniGuiXamlLoader(IList<Assembly> assemblies, ITypeLocator locator, Func<StyleWatcher> styleWatcherSelector) : base(assemblies)
        {
            InstanceCreator = new OmniGuiInstanceCreator(locator, styleWatcherSelector);
        }

        public override IInstanceCreator InstanceCreator { get; } 
        protected override IValuePipeline ValuePipeline { get; } = new OmniGuiValuePipeline(new MarkupExtensionValuePipeline(new NoActionValuePipeline()));
    }
}