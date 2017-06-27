using System;
using System.Text;
using System.Windows;

namespace OmniGui.Wpf
{
    public static class XamlMixin
    {
        public static string ReadFromContent(this Uri uriContent)
        {
            if (uriContent == null)
            {
                throw new ArgumentNullException(nameof(uriContent));
            }

            var contentStream = Application.GetContentStream(uriContent);
            if (contentStream == null)
            {
                throw new ArgumentNullException(nameof(contentStream));
            }

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