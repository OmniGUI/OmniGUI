namespace UwpApp
{
    using Microsoft.Graphics.Canvas;
    using Microsoft.Graphics.Canvas.Text;
    using OmniGui;

    public class Win2DTextEngine : ITextEngine
    {
        private CanvasDrawingSession session;

        public void SetDrawingSession(CanvasDrawingSession session)
        {
            this.session = session;
        }

        public Size Measure(FormattedText formattedText)
        {
            var constraintWidth = (float) (double.IsInfinity(formattedText.Constraint.Width) ? 1000 : formattedText.Constraint.Width);
            var constraintHeight = (float)(double.IsInfinity(formattedText.Constraint.Height) ? 1000 : formattedText.Constraint.Height);

            var t = new CanvasTextLayout(session, formattedText.Text, new CanvasTextFormat(), constraintWidth, constraintHeight);
            return new Size(t.DrawBounds.Width, t.DrawBounds.Height);
        }
    }
}