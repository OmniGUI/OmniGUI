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
            Platform.Current.EventSource.SpecialKeys.Subscribe(ProcessSpecialKey);
        }

        private void ProcessSpecialKey(SpecialKeysArgs args)
        {
            if (args.Key == MyKey.RightArrow)
            {
                CursorPositionOrdinal++;
            }
            else if (args.Key == MyKey.LeftArrow)
            {
                CursorPositionOrdinal--;
            }
            else if (args.Key == MyKey.Backspace)
            {
                RemoveBefore();
                CursorPositionOrdinal--;
            }
            else if (args.Key == MyKey.Delete)
            {
                RemoveAfter();
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
            drawingContext.DrawText(FormattedText, VisualBounds.Point);
            DrawCursor(drawingContext);
        }

        private void DrawCursor(IDrawingContext drawingContext)
        {
            if (isCursorVisible)
            {
                var line = GetCursorSegment();
                drawingContext.DrawLine(line.P1, line.P2, new Pen(new Brush(Colors.Black), 1));
            }
        }

        private Segment GetCursorSegment()
        {
            var x = GetCursorX();
            var y = GetCursorY();

            var startPoint = new Point(x, 0);
            var endPoint = new Point(x, y);

            return new Segment(startPoint, endPoint);
        }

        private double GetCursorY()
        {
            return Platform.Current.TextEngine.GetHeight(FormattedText.FontFamily);
        }

        private double GetCursorX()
        {
            if (Text == String.Empty)
            {
                return 0;
            }

            var textBeforeCursor = Text.Substring(0, CursorPositionOrdinal);
            var formattedTextCopy = new FormattedText(FormattedText)
            {
                Text = textBeforeCursor
            };

            var x = formattedTextCopy.DesiredSize.Width;
            return x;
        }

        public string Text
        {
            get { return FormattedText.Text; }
            set
            {
                FormattedText.Text = value;
                Invalidate();
            }
        }

        private int CursorPositionOrdinal
        {
            get { return cursorPositionOrdinal; }
            set
            {
                if (value > Text.Length || value < 0)
                {
                    return;
                }
                cursorPositionOrdinal = value;
                Invalidate();
            }
        }

        private static void Invalidate()
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

        public void RemoveBefore()
        {
            if (CursorPositionOrdinal == 0)
            {
                return;
            }

            var leftPart = Text.Substring(0, CursorPositionOrdinal - 1);
            var rightPart = Text.Substring(CursorPositionOrdinal, Text.Length - CursorPositionOrdinal);
            Text = leftPart + rightPart;
        }

        public void RemoveAfter()
        {
            if (CursorPositionOrdinal == Text.Length)
            {
                return;
            }

            var leftPart = Text.Substring(0, CursorPositionOrdinal);
            var lenghtOfRightPart = Text.Length - CursorPositionOrdinal - 1;
            string rightPart;

            if (lenghtOfRightPart > 0)
            {
                rightPart = Text.Substring(CursorPositionOrdinal + 1, lenghtOfRightPart);
            }
            else
            {
                rightPart = string.Empty;
            }

            Text = leftPart + rightPart;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return FormattedText.DesiredSize;
        }

        private FormattedText FormattedText { get; } = new FormattedText()
        {
            FontSize = 14,
            Brush = new Brush(Colors.Black),
            Constraint = Size.Infinite,
            FontFamily = "Arial",
            FontWeight = FontWeights.Normal,
        };
    }
}