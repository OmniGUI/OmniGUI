namespace OmniGui.Wpf
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Point = Point;

    public class WpfEventProcessor : IEventProcessor
    {
        private readonly FrameworkElement inputElement;

        public WpfEventProcessor(FrameworkElement inputElement)
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
        public void Invalidate()
        {
            Application.Current.Dispatcher.Invoke(() => inputElement.InvalidateVisual(), DispatcherPriority.Render);
        }     
    }
}