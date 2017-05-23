namespace OmniGui.Tests
{
    using System;
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

            var omniGuiPropertyEngine = new OmniGuiPropertyEngine();

            var visualStateGroup = new VisualStateGroup(omniGuiPropertyEngine);

            var textBlock = new TextBlock();


            visualStateGroup.Setters = new SetterCollection
            {
                new List<Setter>()
                {
                    new Setter()
                    {
                        Target = new SetterTarget(textBlock, Member.FromStandard<TextBlock>(block => block.Text)),
                        Value = "Pepito",
                    }
                }
            };

            visualStateGroup.StateTriggers.Add(new StateTrigger()
            {
                IsActive = true,
            });

            
            Assert.Equal(textBlock.Text, "Pepito");
        }

        [Fact]
        public void TestOriginal()
        {
            var omniGuiPropertyEngine = new OmniGuiPropertyEngine();

            var col = new TriggerCollection(omniGuiPropertyEngine);
            col.IsActive.Subscribe(o => { });
            var stateTrigger = new StateTrigger()
            {
                IsActive = false,
            };

            col.Add(stateTrigger);

            stateTrigger.IsActive = false;                      
        }

        private static List<StateTrigger> GetStateTriggers(OmniGuiPropertyEngine omniGuiPropertyEngine)
        {
            return new List<StateTrigger>
            {
                new StateTrigger()
                {
                    IsActive = true,
                },
            };
        }
    }
}