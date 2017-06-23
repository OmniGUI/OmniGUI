using System;
using UIKit;

namespace OmniGui.iOS
{
    internal class iOSRenderSurface : RenderSurface
    {
        private readonly UIView view;

        public iOSRenderSurface(UIView view)
        {
            this.view = view;
        }

        public override void ForceRender()
        {
            view.SetNeedsDisplay();
        }

        public override void ShowVirtualKeyboard()
        {
            view.BecomeFirstResponder();
        }
    }
}