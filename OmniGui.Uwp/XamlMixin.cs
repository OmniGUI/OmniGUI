using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace OmniGui.Uwp
{
    public static class XamlMixin
    {
        public static async Task<string> ReadFromContent(this Uri uri)
        {            
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var xaml = await FileIO.ReadTextAsync(file);
            return xaml;
        }
    }
}