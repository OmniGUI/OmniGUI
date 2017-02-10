namespace OmniGui.Xaml.Templates
{
    using OmniXaml;
    using OmniXaml.Metadata;

    public class ConstructionFragmentLoader : IConstructionFragmentLoader
    {
        public object Load(ConstructionNode node, IObjectBuilder builder, BuildContext trackingContext)
        {
            return new TemplateContent(node, builder, trackingContext);
        }
    }
}