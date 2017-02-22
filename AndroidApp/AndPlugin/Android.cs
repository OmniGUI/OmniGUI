using Android.Graphics;

namespace AndroidApp.AndPlugin
{
    public static class Android
    {
        public static PointF ToAndroid(this OmniGui.Geometry.Point point)
        {
            return new PointF((int)point.X, (int)point.Y);
        }

        public static Color ToAndroid(this OmniGui.Color color)
        {
            return new Color(color.Red, color.Green, color.Blue, color.Alpha);
        }

        public static RectF ToAndroid(this OmniGui.Geometry.Rect rect)
        {
            return new RectF((float) rect.X,  (float) rect.Y, (float) (rect.X + rect.Width), (float) (rect.Y + rect.Height));
        }
    }
}