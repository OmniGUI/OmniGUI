namespace OmniGui.VisualStates
{
    using System;

    public class VisualStateGroup
    {

        public VisualStateGroup()
        {
            StateTriggers = new TriggerCollection();
            StateTriggers.IsActive.Subscribe(ToggleSetters);
            Setters = new SetterCollection();
        }

        private void ToggleSetters(bool shouldActivate)
        {
            foreach (var setter in Setters)
            {
                setter.Apply();
            }
        }

        public TriggerCollection StateTriggers { get; }
        public SetterCollection Setters { get; set; }
    }
}