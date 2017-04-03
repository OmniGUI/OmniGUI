using OmniXaml;
using OmniXaml.Rework;

namespace OmniGui.Xaml
{
    public class OmniGuiValuePipeline : ValuePipeline
    {
        public OmniGuiValuePipeline(IValuePipeline inner) : base(inner)
        {
        }

        protected override void HandleCore(object parent, Member member, MutablePipelineUnit mutable)
        {
            var bindDefinition = mutable.Value as BindDefinition;
            if (bindDefinition != null)
            {
                mutable.Handled = true;
            }
        }
    }
}