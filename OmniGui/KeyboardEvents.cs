namespace OmniGui
{
    using System;
    using System.Reactive.Linq;

    public class KeyboardEvents
    {
        public KeyboardEvents(Layout layout, IEventSource eventDriver, IObservable<Layout> focusedElement)
        {
            FocusedElement = focusedElement;
            var keys = eventDriver.KeyInput;
            KeyInput = keys.WithLatestFrom(focusedElement, (key, lay) => new {key, lay})
                    .Where(arg => arg.lay == layout).Select(arg => arg.key);
            SpecialKeys = eventDriver.SpecialKeys.WithLatestFrom(focusedElement, (key, lay) => new { key, lay })
                .Where(arg => arg.lay == layout).Select(arg => arg.key);
        }

        public IObservable<SpecialKeysArgs> SpecialKeys { get; }

        public IObservable<Layout> FocusedElement { get; }

        public IObservable<KeyInputArgs> KeyInput { get; }
    }
}