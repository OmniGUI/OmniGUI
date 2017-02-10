namespace OmniGui
{
    public class TextInputArgs
    {
        public TextInputArgs(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}