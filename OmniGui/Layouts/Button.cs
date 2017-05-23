using OmniGui.Geometry;

namespace OmniGui.Layouts
{
    using System;
    using System.Windows.Input;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class Button : ContentLayout
    {
        public static ExtendedProperty CommandProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Command", typeof(Button), typeof(ICommand), new PropertyMetadata());

        public Button(IPropertyEngine propertyEngine) : base(propertyEngine)
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

        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawRoundedRectangle(this.VisualBounds, new Pen(new Brush(Colors.Gray), 1), new CornerRadius(6));
            drawingContext.DrawText(new FormattedText()
            {
                Text = Content.ToString(),
                Brush = new Brush(Colors.Black),
                Constraint = Size.Infinite, FontSize = 15
            }, this.VisualBounds.Point);
            base.Render(drawingContext);
        }
    }
}