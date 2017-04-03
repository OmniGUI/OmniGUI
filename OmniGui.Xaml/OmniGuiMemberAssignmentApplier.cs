using OmniXaml;
using OmniXaml.Rework;
using OmniXaml.ReworkPhases;

namespace OmniGui.Xaml
{
    public class OmniGuiMemberAssignmentApplier : MemberAssigmentApplier
    {
        public OmniGuiMemberAssignmentApplier(IStringSourceValueConverter converter, IValuePipeline pipeline) : base(converter, pipeline)
        {
        }
    }
}