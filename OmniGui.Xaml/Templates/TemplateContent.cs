namespace OmniGui.Xaml.Templates
{
    using OmniXaml;

    public class TemplateContent
    {
        private readonly ConstructionNode node;
        private readonly BuildContext trackingContext;
        private readonly INodeToObjectBuilder builder;

        public TemplateContent(ConstructionNode node, INodeToObjectBuilder builder)
        {
            this.node = node;
            this.builder = builder;
        }

        public Layout LoadFor(Layout layout)
        {
            //trackingContext.Bag.Add("TemplatedParent", layout);
            var loaded = Load();
           // trackingContext.Bag.Remove("TemplatedParent");
            return loaded;
        }

        public Layout Load()
        {
            return (Layout)builder.Build(node);
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