namespace OmniGui.Tests
{
    using System;
    using Layouts;
    using Xunit;

    public class TextBoxTests
    {
        [Fact]
        public void Refresh()
        {
            Platform.Current = new TestPlatform();
            var textBox = new TextBox() {Text = "Hi"};            
        }
    }

    public class TestPlatform : IPlatform
    {
        public ITextEngine TextEngine { get; set; }
        public IEventSource EventSource { get; set; }
        public IObservable<Layout> FocusedElement { get; }
        public void SetFocusedElement(Layout layout)
        {
            throw new NotImplementedException();
        }
    }
}