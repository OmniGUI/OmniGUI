namespace OmniGui.Layouts
{
    using System;
    using System.Reactive.Linq;
    using Geometry;

    public class TextBoxView : Layout
    {
        private IDisposable cursorToggleChanger;
        private bool isCursorVisible;
        private int cursorPositionOrdinal;

        public TextBoxView()
        {
            cursorToggleChanger = Observable.Interval(TimeSpan.FromSeconds(0.4)).Subscribe(_ => SwitchCursorVisibility());
            Text = "Hola tío";
            Platform.Current.EventSource.KeyInput.Subscribe(args => AddText(args.Text));
            Platform.Current.EventSource.SpecialKeys.Subscribe(args =>
            {
                if (args.Key == MyKey.RightArrow)
                {
                    MoveCursorToRight();
                }
                else if (args.Key == MyKey.LeftArrow)
                {
                    MoveCursorToLeft();
                }
                else if (args.Key == MyKey.Backspace)
                {
                    this.Remove();
                    CursorPositionOrdinal--;
                }
            });
        }

        private void MoveCursorToRight()
        {
            if (CursorPositionOrdinal < Text.Length)
            {
                CursorPositionOrdinal++;
            }
        }

        private void MoveCursorToLeft()
        {
            if (CursorPositionOrdinal > 0)
            {
                CursorPositionOrdinal--;
            }
        }

        private void SwitchCursorVisibility()
        {
            IsCursorVisible = !IsCursorVisible;
        }

        private bool IsCursorVisible
        {
            get { return isCursorVisible; }
            set
            {
                isCursorVisible = value;
                Invalidate();
            }
        }

        public override void Render(IDrawingContext drawingContext)
        {
            var formattedText = new FormattedText()
            {
                Text = Text,
                Brush = new Brush(Colors.Black),
                FontName = "Arial",
                FontSize = 20,
                Constraint = new Size(1000, 1000),
                FontFamily = "Arial",
            };

            drawingContext.DrawText(formattedText, VisualBounds.Point);
            DrawCursor(drawingContext);
        }

        private void DrawCursor(IDrawingContext drawingContext)
        {
            if (isCursorVisible)
            {
                var cursorHeight = Platform.Current.TextEngine.GetHeight("Arial");
                var textBeforeCursor = Text.Substring(0, CursorPositionOrdinal);
                var x = new FormattedText
                {
                    FontSize = 20,
                    FontFamily = "Arial",
                    Text = textBeforeCursor,
                    Constraint = Size.Infinite,                    
                }.Measure().Width;

                drawingContext.DrawLine(new Point(x, 0), new Point(x, cursorHeight), new Pen(new Brush(Colors.Black), 1));
            }
        }

        public string Text { get; set; }

        private int CursorPositionOrdinal
        {
            get { return cursorPositionOrdinal; }
            set
            {
                cursorPositionOrdinal = value;
                Invalidate();
            }
        }

        private void Invalidate()
        {
            Platform.Current.EventSource.Invalidate();
        }

        public void AddText(string text)
        {
            var firstPart = Text.Substring(0, CursorPositionOrdinal);
            var secondPart = Text.Substring(CursorPositionOrdinal, Text.Length - CursorPositionOrdinal);

            Text = firstPart + text + secondPart;
            CursorPositionOrdinal++;
            Invalidate();
        }

        public void Remove()
        {
            if (CursorPositionOrdinal == 0)
            {
                return;
            }

            var leftPart = Text.Substring(0, CursorPositionOrdinal - 1);
            var rightPart = Text.Substring(CursorPositionOrdinal, Text.Length - CursorPositionOrdinal);
            Text = leftPart + rightPart;
        }
    }
}