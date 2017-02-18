namespace OmniGui
{
    using System;
    using System.Reactive.Linq;

    public class KeyboardEvents
    {
        public KeyboardEvents(Layout layout, IEventSource eventDriver, IObservable<Layout> focusedElement)
        {
            FocusedElement = focusedElement;
            TextInput = eventDriver.TextInput;
            var keys = eventDriver.KeyInput;
            KeyInput = keys.WithLatestFrom(focusedElement, (key, lay) => new {key, lay})
                    .Where(arg => arg.lay == layout).Select(arg => arg.key);
        }

        public IObservable<Layout> FocusedElement { get; }

        public IObservable<TextInputArgs> TextInput { get; set; }
        public IObservable<KeyInputArgs> KeyInput { get; set; }
    }
}