using System;

namespace OmniGui
{
    public static class Colors
    {
        public static Color Red { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue, 0, 0);
        public static Color Green { get; } = Color.FromArgb(byte.MaxValue, 0, byte.MaxValue, 0);
        public static Color Blue { get; } = Color.FromArgb(byte.MaxValue, 0, 0, byte.MaxValue);
        public static Color White { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static Color Gray { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue / 2, byte.MaxValue / 2, byte.MaxValue / 2);
    }
}