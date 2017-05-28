using System;
using System.Globalization;
using OmniGui.Geometry;
using OmniGui.Layouts.Grid;
using OmniXaml;
using OmniXaml.Attributes;

namespace OmniGui.Xaml
{
    public static class TypeConverters
    {
        [TypeConverterMember(typeof(Size))]
        public static Func<string, ConvertContext, (bool, object)> SizeConverter = (str, v) => ToSize(str);

        [TypeConverterMember(typeof(Color))]
        public static Func<string, ConvertContext, (bool, object)> ColorConverter = (str, v) => (true, ColorConvert(str));

        [TypeConverterMember(typeof(Brush))]
        public static Func<string, ConvertContext, (bool, object)> BrushConverter = (str, v) => (true, new Brush(ColorConvert(str)));

        [TypeConverterMember(typeof(Thickness))]
        public static Func<string, ConvertContext, (bool, object)> ThicknessConverter = (str, v) => (true, Thickness.Parse(str, CultureInfo.CurrentCulture));

        [TypeConverterMember(typeof(GridLength))]
        public static Func<string, ConvertContext, (bool, object)> GridLengthConverter = (str, v) => (true, GridLength.Parse(str, CultureInfo.CurrentCulture));

        private static  Color ColorConvert(string contextValue)
        {
            return Color.Parse(contextValue);            
        }      

        private static (bool, Size) ToSize(string contextValue)
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

            return (true, new Size(width, height));
        }
    }
}