using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using OmniGui;

namespace UwpApp.Plugin
{
    using System;
    using System.Collections.Generic;
    using OmniGui.Geometry;

    public class Win2DTextEngine : ITextEngine
    {
        private const float NearInfinity = 10000;
        private const string LineHeightProbe = "fg";
        private CanvasDrawingSession drawingSession;
        private readonly IDictionary<string, double> lineHeights = new Dictionary<string, double>();

        public void SetDrawingSession(CanvasDrawingSession session)
        {
            this.drawingSession = session;
        }

        public Size Measure(FormattedText formattedText)
        {
            var constraintWidth = (float)(double.IsInfinity(formattedText.Constraint.Width) ? NearInfinity : formattedText.Constraint.Width);
            var constraintHeight = (float)(double.IsInfinity(formattedText.Constraint.Height) ? NearInfinity : formattedText.Constraint.Height);

            var canvasTextFormat = new CanvasTextFormat
            {
                FontFamily = formattedText.FontFamily,
                FontSize = formattedText.FontSize,
                FontWeight = formattedText.FontWeight.ToWin2D(),
            };

            var t = new CanvasTextLayout(drawingSession, formattedText.Text, canvasTextFormat, constraintWidth, constraintHeight);
            return new Size(t.DrawBounds.Width, t.DrawBounds.Height);
        }

        public double GetHeight(string fontFamily)
        {
            if (lineHeights.TryGetValue(fontFamily, out var height))
            {
                return height;
            }

            var canvasTextFormat = new CanvasTextFormat
            {
                FontFamily = fontFamily,
            };

            var t = new CanvasTextLayout(drawingSession, LineHeightProbe, canvasTextFormat, NearInfinity, NearInfinity);
            height = t.DrawBounds.Height;
            lineHeights.Add(fontFamily, height);
            return height;

        }
    }
}