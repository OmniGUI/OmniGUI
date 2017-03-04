namespace OmniGui.VisualStates
{
    using Zafiro.PropertySystem.Standard;

    public class StateTrigger
    {
        private readonly IExtendedPropertyEngine propertyEngine;
        public static ExtendedProperty IsActiveProperty;

        public StateTrigger(IExtendedPropertyEngine propertyEngine)
        {
            this.propertyEngine = propertyEngine;
            IsActiveProperty = propertyEngine.RegisterProperty("IsActive", typeof(StateTrigger), typeof(bool),
                new PropertyMetadata());
        }
        public bool IsActive
        {
            get => (bool) propertyEngine.GetValue(IsActiveProperty, this);
            set => propertyEngine.SetValue(IsActiveProperty, this, value);
        }
    }
}