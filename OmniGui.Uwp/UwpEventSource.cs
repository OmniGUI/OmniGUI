using System;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Point = OmniGui.Geometry.Point;

namespace OmniGui.Uwp
{
    public class UwpEventSource : IEventSource
    {
        private readonly Control control;

        public UwpEventSource(Control control)
        {
            this.control = control;
            Pointer = GetPointerObservable();
            KeyInput = GetKeysObservable();
            TextInput = GetTextInput();
        }

        public IObservable<PointerInput> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; }
        public IObservable<KeyArgs> KeyInput { get; }
        public IObservable<ScrollWheelArgs> ScrollWheel { get; } = Observable.Never<ScrollWheelArgs>();

        private IObservable<PointerInput> GetPointerObservable()
        {
            var pressed = Observable.FromEventPattern<PointerEventHandler, PointerRoutedEventArgs>(
                ev => control.PointerPressed += ev,
                ev => control.PointerPressed -= ev).Select(pattern =>
            {
                var pointerPoint = pattern.EventArgs.GetCurrentPoint(control);
                var point1 = new Point(pointerPoint.Position.X, pointerPoint.Position.Y);
                return new PointerInput {Point = point1, PrimaryButtonStatus = PointerStatus.Down};
            });

            var released = Observable.FromEventPattern<PointerEventHandler, PointerRoutedEventArgs>(
                ev => control.PointerReleased += ev,
                ev => control.PointerReleased -= ev).Select(pattern =>
            {
                var pointerPoint = pattern.EventArgs.GetCurrentPoint(control);
                var point = new Point(pointerPoint.Position.X, pointerPoint.Position.Y);
                return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Up };
            });

            var moved = Observable.FromEventPattern<PointerEventHandler, PointerRoutedEventArgs>(
                ev => control.PointerMoved += ev,
                ev => control.PointerMoved -= ev).Select(pattern =>
            {
                var pointerPoint1 = pattern.EventArgs.GetCurrentPoint(control);
                var point = new Point(pointerPoint1.Position.X, pointerPoint1.Position.Y);
                return new PointerInput { Point = point, PrimaryButtonStatus = PointerStatus.Released };
            });

            return pressed.Merge(released).Merge(moved);
        }

        private static IObservable<KeyArgs> GetKeysObservable()
        {
            var element = Window.Current.CoreWindow;

            var fromEventPattern = Observable
                .FromEventPattern<TypedEventHandler<CoreWindow, KeyEventArgs>, KeyEventArgs>(
                    ev => element.KeyDown += ev,
                    ev => element.KeyDown -= ev);
            return fromEventPattern.Select(ep => new KeyArgs(ep.EventArgs.VirtualKey.ToOmniGui()));
        }

        private static IObservable<TextInputArgs> GetTextInput()
        {
            var element = Window.Current.CoreWindow;

            var fromEventPattern = Observable
                .FromEventPattern<TypedEventHandler<CoreWindow, CharacterReceivedEventArgs>, CharacterReceivedEventArgs>(
                    ev => element.CharacterReceived += ev,
                    ev => element.CharacterReceived -= ev);
            return fromEventPattern.Select(
                ep => new TextInputArgs { Text = new string(new[] { (char)ep.EventArgs.KeyCode }) });
        }
    }
}