using System.IO;

namespace OmniGui.iOS
{
    public static class ReadMixin
    {
        public static string ReadText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}