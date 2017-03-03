namespace OmniGui.VisualStates
{
    using System.Collections.Generic;
    using Zafiro.Core;

    public class VisualStateManager
    {
        private static IDictionary<Layout, ICollection<VisualStateGroup>> GroupsByLayout { get; } =
            new Dictionary<Layout, ICollection<VisualStateGroup>>();

        public static void SetVisualStateGroups(ICollection<VisualStateGroup> groups, Layout instance)
        {
            GroupsByLayout.Add(instance, groups);
        }
        public static ICollection<VisualStateGroup> GetVisualStateGroups(Layout instance)
        {
            return GroupsByLayout.GetCreate(instance, () => new List<VisualStateGroup>());
        }
    }
}