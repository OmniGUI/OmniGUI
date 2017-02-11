namespace OmniGui
{
    using System;
    using System.Windows.Input;
    using Zafiro.PropertySystem.Standard;

    public class Button : ContentLayout
    {
        public readonly ExtendedProperty TextProperty = PropertyEngine.RegisterProperty("Text", typeof(Button), typeof(string), new PropertyMetadata());
        public readonly ExtendedProperty CommandProperty = PropertyEngine.RegisterProperty("Command", typeof(Button), typeof(ICommand), new PropertyMetadata());

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Button()
        {         
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