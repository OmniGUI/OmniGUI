namespace OmniGui
{
    using System;
    using System.Windows.Input;

    public class Button : ContentLayout
    {
        public Button()
        {
            this.Children.Add(new Border()
            {
                Child = new TextBlock
                {
                    Text = "Botoncillo",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                },
                Padding = new Thickness(4),
                Background = new Brush(Colors.LightGray),
                BorderThickness = 2,
                CornerRadius = 5,
                BorderBrush = new Brush(Colors.Black)
            });

            Pointer.Down.Subscribe(p =>
            {
                if (Command?.CanExecute(null) == true)
                {
                    Command.Execute(null);
                }
            });
        }

        public ICommand Command { get; set; }
    }
}