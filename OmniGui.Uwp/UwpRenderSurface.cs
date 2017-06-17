using Microsoft.Toolkit.Uwp;
using Windows.UI.ViewManagement;

namespace OmniGui.Uwp
{
    public class UwpRenderSurface : RenderSurface
    {
        private readonly OmniGuiControl control;

        public UwpRenderSurface(OmniGuiControl control)
        {
            this.control = control;
        }

        public override void ForceRender()
        {
            DispatcherHelper.ExecuteOnUIThreadAsync(() => control.CanvasControl.Invalidate());            
        }

        public override void ShowVirtualKeyboard()
        {
            var pane = InputPane.GetForCurrentView();
            pane.TryShow();
        }
    }
}