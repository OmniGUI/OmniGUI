using System;
using System.Text;
using System.Windows;

namespace OmniGui.Wpf
{
    public static class XamlMixin
    {
        public static string ReadFromContent(this Uri uriContent)
        {
            var contentStream = Application.GetContentStream(uriContent);
            using (var stream = contentStream.Stream)
            {
                var l = stream.Length;
                var bytes = new byte[l];
                stream.Read(bytes, 0, (int)l);
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}