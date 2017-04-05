namespace OmniGui.Tests
{
    using Layouts;
    using OmniXaml;
    using System.Collections.ObjectModel;
    using DynamicData;
    using VisualStates;
    using Xunit;
    using VisualStateGroup = VisualStates.VisualStateGroup;
    using System.Collections.Generic;

    public class VisualStateGroupTests
    {
        [Fact]
        public void Test()
        {
            var visualStateGroup = new VisualStateGroup();
            var omniGuiPropertyEngine = new OmniGuiPropertyEngine();
            var stateTriggers = new List<StateTrigger>()
            {
                new StateTrigger(omniGuiPropertyEngine)
                {
                    IsActive = true,
                },
            };

            visualStateGroup.StateTriggers.AddRange(stateTriggers);        

            var textBlock = new TextBlock(omniGuiPropertyEngine);

            visualStateGroup.Setters = new SetterCollection()
            {
                Target = new SetterTarget(textBlock, Member.FromStandard<TextBlock>(block => block.Text)),
                Value = "Pepito",
            };

            visualStateGroup.StateTriggers.Clear();

            Assert.Equal(textBlock.Text, "Pepito");
        }
    }
}