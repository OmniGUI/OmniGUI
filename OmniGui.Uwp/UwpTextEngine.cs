using System.Collections.Generic;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using OmniGui.Default;
using OmniGui.Geometry;

namespace OmniGui.Uwp
{
    public class UwpTextEngine : ITextEngine
    {
        private const float NearInfinity = 10000;
        private const string LineHeightProbe = "fg";
        public CanvasDrawingSession DrawingSession { get;  set; }
        private readonly IDictionary<string, double> lineHeights = new Dictionary<string, double>();

        public Size Measure(FormattedText formattedText)
        {
            var constraintWidth = (float)(double.IsInfinity(formattedText.Constraint.Width) ? NearInfinity : formattedText.Constraint.Width);
            var constraintHeight = (float)(double.IsInfinity(formattedText.Constraint.Height) ? NearInfinity : formattedText.Constraint.Height);

            var canvasTextFormat = new CanvasTextFormat
            {
                FontFamily = formattedText.FontName,
                FontSize = formattedText.FontSize,
                FontWeight = formattedText.FontWeight.ToWin2D(),
            };

            var t = new CanvasTextLayout(DrawingSession, formattedText.Text, canvasTextFormat, constraintWidth, constraintHeight);
            return new Size(t.DrawBounds.Width, t.DrawBounds.Height);
        }

        public double GetHeight(string fontName, float fontSize)
        {
            if (lineHeights.TryGetValue(fontName, out var height))
            {
                return height;
            }

            var canvasTextFormat = new CanvasTextFormat
            {
                FontFamily = fontName,
                FontSize = fontSize,
            };

            var t = new CanvasTextLayout(DrawingSession, LineHeightProbe, canvasTextFormat, NearInfinity, NearInfinity);
            height = t.DrawBounds.Height;
            lineHeights.Add(fontName, height);
            return height;
        }
    }
}