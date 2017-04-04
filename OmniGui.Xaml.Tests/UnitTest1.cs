using System.Collections.Generic;
using System.Reflection;
using OmniGui.Layouts;
using OmniXaml.Rework;
using Xunit;
using OmniXaml;
using OmniXaml.Services;
using OmniGui.Xaml.Rework;
using OmniGui.Xaml.Templates;

namespace OmniGui.Xaml.Tests
{
    public class Class1
    {
        [Fact]
        public void BindTest()
        {
            var converter = new TypeConverterSourceValueConverter();
            var omniGuiInstanceCreator = new Rework.OmniGuiInstanceCreator(converter, new TypeLocator(() => new List<ControlTemplate>(), new List<Assembly>()));
            var valuePipeline = new OmniGuiValuePipeline(new MarkupExtensionValuePipeline(new NoActionValuePipeline()));
            var assignmentApplier = new OmniGuiMemberAssignmentApplier(converter, valuePipeline);
            var sut = new OmniGuiObjectBuilder(omniGuiInstanceCreator, converter, assignmentApplier);

            var node = new ConstructionNode(typeof(Button))
            {
                Assignments = new List<MemberAssignment>
                {
                   new MemberAssignment
                   {
                       Member = Member.FromStandard<Button>( b => b.Content),
                       Children = new List<ConstructionNode>()
                       {
                           new ConstructionNode(typeof(Bind))
                           {
                               Assignments = new List<MemberAssignment>()
                               {
                                   new MemberAssignment()
                                   {
                                       Member = Member.FromStandard<Bind>(b => b.TargetProperty),
                                       Children = ConstructionNode.ForString("SomeProperty")
                                   }
                               }
                           }

                       }
                   }
                }
            };

            var instance = sut.Inflate(node);
        }
    }
}