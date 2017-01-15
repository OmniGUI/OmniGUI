using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AnotherTry
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(double.PositiveInfinity, 300, 300)]
        [InlineData(10, 300, 300)]
        [InlineData(double.PositiveInfinity, double.NaN, 0)]
        public void SingleStackPanel(double available, double request, double desired)
        {
            var stackPanel = new StackPanel { WidthRequest = request };
            stackPanel.Measure(available);
            Assert.Equal(desired, stackPanel.DesiredSize);
        }

        [Theory]
        [InlineData(10, 300, 400, 300)]
        [InlineData(10, double.NaN, 400, 400)]
        [InlineData(10, double.NaN, double.NaN, 0)]
        public void StackPanelWithChild(double available, double request, double childRequest, double desired)
        {
            var stackPanel = new StackPanel
            {
                WidthRequest = request,
                Children = new List<StackPanel>
                {
                    new StackPanel { WidthRequest = childRequest, }
                }
            };
            stackPanel.Measure(available);
            Assert.Equal(desired, stackPanel.DesiredSize);
        }

        [Theory]
        [InlineData(10, double.NaN, 100, 4, 400)]
        public void StackPanelWithNChild(double available, double request, double childRequest, int childCount, double desired)
        {
            var stackPanel = new StackPanel
            {
                WidthRequest = request,
                Children = Enumerable.Range(0, childCount).Select(_ => new StackPanel { WidthRequest = childRequest }).ToList()
            };
            stackPanel.Measure(available);
            Assert.Equal(desired, stackPanel.DesiredSize);
        }
    }

    public class StackPanel
    {
        public double WidthRequest { get; set; }
        public double DesiredSize { get; set; }
        public IList<StackPanel> Children { get; set; } = new List<StackPanel>();

        public void Measure(double availableSize)
        {
            if (!double.IsNaN(WidthRequest))
            {
                DesiredSize = WidthRequest;
                return;
            }

            DesiredSize = 0;

            foreach (var child in Children)
            {
                child.Measure(availableSize);
                DesiredSize += child.DesiredSize;
            }                          
        }
    }
}
