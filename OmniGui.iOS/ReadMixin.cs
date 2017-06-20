using System.IO;

namespace OmniGui.iOS
{
    public static class ReadMixin
    {
        public static string ReadText(this string path)
        {
            return File.ReadAllText(path);
        }

        public static Stream OpenStream(this string path)
        {
            return File.Open(path, FileMode.Open);
        }
    }
}