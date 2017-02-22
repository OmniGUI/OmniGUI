using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using OmniGui;

namespace UwpApp.Plugin
{
    using Windows.UI.ViewManagement;
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using Microsoft.Toolkit.Uwp;
    using Point = OmniGui.Geometry.Point;

    public class UwpEventSource : IEventSource
    {
        private readonly FrameworkElement inputElement;
        private readonly CanvasControl canvas;

        public UwpEventSource(FrameworkElement inputElement, CanvasControl canvas)
        {
            this.inputElement = inputElement;
            this.canvas = canvas;
            Pointer = GetPointerObservable(inputElement);
            KeyInput = GetKeyInputObservable();
            SpecialKeys = GetSpecialKeysObservable();
        }

        private IObservable<SpecialKeysArgs> GetSpecialKeysObservable()
        {
            var element = Window.Current.CoreWindow;

            var fromEventPattern = Observable.FromEventPattern<TypedEventHandler<CoreWindow, KeyEventArgs>, KeyEventArgs>(
                ev => element.KeyDown += ev,
                ev => element.KeyDown -= ev);
            return fromEventPattern.Select(ep => new SpecialKeysArgs(ep.EventArgs.VirtualKey.ToOmniGui()));
        }

        public IObservable<KeyInputArgs> KeyInput { get; }
        public IObservable<SpecialKeysArgs> SpecialKeys { get; }

        private static IObservable<KeyInputArgs> GetKeyInputObservable()
        {
            var element = Window.Current.CoreWindow;

            var fromEventPattern = Observable.FromEventPattern<TypedEventHandler<CoreWindow, CharacterReceivedEventArgs>, CharacterReceivedEventArgs>(
                             ev => element.CharacterReceived += ev,
                             ev => element.CharacterReceived -= ev);
            return fromEventPattern.Select(ep => new KeyInputArgs() { Text = new string(new[] { (char)ep.EventArgs.KeyCode }) });
        }

        private static IObservable<Point> GetPointerObservable(FrameworkElement inputElement)
        {
            var fromEventPattern = Observable.FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                ev => inputElement.Tapped += ev,
                ev => inputElement.Tapped -= ev);
            var observable = fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(inputElement);
                return new Point(position.X, position.Y);
            });
            return observable;
        }

        public IObservable<Point> Pointer { get; }
        public void Invalidate()
        {
            DispatcherHelper.ExecuteOnUIThreadAsync(() => canvas.Invalidate());
        }

        public void ShowVirtualKeyboard()
        {
            InputPane pane = InputPane.GetForCurrentView();
            pane.TryShow();
        }

        public IObservable<TextInputArgs> TextInput => new Subject<TextInputArgs>();
    }
}