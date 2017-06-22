using System.Drawing;
using Gdk;
using Gtk;
using Graphics = Gtk.DotNet.Graphics;

namespace OmniGui.Gtk
{
    public class OmniGuiWidget : DrawingArea
    {
        protected override bool OnExposeEvent(EventExpose evnt)
        {
            using (var g = Graphics.FromDrawable(evnt.Window))
            {
                var location = this.Allocation.Location;
                var size = this.Allocation.Size;
                g.FillRectangle(Brushes.Red, new RectangleF(new PointF(location.X, location.Y), new SizeF(size.Width, size.Height)));
            }

            return base.OnExposeEvent(evnt);            
        }
    }
}