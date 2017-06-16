using System;
using Windows.UI.Xaml.Controls;

namespace OmniGui.Uwp
{
    public class UwpRenderSurface : DefaultRenderSurface
    {
        private readonly Control control;

        public UwpRenderSurface(Control control)
        {
            this.control = control;
        }
    }
}