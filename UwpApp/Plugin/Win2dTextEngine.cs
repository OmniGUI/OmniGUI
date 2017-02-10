using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using OmniGui;

namespace UwpApp.Plugin
{
    using System;
    using Windows.UI.Xaml.Media;

    public class Win2DTextEngine : ITextEngine
    {
        private CanvasDrawingSession session;

        public void SetDrawingSession(CanvasDrawingSession session)
        {
            this.session = session;
        }

        public Size Measure(FormattedText formattedText)
        {
            var constraintWidth = (float)(double.IsInfinity(formattedText.Constraint.Width) ? 1000 : formattedText.Constraint.Width);
            var constraintHeight = (float)(double.IsInfinity(formattedText.Constraint.Height) ? 1000 : formattedText.Constraint.Height);

            var t = new CanvasTextLayout(session, formattedText.Text, new CanvasTextFormat(), constraintWidth, constraintHeight);
            return new Size(t.DrawBounds.Width, t.DrawBounds.Height);
        }

        public double GetHeight(string fontFamily)
        {
            var canvasTextFormat = new CanvasTextLayout(session, "", new CanvasTextFormat(), 0, 0);

            var fontDpiSize = 16;
            var fontHeight = Math.Ceiling(fontDpiSize * 1.2);
            return fontHeight;

        }
    }
}