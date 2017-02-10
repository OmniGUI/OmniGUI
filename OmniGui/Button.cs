namespace OmniGui
{
    using System;
    using System.Windows.Input;

    public class Button : ContentLayout
    {
        private readonly TextBlock textBlock;

        public string Text
        {
            get { return textBlock.Text; }
            set { textBlock.Text = value; }
        }

        public Button()
        {
            textBlock = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            this.Children.Add(new Border()
            {
                Child = textBlock,
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