using System.Linq;

namespace OmniGui.Xaml.Templates
{
    using OmniXaml;

    public class TemplateContent
    {
        private readonly ConstructionNode node;
        private readonly INodeToObjectBuilder builder;
        private readonly BuilderContext context;

        public TemplateContent(ConstructionNode node, INodeToObjectBuilder builder, BuilderContext context)
        {
            this.node = node;
            this.builder = builder;
            this.context = context;
        }

        public Layout LoadFor(Layout layout)
        {
            context.Store.Add("TemplateParent", layout);
            var loadFor = (Layout)builder.Build(node, context);
            context.Store.Remove("TemplateParent");
            return loadFor; 
        }

        public Layout Load()
        {
            var builderContext = new BuilderContext();
            return (Layout)builder.Build(node, builderContext);
        }

        protected bool Equals(TemplateContent other)
        {
            return Equals(node, other.node);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((TemplateContent) obj);
        }

        public override int GetHashCode()
        {
            return (node != null ? node.GetHashCode() : 0);
        }
    }
}