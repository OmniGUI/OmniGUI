using System;
using Windows.UI.Xaml.Controls;
using OmniGui.Default;
using OmniGui.Geometry;

namespace OmniGui.Uwp
{
    public class UwpEventSource : DefaultEventSource
    {
        private readonly Control control;

        public UwpEventSource(Control control)
        {
            this.control = control;
        }    
    }
}