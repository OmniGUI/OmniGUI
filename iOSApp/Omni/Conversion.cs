using System;
using CoreGraphics;
using OmniGui;
using OmniGui.Geometry;
using UIKit;

namespace iOSApp.Omni
{
    public static class Conversion
    {
        public static Rect ToOmniGui(this CGRect rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static CGRect ToiOS(this Rect rect)
        {
            return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static CGSize ToiOS(this Size size)
        {
            return new CGSize(size.Width, size.Height);
        }

        public static Size ToOmniGui(this CGSize size)
        {
            return new Size(size.Width, size.Height);
        }

        public static CGColor ToiOS(this Color color)
        {
            var c = UIColor.FromRGBA(color.Red, color.Green, color.Blue, color.Alpha);
            return c.CGColor;
        }

        public static CGPoint ToiOS(this Point point)
        {
            return new CGPoint(point.X, point.Y);
        }
    }
}