namespace OmniGui.Xaml.Templates
{
    using OmniXaml;

    public class TemplateContent
    {
        private readonly ConstructionNode node;
        private readonly IObjectBuilder builder;
        private readonly BuildContext trackingContext;

        public TemplateContent(ConstructionNode node, IObjectBuilder builder, BuildContext trackingContext)
        {
            this.node = node;
            this.builder = builder;
            this.trackingContext = trackingContext;
        }

        public Layout LoadFor(Layout layout)
        {
            trackingContext.Bag.Add("TemplatedParent", layout);
            var loaded = Load();
            trackingContext.Bag.Remove("TemplatedParent");
            return loaded;
        }

        public Layout Load()
        {
            return (Layout)builder.Inflate(node, trackingContext);
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