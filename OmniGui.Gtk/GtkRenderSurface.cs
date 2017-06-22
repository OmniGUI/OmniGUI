using Gtk;

namespace OmniGui.Gtk
{
    internal class GtkRenderSurface : RenderSurface
    {
        private readonly Widget widget;

        public GtkRenderSurface(Widget widget)
        {
            this.widget = widget;
        }

        public override void ForceRender()
        {
            widget.QueueDraw();
        }

        public override void ShowVirtualKeyboard()
        {            
        }
    }
}