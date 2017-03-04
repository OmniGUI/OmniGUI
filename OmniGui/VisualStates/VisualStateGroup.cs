namespace OmniGui.VisualStates
{
    using System.Collections.Generic;

    public class VisualStateGroup
    {
        public ICollection<StateTrigger> StateTriggers { get; set; } = new List<StateTrigger>();
    }
}