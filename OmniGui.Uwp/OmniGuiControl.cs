using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using OmniGui.Xaml;
using OmniXaml.Services;
using ControlTemplate = OmniGui.Xaml.Templates.ControlTemplate;

namespace OmniGui.Uwp
{
    public class OmniGuiControl : Control
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(OmniGuiControl), new PropertyMetadata(default(Uri), OnSourceChanged));

        private static Layout layout;
        private static Exception setSourceException;
        private Container container;

        static OmniGuiControl()
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
        }

        public OmniGuiControl()
        {
            Observable
                .FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                    ev => Tapped += ev,
                    ev => Tapped -= ev)
                .Subscribe(p => Focus(FocusState.Programmatic));

            Observable
                .FromEventPattern<TypedEventHandler<FrameworkElement, DataContextChangedEventArgs>,
                    DataContextChangedEventArgs>(
                    ev => DataContextChanged += ev,
                    ev => DataContextChanged -= ev)
                .Subscribe(dc => TrySetDataContext(dc.EventArgs.NewValue));

            var deps = new FrameworkDependencies(new UwpEventSource(this), new UwpRenderSurface(this),
                new UwpTextEngine());
            var typeLocator = new TypeLocator(() => ControlTemplates, deps);
            XamlLoader = new OmniGuiXamlLoader(Assemblies, () => ControlTemplates,
                typeLocator);
        }

        public IList<Assembly> Assemblies { get; } =
            new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("WindowsApp")),
                Assembly.Load(new AssemblyName("Common"))
            };

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Container Container => container ??
                                      (container =
                                          CreateContainer(new Uri("ms-appx:///Container.xaml",
                                              UriKind.RelativeOrAbsolute)));


        public Layout Layout { get; set; }

        public Uri Source
        {
            get => (Uri) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }


        public IXamlLoader XamlLoader { get; }

        private Exception Exception
        {
            get => setSourceException;
            set => setSourceException = value;
        }

        private void TrySetDataContext(object dc)
        {
            if (Layout != null)
            {
                Layout.DataContext = dc;
            }
        }

        private Container CreateContainer(Uri uri)
        {
            return (Container) XamlLoader.Load(uri.ReadFromContent().Result);
        }

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var target = (OmniGuiControl) dependencyObject;
            var xaml = (Uri) args.NewValue;

            try
            {
                var flacidLayout = (Layout) target.XamlLoader.Load(xaml.ReadFromContent().Result);
                new TemplateInflator().Inflate(flacidLayout, target.ControlTemplates);
                target.Layout = flacidLayout;
            }
            catch (Exception e)
            {
                target.Exception = e;
            }
        }
        //    Layout.Render(new WpfDrawingContext(drawingContext));
        //    Layout.Arrange(new Rect(Point.Zero, availableSize));
        //    Layout.Measure(availableSize);

        //    var availableSize = new Size(width, height);
        //    var height = ActualHeight;

        //    var width = ActualWidth;
        //    }
        //        return;
        //    {

        //    if (Layout == null)
        //    }
        //        RenderException(Exception, drawingContext);
        //    {
        //    if (Exception != null)
        //{

        //protected override void OnRender(DrawingContext drawingContext)
        //}

        //private void RenderException(Exception exception, DrawingContext drawingContext)
        //{
        //    var textToFormat = $"XAML load error in {Source}: {exception}";
        //    var formattedText = new System.Windows.Media.FormattedText(textToFormat, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(SystemFonts.MenuFontFamily.Source), FontSize, Brushes.Red, new NumberSubstitution(), TextFormattingMode.Display, 96);
        //    formattedText.MaxTextWidth = ActualWidth;

        //    drawingContext.DrawText(formattedText, new System.Windows.Point((ActualWidth - formattedText.Width) / 2, (ActualHeight - formattedText.Height) / 2));
        //}
    }
}