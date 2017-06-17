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

        public IObservable<Point> Pointer { get; }
        public IObservable<TextInputArgs> TextInput { get; }
        public IObservable<KeyArgs> KeyInput { get; }

        private IObservable<Point> GetPointerObservable()
        {
            var fromEventPattern = Observable.FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                ev => control.Tapped += ev,
                ev => control.Tapped -= ev);
            var observable = fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(control);
                return new Point(position.X, position.Y);
            });
            return observable;
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
                .FromEventPattern<TypedEventHandler<CoreWindow, CharacterReceivedEventArgs>, CharacterReceivedEventArgs
                >(
                    ev => element.CharacterReceived += ev,
                    ev => element.CharacterReceived -= ev);
            return fromEventPattern.Select(
                ep => new TextInputArgs {Text = new string(new[] {(char) ep.EventArgs.KeyCode})});
        }
    }
}