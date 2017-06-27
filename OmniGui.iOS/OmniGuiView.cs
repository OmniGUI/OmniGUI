using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using CoreGraphics;
using ObjCRuntime;
using OmniGui.Xaml;
using OmniGui.Xaml.Templates;
using OmniXaml.Services;
using UIKit;

namespace OmniGui.iOS
{
    [Adopts("UIKeyInput")]
    public class OmniGuiView : UIView, IUIKeyInput
    {
        private object dataContext;
        private string source;
        private ResourceStore resourceStore;
        private Exception exception;
        private UIView exceptionView;
        private ISubject<TextInputArgs> textInputSubject = new Subject<TextInputArgs>();
        private ISubject<KeyArgs> keySubject = new Subject<KeyArgs>();

        public OmniGuiView()
        {
            XamlLoader = CreateXamlLoader();            
        }

        public override bool CanBecomeFirstResponder => true;

        public override void Draw(CGRect rect)
        {
            if (Exception != null)
            {
                return;
            }

            var bounds = rect.ToOmniGui();

            Layout.Measure(bounds.Size);
            Layout.Arrange(bounds);
            using (var ctx = UIGraphics.GetCurrentContext())
            {
                Layout.Render(new iOSDrawingContext(ctx));
            }
        }

        public ISubject<KeyArgs> KeyInput => keySubject;
        public ISubject<TextInputArgs> TextInput => textInputSubject;


        public Layout Layout { get; set; }

        private IXamlLoader CreateXamlLoader()
        {
            var androidEventSource = new iOSEventSource(this);
            var deps = new Platform(androidEventSource, new iOSRenderSurface(this), new iOSTextEngine());
            var typeLocator = new TypeLocator(() => ResourceStore, deps, () => XamlLoader.StringSourceValueConverter);
            return new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), typeLocator, () => new StyleWatcher(ResourceStore.Styles));
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
                var flacidLayout = (Layout) XamlLoader.Load(ReadMixin.ReadText(value));
                new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
                Layout = flacidLayout;
                Layout.DataContext = DataContext;
            }
            catch (Exception e)
            {
                Exception = e;
                this.SetNeedsDisplay();
            }
        }

        public Exception Exception
        {
            get { return exception; }
            set
            {
                exception = value;
                if (exception != null)
                {
                    exceptionView = new UITextView(Bounds) {Text = Exception.ToString()};
                    AddSubview(exceptionView);
                    SetNeedsDisplay();
                }
                else
                {
                    exceptionView.RemoveFromSuperview();
                }
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => ResourceStore.ControlTemplates;

        public ResourceStore ResourceStore => resourceStore ?? (resourceStore = CreateContainer("ResourceStore.xaml"));

        private ResourceStore CreateContainer(string containerAsset)
        {
            return (ResourceStore) XamlLoader.Load(ReadMixin.ReadText(containerAsset));
        }

        public void InsertText(string text)
        {
            textInputSubject.OnNext(new TextInputArgs {Text = text});
        }

        public void DeleteBackward()
        {
            keySubject.OnNext(new KeyArgs(MyKey.Delete));
        }

        public IXamlLoader XamlLoader { get; }

        public object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                if (Layout != null)
                {
                    Layout.DataContext = value;
                }
            }
        }

        public bool HasText => true;

        public UITextAutocapitalizationType AutocapitalizationType { get; set; } = UITextAutocapitalizationType.None;

        public UITextAutocorrectionType AutocorrectionType { get; set; } = UITextAutocorrectionType.Default;

        public UIKeyboardType KeyboardType { get; set; } = UIKeyboardType.Default;

        public UIKeyboardAppearance KeyboardAppearance { get; set; } = UIKeyboardAppearance.Default;

        public UIReturnKeyType ReturnKeyType { get; set; } = UIReturnKeyType.Default;

        public bool EnablesReturnKeyAutomatically { get; set; } = true;

        public bool SecureTextEntry { get; set; } = false;

        public UITextSpellCheckingType SpellCheckingType { get; set; } = UITextSpellCheckingType.Default;
    }
}