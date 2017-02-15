namespace OmniGui.Wpf
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Point = Geometry.Point;

    public class WpfEventProcessor : IEventProcessor
    {
        private readonly FrameworkElement inputElement;

        public WpfEventProcessor(FrameworkElement inputElement)
        {
            this.inputElement = inputElement;
            Pointer = GetPointerObservable(inputElement);
            TextInput = GetKeyboardObservable(inputElement);
            KeyInput = GetKeyInputObservable(inputElement);
        }

        public IObservable<TextInputArgs> TextInput { get; }

        public IObservable<Point> Pointer { get; }

        public IObservable<KeyInputArgs> KeyInput { get; }

        public void Invalidate()
        {
            Application.Current.Dispatcher.Invoke(() => inputElement.InvalidateVisual(), DispatcherPriority.Render);
        }

        private IObservable<KeyInputArgs> GetKeyInputObservable(FrameworkElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<TextCompositionEventHandler, TextCompositionEventArgs>(
                ev => element.PreviewTextInput += ev,
                ev => element.PreviewTextInput -= ev);
            return fromEventPattern.Select(ep => new KeyInputArgs {Text = ep.EventArgs.Text});
        }

        private static IObservable<TextInputArgs> GetKeyboardObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<TextCompositionEventHandler, TextCompositionEventArgs>(
                ev => element.PreviewTextInput += ev,
                ev => element.PreviewTextInput -= ev);
            return fromEventPattern.Select(ep => new TextInputArgs(ep.EventArgs.Text));
        }

        private static IObservable<Point> GetPointerObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                ev => element.PreviewMouseLeftButtonDown += ev,
                ev => element.PreviewMouseLeftButtonDown -= ev);
            return fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(element);
                return new Point(position.X, position.Y);
            });
        }
    }
}