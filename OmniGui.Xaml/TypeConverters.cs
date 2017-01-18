using System;
using OmniXaml;
using OmniXaml.Attributes;

namespace OmniGui.Xaml
{
    public static class TypeConverters
    {
        [TypeConverterMember(typeof(Size))]
        public static Func<ConverterValueContext, object> SizeConverter = context => MyConvert((string) context.Value);

        [TypeConverterMember(typeof(Color))]
        public static Func<ConverterValueContext, object> ColorConverter = context => ColorConvert((string)context.Value);

        private static Color ColorConvert(string contextValue)
        {
            return Color.Parse(contextValue);
        }

        private static object MyConvert(string contextValue)
        {
            var values = contextValue.Split(',');
            var w = values[0];
            var h = values[1];

            double width;
            double height;
            
            if (w == "-")
            {
                width = double.NaN;
            }
            else
            {
                width = double.Parse(w);
            }

            if (h == "-")
            {
                height = double.NaN;
            }
            else
            {
                height = double.Parse(h);
            }

            return new Size(width, height);
        }
    }
}