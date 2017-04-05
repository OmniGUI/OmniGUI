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
            var s = StateTriggers.Connect();

            var observer = s.Filter(trigger => trigger.IsActive)
                .Any()
                .DistinctUntilChanged()
                .Subscribe(_ => ApplySetters());

        }

        private void ApplySetters()
        {            
        }

        public SourceList<StateTrigger> StateTriggers { get; } = new SourceList<StateTrigger>();
        public SetterCollection Setters { get; set; }
    }
}