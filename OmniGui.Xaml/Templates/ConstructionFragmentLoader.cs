namespace OmniGui.Xaml.Templates
{
    using OmniXaml;
    using OmniXaml.Metadata;

    public class ConstructionFragmentLoader : IConstructionFragmentLoader
    {
        public object Load(ConstructionNode node, INodeToObjectBuilder builder)
        {
            return new TemplateContent(node, builder);
        }
    }
}