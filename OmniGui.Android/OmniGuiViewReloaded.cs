using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.InputMethods;
using Java.Lang;
using OmniGui.Geometry;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml.Services;

namespace OmniGui.Android
{
    public class OmniGuiViewReloaded : View
    {
        private string source;
        private Container container;
        private object dataContext;
        public ISubject<ICharSequence> TextInput { get; } = new Subject<ICharSequence>();


        public OmniGuiViewReloaded(Context context) : base(context)
        {
            FocusableInTouchMode = true;
            XamlLoader = CreateXamlLoader(this, (Activity) Context);
        }

        private IXamlLoader CreateXamlLoader(OmniGuiViewReloaded omniGuiViewReloaded, Activity activity)
        {
            var androidEventSource = new AndroidEventSource(this, activity);
            var deps = new FrameworkDependencies(androidEventSource, new AndroidRenderSurface(this, activity), new AndroidTextEngine());
            var typeLocator = new TypeLocator(() => ControlTemplates, deps);
            return new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), () => ControlTemplates, typeLocator);
        }

        public string Source
        {
            get { return source; }
            set
            {
                source = value;
                SetSource(value);
            }
        }

        private void SetSource(string value)
        {
            try
            {
                var flacidLayout = (Layout)XamlLoader.Load(ReadMixin.ReadTextFromAsset(value, Context.Assets));
                new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
                Layout = flacidLayout;
                Layout.DataContext = DataContext;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Container Container => container ?? (container = CreateContainer("Container.xaml"));

        private Container CreateContainer(string containerAsset)
        {
            return (Container)XamlLoader.Load(ReadMixin.ReadTextFromAsset(containerAsset, Context.Assets));
        }

        public Layout Layout { get; set; }

        public IXamlLoader XamlLoader { get; set; }

        public object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                Layout.DataContext = value;
            }
        }

        public override void Draw(Canvas canvas)
        {
            var context = new AndroidDrawingContext(canvas);
            var availableSize = new Size(canvas.Width, canvas.Height);
            Layout.Measure(availableSize);
            Layout.Arrange(new OmniGui.Geometry.Rect(OmniGui.Geometry.Point.Zero, availableSize));
            Layout.Render(context);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Up)
            {
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
            }

            return true;
        }

        public override IInputConnection OnCreateInputConnection(EditorInfo outAttrs)
        {
            return new PlatformInputConnection(this, true, TextInput);
        }

        private class PlatformInputConnection : BaseInputConnection
        {
            private readonly IObserver<ICharSequence> textObserver;

            public PlatformInputConnection(View targetView, bool fullEditor, IObserver<ICharSequence> textObserver) : base(targetView, fullEditor)
            {
                this.textObserver = textObserver;
            }

            public override bool CommitText(ICharSequence text, int newCursorPosition)
            {
                textObserver.OnNext(text);
                return true;
            }
        }
    }
}