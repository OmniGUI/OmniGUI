namespace OmniGui.Wpf
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Point = Geometry.Point;

    public class WpfEventSource : IEventSource
    {
        private readonly FrameworkElement inputElement;

        public WpfEventSource(FrameworkElement inputElement)
        {
            this.inputElement = inputElement;
            Pointer = GetPointerObservable(inputElement);
            TextInput = GetKeyInputObservable(inputElement);
            KeyInput = GetSpecialKeysObservable(inputElement);
        }

        private static IObservable<KeyArgs> GetSpecialKeysObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<KeyEventHandler, KeyEventArgs>(
                ev => element.PreviewKeyDown += ev,
                ev => element.PreviewKeyDown -= ev);

            return fromEventPattern.Select(ep => new KeyArgs(ep.EventArgs.Key.ToOmniGui()));
        }

        public IObservable<Point> Pointer { get; }

        public IObservable<TextInputArgs> TextInput { get; }
        public IObservable<KeyArgs> KeyInput { get; }

        public void Invalidate()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                inputElement.InvalidateVisual();
            }, DispatcherPriority.Render);
        }

        public void ShowVirtualKeyboard()
        {            
        }

        private static IObservable<TextInputArgs> GetKeyInputObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<TextCompositionEventHandler, TextCompositionEventArgs>(
                ev => element.PreviewTextInput += ev,
                ev => element.PreviewTextInput -= ev);
            return fromEventPattern
                .Where(ep => ep.EventArgs.Text.ToCharArray().First() != Chars.Backspace)
                .Select(ep => new TextInputArgs {Text = ep.EventArgs.Text});
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