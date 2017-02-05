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
            SetPointer(inputElement);
            SetKeyboard(inputElement);
        }

        private void SetKeyboard(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<TextCompositionEventHandler, TextCompositionEventArgs>(
    ev => element.PreviewTextInput += ev,
    ev => element.PreviewTextInput -= ev);
            TextInput = fromEventPattern.Select(ep => new TextInputArgs(ep.EventArgs.Text));
        }

        public IObservable<TextInputArgs> TextInput { get; set; }

        private void SetPointer(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                ev => element.PreviewMouseLeftButtonDown += ev,
                ev => element.PreviewMouseLeftButtonDown -= ev);
            Pointer = fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(element);
                return new Point(position.X, position.Y);
            });
        }

        public IObservable<Point> Pointer { get; private set; }

        public void Invalidate()
        {
            Application.Current.Dispatcher.Invoke(() => inputElement.InvalidateVisual(), DispatcherPriority.Render);
        }     
    }
}