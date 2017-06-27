using OmniGui.Geometry;

namespace OmniGui.Layouts
{
    using System;
    using System.Windows.Input;
    using Zafiro.PropertySystem.Standard;

    public class Button : ContentLayout
    {
        public static readonly ExtendedProperty CommandProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Command", typeof(Button), typeof(ICommand), new PropertyMetadata());

        public Button(Platform platform) : base(platform)
        {
            Pointer.Enter.Subscribe(_ => Style = "Button:hover");
            Pointer.Leave.Subscribe(_ => Style = "Button");
            Pointer.Up.Subscribe(_ => Style = GetType().Name);
            Pointer.Down.Subscribe(p =>
            {
                this.Style = "Button:click";

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