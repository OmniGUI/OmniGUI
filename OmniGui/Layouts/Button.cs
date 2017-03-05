namespace OmniGui.Layouts
{
    using System;
    using System.Windows.Input;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class Button : ContentLayout
    {
        public static ExtendedProperty CommandProperty;

        public Button(IPropertyEngine propertyEngine) : base(propertyEngine)
        {
            RegistrationGuard.RegisterFor<Button>(() =>
            {
                CommandProperty = PropertyEngine.RegisterProperty("Command", typeof(Button), typeof(ICommand), new PropertyMetadata());
            });

            Pointer.Down.Subscribe(p =>
            {
                if (Command?.CanExecute(null) == true)
                {
                    Command.Execute(null);
                }
            });
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}