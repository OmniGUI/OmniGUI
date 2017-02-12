namespace OmniGui.Tests
{
    using OmniXaml.Attributes;
    using Space;
    using Xunit;

    public class StackPanelTests
    {

        [Fact]
        public void First()
        {
            AssertSize(new StackPanel(), Size.Zero);
        }

        [Fact(Skip = "")]
        public void Second()
        {
            AssertSize(new StackPanel() { RequestedSize = Size.OnlyHeight(100) }, Size.HeightAndZero(100));
        }

        [Fact(Skip = "")]
        public void Third()
        {
            AssertSize(new StackPanel() { RequestedSize = Size.OnlyWidth(100) }, Size.WidthAndZero(100));
        }

        [Fact(Skip="")]
        public void WidthChild()
        {
            var stackPanel = new StackPanel()
            {
                RequestedSize = Size.NoneSpecified,
            }.AddChild(new StackPanel { RequestedSize = Size.OnlyHeight(100) });


            AssertSize(stackPanel, Size.HeightAndZero(100));
        }

        [Fact(Skip = "")]
        public void WidthChild2()
        {
            var stackPanel = new StackPanel()
                {
                    RequestedSize = Size.NoneSpecified,
                }
                .AddChild(new StackPanel() {RequestedSize = Size.OnlyHeight(100)})
                .AddChild(new StackPanel {RequestedSize = Size.OnlyHeight(200)});

            AssertSize(stackPanel, Size.HeightAndZero(300));
        }

        private static void AssertSize(Layout panel, Size desiredSize)
        {
            var avalialable = new Size(20, 10);

            var stackPanel = panel;
            stackPanel.Measure(avalialable);
            Assert.Equal(desiredSize, stackPanel.DesiredSize);
        }


    }
}
