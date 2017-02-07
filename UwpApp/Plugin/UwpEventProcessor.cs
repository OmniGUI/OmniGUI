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
    using Point = OmniGui.Point;

    public class UwpEventProcessor : IEventProcessor
    {
        private readonly FrameworkElement inputElement;

        public UwpEventProcessor(FrameworkElement inputElement)
        {
            this.inputElement = inputElement;
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
            Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => inputElement.InvalidateMeasure());
        }

        public IObservable<TextInputArgs> TextInput => new Subject<TextInputArgs>();
    }
}