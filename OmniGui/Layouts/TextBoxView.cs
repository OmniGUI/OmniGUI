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
            typeof(string), new PropertyMetadata { DefaultValue = null });

        public static readonly ExtendedProperty FontSizeProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("FontSize",
            typeof(TextBoxView),
            typeof(string), new PropertyMetadata { DefaultValue = 16 });

        private IDisposable changedSubscription;
        private int cursorPositionOrdinal;
        private IDisposable cursorToggleChanger;

        private bool isCursorVisible;
        private bool isFocused;

        public TextBoxView(FrameworkDependencies deps) : base(deps)
        {
            var changedObservable = GetChangedObservable(TextProperty);

            FormattedText = new FormattedText(Deps.TextEngine)
            {
                FontSize = 16,
                Brush = new Brush(Colors.Black),
                Constraint = Size.Infinite,
                FontName = "Arial",
                FontWeight = FontWeights.Normal
            };

            Pointer.Down.Subscribe(point =>
            {
                Deps.RenderSurface.ShowVirtualKeyboard();
                Deps.RenderSurface.SetFocusedElement(this);
            });

            Keyboard.KeyInput.Where(Filter).Subscribe(args => AddText(args.Text));
            Keyboard.SpecialKeys.Subscribe(ProcessSpecialKey);
            NotifyRenderAffectedBy(TextProperty, FontSizeProperty);
            Deps.RenderSurface.FocusedElement.Select(layout => layout == this)
                .Subscribe(isFocused => IsFocused = isFocused);

            GetChangedObservable(FontSizeProperty).Subscribe(o =>
            {
                if (FormattedText != null && o != null)
                {
                    FormattedText.FontSize = (float)o;
                }
            });

            changedSubscription = changedObservable
                .Subscribe(o =>
                {
                    FormattedText.Text = (string)o;
                    EnforceCursorLimits();
                    ForceRender();
                });
        }

        private static Func<TextInputArgs, bool> Filter
        {
            get
            {
                return args =>
          {
              var isFiltered = args.Text.ToCharArray().First() != Chars.Backspace;
              return isFiltered;
          };
            }
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
                ForceRender();
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private int CursorPositionOrdinal
        {
            get => cursorPositionOrdinal;
            set
            {
                if (Text == null || value > Text.Length || value < 0)
                {
                    return;
                }
                cursorPositionOrdinal = value;
                ForceRender();
            }
        }

        public string FontFamily => FormattedText.FontName;

        private FormattedText FormattedText { get; }

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

        private void ProcessSpecialKey(KeyArgs args)
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

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
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
            return Deps.TextEngine.GetHeight(FormattedText.FontName, FormattedText.FontSize);
        }

        private double GetCursorX()
        {
            if (string.IsNullOrEmpty(Text))
            {
                return 0;
            }

            var textBeforeCursor = Text.Substring(0, CursorPositionOrdinal);
            var formattedTextCopy = new FormattedText(FormattedText, Deps.TextEngine)
            {
                Text = textBeforeCursor
            };
            
            var x = formattedTextCopy.DesiredSize.Width;
            return x;
        }

        private void ForceRender()
        {
            Deps.RenderSurface.ForceRender();
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

            var height = Math.Max(textDesired.Height, Deps.TextEngine.GetHeight(FontFamily, FormattedText.FontSize));
            return new Size(textDesired.Width, height);
        }
    }
}