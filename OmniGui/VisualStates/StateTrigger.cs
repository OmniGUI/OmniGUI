namespace OmniGui.VisualStates
{
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class StateTrigger
    {
        private readonly IPropertyEngine propertyEngine;

        public static readonly ExtendedProperty IsActiveProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty(
            "IsActive", typeof(StateTrigger), typeof(bool),
            new PropertyMetadata() {DefaultValue = false});

        public bool IsActive
        {
            get => (bool)OmniGuiPlatform.PropertyEngine.GetValue(IsActiveProperty, this);
            set => OmniGuiPlatform.PropertyEngine.SetValue(IsActiveProperty, this, value);
        }
    }
}