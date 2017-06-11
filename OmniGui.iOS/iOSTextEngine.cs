using System.Drawing;
using CoreText;
using Foundation;
using UIKit;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.iOS
{
    public class iOSTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            var width = formattedText.Constraint.Width;
            var options = NSStringDrawingOptions.UsesFontLeading |
                          NSStringDrawingOptions.UsesLineFragmentOrigin;

            var boundSize = new SizeF((float) width, float.MaxValue);
            var nsText = new NSString(formattedText.Text);
            var attributes = new UIStringAttributes
            {
                Font = UIFont.FromName(formattedText.FontName, formattedText.FontSize),
            };
            var sizeF = nsText.GetBoundingRect(boundSize, options, attributes, null).Size;

            return sizeF.ToOmniGui();
        }

        public double GetHeight(string fontName, float fontSize)
        {
            return new CTFont(fontName, fontSize).CapHeightMetric; 
        }
    }
}