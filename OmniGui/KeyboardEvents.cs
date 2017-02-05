namespace OmniGui
{
    using System;

    public class KeyboardEvents
    {
        public KeyboardEvents(IEventProcessor eventDriver)
        {
            TextInput = eventDriver.TextInput;
        }

        public IObservable<TextInputArgs> TextInput { get; set; }
    }
}