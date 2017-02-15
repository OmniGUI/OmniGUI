using System;
using System.Globalization;
using OmniXaml;
using OmniXaml.Attributes;

namespace OmniGui.Xaml
{
    using Geometry;
    using Layouts.Grid;

    public static class TypeConverters
    {
        [TypeConverterMember(typeof(Size))]
        public static Func<ConverterValueContext, object> SizeConverter = context => ToSize((string) context.Value);

        [TypeConverterMember(typeof(Color))]
        public static Func<ConverterValueContext, object> ColorConverter = context => ColorConvert((string)context.Value);

        [TypeConverterMember(typeof(Brush))]
        public static Func<ConverterValueContext, object> BrushConverter = context => new Brush(ColorConvert((string) context.Value));

        [TypeConverterMember(typeof(Thickness))]
        public static Func<ConverterValueContext, object> ThicknessConverter = context => Thickness.Parse((string)context.Value, CultureInfo.CurrentCulture);

        [TypeConverterMember(typeof(GridLength))]
        public static Func<ConverterValueContext, object> GridLengthConverter = context => GridLength.Parse((string)context.Value, CultureInfo.CurrentCulture);


        private static Color ColorConvert(string contextValue)
        {
            return Color.Parse(contextValue);            
        }      

        private static Size ToSize(string contextValue)
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