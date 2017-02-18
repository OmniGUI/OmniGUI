namespace OmniGui.Tests
{
    using Layouts;
    using Xunit;

    public class TextBoxTests
    {
        [Fact]
        public void Refresh()
        {
            var testPlatform = new TestPlatform();
            Platform.Current = testPlatform;
            var textBox = new TextBox() {Text = "Hi"};
            textBox.Text = "New";
            var testPlatformEventSource = (TestEventSource)testPlatform.EventSource;
            Assert.Equal(2, testPlatformEventSource.InvalidateCount);
        }

        [Fact]
        public void SendKey()
        {
            var testPlatform = new TestPlatform();
            Platform.Current = testPlatform;
            var textBox = new TextBox { Text = "Hi" };
            var testPlatformEventSource = (TestEventSource)testPlatform.EventSource;
            testPlatform.SetFocusedElement(textBox);
            testPlatformEventSource.SendKey('a');
            Assert.Equal("Hia", textBox.Text);
        }

        [Fact]
        public void SendBackspace()
        {
            var testPlatform = new TestPlatform();
            Platform.Current = testPlatform;
            var textBox = new TextBox { Text = "Hi" };
            var testPlatformEventSource = (TestEventSource)testPlatform.EventSource;
            testPlatform.SetFocusedElement(textBox);
            testPlatformEventSource.SendKey(Chars.Backspace);
            Assert.Equal("H", textBox.Text);
            Assert.Equal(2, testPlatformEventSource.InvalidateCount);
        }
    }
}