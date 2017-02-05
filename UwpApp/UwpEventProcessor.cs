namespace UwpApp
{
    using System;
    using System.Reactive.Linq;
    using Windows.UI.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using OmniGui;
    using Point = OmniGui.Point;

    public class UwpEventProcessor : IEventProcessor
    {
        public UwpEventProcessor(FrameworkElement inputElement)
        {
            var fromEventPattern = Observable.FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                ev => inputElement.Tapped += ev,
                ev => inputElement.Tapped -= ev);
            Pointer = fromEventPattern.Select(pattern =>
            {
                var position = pattern.EventArgs.GetPosition(inputElement);
                return new Point(position.X, position.Y);
            });

        }
        public IObservable<Point> Pointer { get; }
        public void Invalidate()
        {            
        }

        public IObservable<TextInputArgs> TextInput { get; set; }
    }
}