using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;
using OmniGui.Geometry;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml.Services;
using Serilog;
using Exception = System.Exception;
using Point = OmniGui.Geometry.Point;
using Rect = OmniGui.Geometry.Rect;

namespace OmniGui.Android
{
    public class OmniGuiView : View
    {
        private object dataContext;
        private ResourceStore resourceStore;
        private string source;

        public OmniGuiView(Context context, IList<Assembly> assemblies) : base(context)
        {
            FocusableInTouchMode = true;
            XamlLoader = CreateXamlLoader(this, (Activity) Context, assemblies);
            Focusable = true;
            RequestFocus();
        }

        public ISubject<ICharSequence> TextInput { get; } = new Subject<ICharSequence>();

        public string Source
        {
            get => source;
            set
            {
                source = value;
                SetSource(value);
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => ResourceStore.ControlTemplates;

        public ResourceStore ResourceStore => resourceStore ?? (resourceStore = CreateContainer("resourcestore.xaml"));

        private new Layout Layout { get; set; }

        public IXamlLoader XamlLoader { get; }

        public object DataContext
        {
            get => dataContext;
            set
            {
                dataContext = value;
                if (Layout != null)
                {
                    Layout.DataContext = dataContext;
                }
            }
        }

        private Exception LoadException { get; set; }

        private IXamlLoader CreateXamlLoader(OmniGuiView view, Activity activity, IList<Assembly> assemblies)
        {
            var androidEventSource = new AndroidEventSource(view);
            var deps = new Platform(androidEventSource, new AndroidRenderSurface(this, activity),
                new AndroidTextEngine());
            var typeLocator = new TypeLocator(() => ResourceStore, deps, () => XamlLoader.StringSourceValueConverter);

            return new OmniGuiXamlLoader(assemblies, typeLocator, () => new StyleWatcher(ResourceStore.Styles));
        }

        private void SetSource(string value)
        {
            try
            {
                LoadException = null;
                var flacidLayout = (Layout) XamlLoader.Load(ReadMixin.ReadTextFromAsset(value, Context.Assets));
                new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
                Layout = flacidLayout;
                Layout.DataContext = DataContext;
            }
            catch (Exception e)
            {
                LoadException = e;
                Log.Error("Could not load XAML from file", e);
            }
        }

        private ResourceStore CreateContainer(string containerAsset)
        {
            return (ResourceStore) XamlLoader.Load(ReadMixin.ReadTextFromAsset(containerAsset, Context.Assets));
        }

        public override void Draw(Canvas canvas)
        {
            if (LoadException != null)
            {
                DrawError(canvas);
                return;
            }

            var context = new AndroidDrawingContext(canvas);
            var availableSize = new Size(canvas.Width, canvas.Height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Rect(Point.Zero, availableSize));
            Layout.Render(context);
        }

        private void DrawError(Canvas canvas)
        {
            var exceptionText = $"Error while loading XAML: {LoadException}";
            var scale = ((Activity) Context).Resources.DisplayMetrics.Density;
            var paint = new TextPaint(PaintFlags.AntiAlias) {Color = global::Android.Graphics.Color.Red, TextSize = 16};
            var textWidth = canvas.Width - (int) (16 * scale);
            var textLayout = new StaticLayout(
                exceptionText, paint, textWidth, global::Android.Text.Layout.Alignment.AlignCenter, 1.0f, 0.0f, false);

            var textHeight = textLayout.Height;

            var x = (float) (Width - textWidth) / 2;
            var y = (float) (Height - textHeight) / 2;

            canvas.Save();
            canvas.Translate(x, y);
            textLayout.Draw(canvas);
            canvas.Restore();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {
                var imm = (InputMethodManager) Context.GetSystemService(Context.InputMethodService);
                imm.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }

            return true;
        }

        public override IInputConnection OnCreateInputConnection(EditorInfo outAttrs)
        {
            return new PlatformInputConnection(this, true, TextInput);
        }
    }
}