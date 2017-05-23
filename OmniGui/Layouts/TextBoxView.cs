namespace OmniGui.Layouts
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using Geometry;
    using Zafiro.PropertySystem.Standard;

    public class TextBoxView : Layout
    {
        public static readonly ExtendedProperty TextProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Text",
            typeof(TextBoxView),
            typeof(string), new PropertyMetadata {DefaultValue = null});

        private IDisposable changedSubscription;
        private int cursorPositionOrdinal;
        private IDisposable cursorToggleChanger;

        private bool isCursorVisible;
        private bool isFocused;

        public TextBoxView()
        {
            var changedObservable = GetChangedObservable(TextProperty);

            Pointer.Down.Subscribe(point =>
            {
                Platform.Current.EventSource.ShowVirtualKeyboard();
                Platform.Current.SetFocusedElement(this);
            });

            Keyboard.KeyInput.Where(Filter).Subscribe(args => AddText(args.Text));
            Keyboard.SpecialKeys.Subscribe(ProcessSpecialKey);
            NotifyRenderAffectedBy(TextProperty);
            Platform.Current.FocusedElement.Select(layout => layout == this)
                .Subscribe(isFocused => IsFocused = isFocused);

            changedSubscription = changedObservable
                .Subscribe(o =>
                {
                    FormattedText.Text = (string) o;
                    EnforceCursorLimits();
                    Invalidate();
                });
        }

        private static Func<KeyInputArgs, bool> Filter
        {
            get { return args => args.Text.ToCharArray().First() != Chars.Backspace; }
        }

        private bool IsFocused
        {
            get => isFocused;
            set
            {
                if (isFocused != value)
                {
                    if (value)
                    {
                        CreateCaretBlink();
                    }
                    else
                    {
                        DisableCaretBlink();
                    }
                }

                isFocused = value;
            }
        }

        private bool IsCursorVisible
        {
            get => isCursorVisible;
            set
            {
                isCursorVisible = value;
                Invalidate();
            }
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private int CursorPositionOrdinal
        {
            get => cursorPositionOrdinal;
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

        public string FontFamily => FormattedText.FontFamily;

        private FormattedText FormattedText { get; } = new FormattedText
        {
            FontSize = 14,
            Brush = new Brush(Colors.Black),
            Constraint = Size.Infinite,
            FontFamily = "Arial",
            FontWeight = FontWeights.Normal
        };

        private void DisableCaretBlink()
        {
            cursorToggleChanger?.Dispose();
        }

        private void CreateCaretBlink()
        {
            cursorToggleChanger?.Dispose();
            IsCursorVisible = true;
            var timelyObs = Observable.Interval(TimeSpan.FromSeconds(0.4));
            cursorToggleChanger = timelyObs.Subscribe(_ => SwitchCursorVisibility());
        }

        private void EnforceCursorLimits()
        {
            if (Text?.Length < CursorPositionOrdinal)
            {
                CursorPositionOrdinal = Text.Length;
            }
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

        public override void Render(IDrawingContext drawingContext)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                drawingContext.DrawText(FormattedText, VisualBounds.Point);
            }

            DrawCursor(drawingContext);
        }

        private void DrawCursor(IDrawingContext drawingContext)
        {
            if (IsCursorVisible && IsFocused)
            {
                var line = GetCursorSegment();
                drawingContext.DrawLine(line.P1, line.P2, new Pen(new Brush(Colors.Black), 1));
            }
        }

        private Segment GetCursorSegment()
        {
            var x = GetCursorX();
            var y = GetCursorY();

            var startPoint = new Point(x + VisualBounds.X, VisualBounds.Y);
            var endPoint = new Point(x + VisualBounds.X, y + VisualBounds.Y);

            return new Segment(startPoint, endPoint);
        }

        private double GetCursorY()
        {
            return Platform.Current.TextEngine.GetHeight(FormattedText.FontFamily);
        }

        private double GetCursorX()
        {
            if (string.IsNullOrEmpty(Text))
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

        private static void Invalidate()
        {
            Platform.Current.EventSource.Invalidate();
        }

        public void AddText(string text)
        {
            if (Text == null)
            {
                Text = text;
            }
            else
            {
                var firstPart = Text.Substring(0, CursorPositionOrdinal);
                var secondPart = Text.Substring(CursorPositionOrdinal, Text.Length - CursorPositionOrdinal);

                Text = firstPart + text + secondPart;
            }

            CursorPositionOrdinal++;
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
            var textDesired = FormattedText.Text != null ? FormattedText.DesiredSize : Size.Empty;

            var height = Math.Max(textDesired.Height, Platform.Current.TextEngine.GetHeight(FontFamily));
            return new Size(textDesired.Width, height);
        }
    }
}