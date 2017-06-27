using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OmniGui.Xaml;
using OmniXaml.Services;
using ControlTemplate = OmniGui.Xaml.Templates.ControlTemplate;
using Point = OmniGui.Geometry.Point;
using Rect = OmniGui.Geometry.Rect;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.Wpf
{
    public class OmniGuiControl : Control
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(OmniGuiControl), new PropertyMetadata(default(Uri), OnSourceChanged));

        private static Layout layout;
        private static Exception setSourceException;
        private ResourceStore resourceStore;

        static OmniGuiControl()
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
        }

        public OmniGuiControl()
        {
            Focusable = true;

            Observable
                .FromEventPattern<MouseButtonEventHandler, MouseEventArgs>(
                    ev => PreviewMouseDown += ev,
                    ev => PreviewMouseDown -= ev)
                .Subscribe(p => Focus());

            Observable
                .FromEventPattern<DependencyPropertyChangedEventHandler, DependencyPropertyChangedEventArgs>(
                    ev => DataContextChanged += ev,
                    ev => DataContextChanged -= ev)
                .Subscribe(TrySetDataContext);

            var deps = new Platform(new WpfEventSource(this), new WpfRenderSurface(this), new WpfTextEngine());
            var typeLocator = new TypeLocator(() => ResourceStore, deps);
            XamlLoader = new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(),
                typeLocator);
        }

        private void TrySetDataContext(EventPattern<DependencyPropertyChangedEventArgs> dc)
        {
            if (Layout != null)
            {
                Layout.DataContext = dc.EventArgs.NewValue;
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => ResourceStore.ControlTemplates;

        public ResourceStore ResourceStore => resourceStore ??
                                      (resourceStore =
                                          CreateContainer(new Uri("ResourceStore.xaml", UriKind.RelativeOrAbsolute)));


        public Layout Layout { get; set; }

        public Uri Source
        {
            get { return (Uri) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        public IXamlLoader XamlLoader { get; }

        private ResourceStore CreateContainer(Uri uri)
        {
            return (ResourceStore) XamlLoader.Load(uri.ReadFromContent());
        }

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var target = (OmniGuiControl)dependencyObject;
            var xaml = (Uri)args.NewValue;

            try
            {               
                var flacidLayout = (Layout) target.XamlLoader.Load(xaml.ReadFromContent());
                new TemplateInflator().Inflate(flacidLayout, target.ControlTemplates);
                target.Layout = flacidLayout;
            }
            catch (Exception e)
            {
                target.Exception = e;
            }
        }

        private Exception Exception
        {
            get { return setSourceException; }
            set
            {
                setSourceException = value;
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Exception != null)
            {
                RenderException(Exception, drawingContext);
            }

            if (Layout == null)
            {
                return;
            }

            var width = ActualWidth;
            var height = ActualHeight;

            var availableSize = new Size(width, height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Rect(Point.Zero, availableSize));
            Layout.Render(new WpfDrawingContext(drawingContext));
        }

        private void RenderException(Exception exception, DrawingContext drawingContext)
        {
            var textToFormat = $"XAML load error in {Source}: {exception}";
            var formattedText = new System.Windows.Media.FormattedText(textToFormat, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(SystemFonts.MenuFontFamily.Source), FontSize, Brushes.Red, new NumberSubstitution(), TextFormattingMode.Display, 96);
            formattedText.MaxTextWidth = ActualWidth;

            drawingContext.DrawText(formattedText, new System.Windows.Point( (ActualWidth - formattedText.Width) / 2, (ActualHeight - formattedText.Height) / 2));
        }
    }
}