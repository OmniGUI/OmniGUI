using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using Point = OmniGui.Geometry.Point;

namespace OmniGui.Wpf
{
    public class WpfEventSource : IEventSource
    {
        private readonly FrameworkElement inputElement;

        public WpfEventSource(FrameworkElement inputElement)
        {
            this.inputElement = inputElement;
            Pointer = GetPointerObservable(inputElement);
            TextInput = GetKeyInputObservable(inputElement);
            KeyInput = GetSpecialKeysObservable(inputElement);
            ScrollWheel = GetPointerScrollWheelObservable(inputElement);
        }

        public IObservable<PointerInput> Pointer { get; }

        public IObservable<TextInputArgs> TextInput { get; }
        public IObservable<KeyArgs> KeyInput { get; }
        public IObservable<ScrollWheelArgs> ScrollWheel { get; }

        private IObservable<ScrollWheelArgs> GetPointerScrollWheelObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<MouseWheelEventHandler, MouseWheelEventArgs>(
                ev => element.PreviewMouseWheel += ev,
                ev => element.PreviewMouseWheel -= ev);

            return fromEventPattern.Select(ep => new ScrollWheelArgs {Delta = ep.EventArgs.Delta});
        }

        private static IObservable<KeyArgs> GetSpecialKeysObservable(IInputElement element)
        {
            var fromEventPattern = Observable.FromEventPattern<KeyEventHandler, KeyEventArgs>(
                ev => element.PreviewKeyDown += ev,
                ev => element.PreviewKeyDown -= ev);

            return fromEventPattern.Select(ep => new KeyArgs(ep.EventArgs.Key.ToOmniGui()));
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

        private static IObservable<PointerInput> GetPointerObservable(IInputElement element)
        {
            var down = Observable.FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                    ev => element.PreviewMouseLeftButtonDown += ev,
                    ev => element.PreviewMouseLeftButtonDown -= ev)
                .Select(pattern =>
                {
                    var position = pattern.EventArgs.GetPosition(element);
                    var point = new Point(position.X, position.Y);
                    return new PointerInput {Point = point, PrimaryButtonStatus = PointerStatus.Down};
                });

            var up = Observable.FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                    ev => element.PreviewMouseLeftButtonUp += ev,
                    ev => element.PreviewMouseLeftButtonUp -= ev)
                .Select(pattern =>
                {
                    var position = pattern.EventArgs.GetPosition(element);
                    var point = new Point(position.X, position.Y);
                    return new PointerInput {Point = point, PrimaryButtonStatus = PointerStatus.Up};
                });

            var hover = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                    ev => element.PreviewMouseMove += ev,
                    ev => element.PreviewMouseMove -= ev)
                .Select(pattern =>
                {
                    var position = pattern.EventArgs.GetPosition(element);
                    var point = new Point(position.X, position.Y);
                    return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Released };
                });

            return down.Merge(up).Merge(hover);
        }
    }
}