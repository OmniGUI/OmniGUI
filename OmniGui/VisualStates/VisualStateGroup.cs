namespace OmniGui.VisualStates
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using DynamicData;

    public class VisualStateGroup
    {
        public VisualStateGroup()
        {
            var observableTriggers = StateTriggers.Connect();

            var setterToggler = observableTriggers
                .Filter(trigger => trigger.IsActive)
                .Any()
                .DistinctUntilChanged()
                .Subscribe(isTriggetActive => ToggleSetters(isTriggetActive));
        }

        private void ToggleSetters(bool shouldActivate)
        {            
        }

        public SourceList<StateTrigger> StateTriggers { get; } = new SourceList<StateTrigger>();
        public SetterCollection Setters { get; set; }
    }
}