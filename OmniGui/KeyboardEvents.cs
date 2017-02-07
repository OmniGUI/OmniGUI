namespace OmniGui
{
    using System;

    public class KeyboardEvents
    {
        public KeyboardEvents(IEventProcessor eventDriver)
        {
            TextInput = eventDriver.TextInput;
            KeyInput = eventDriver.KeyInput;
        }

        public IObservable<TextInputArgs> TextInput { get; set; }
        public IObservable<KeyInputArgs> KeyInput { get; set; }
    }
}