namespace OmniGui.Wpf
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Point = Point;

    public class WpfEventProcessor : IEventProcessor
    {
        private readonly IFrameworkInputElement inputElement;

        public WpfEventProcessor(IFrameworkInputElement inputElement)
        {
            this.inputElement = inputElement;
            var fromEventPattern = Observable.FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                ev => inputElement.PreviewMouseLeftButtonDown += ev,
                ev => inputElement.PreviewMouseLeftButtonDown -= ev);
            Pointer = fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(inputElement);
                return new Point(position.X, position.Y);
            });

        }
        public IObservable<Point> Pointer { get; }
    }
}