using System.Windows;
using System.Windows.Threading;

namespace OmniGui.Wpf
{
    public class WpfRenderSurface : RenderSurface
    {
        public UIElement UiElement { get; }

        public WpfRenderSurface(UIElement uiElement)
        {
            UiElement = uiElement;
        }

        public override void ForceRender()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UiElement.InvalidateVisual();
            }, DispatcherPriority.Render);
        }

        public override void ShowVirtualKeyboard()
        {            
        }
    }
}