using Windows.UI.Xaml.Controls;

namespace UwpApp
{
    using Microsoft.Graphics.Canvas.UI.Xaml;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CanvasControl_OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {            
        }
    }
}
