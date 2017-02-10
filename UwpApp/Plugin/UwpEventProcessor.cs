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
    using Microsoft.Graphics.Canvas.UI.Xaml;
    using Point = OmniGui.Point;

    public class UwpEventProcessor : IEventProcessor
    {
        private readonly FrameworkElement inputElement;
        private readonly CanvasControl canvas;

        public UwpEventProcessor(FrameworkElement inputElement, CanvasControl canvas)
        {
            this.inputElement = inputElement;
            this.canvas = canvas;
            Pointer = GetPointerObservable(inputElement);
            KeyInput = GetKeyInputObservable();
        }

        public IObservable<KeyInputArgs> KeyInput { get; set; }

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
            Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => canvas.Invalidate());
        }

        public IObservable<TextInputArgs> TextInput => new Subject<TextInputArgs>();
    }
}