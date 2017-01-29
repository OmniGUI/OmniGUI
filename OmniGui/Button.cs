namespace OmniGui
{
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
        }
    }
}