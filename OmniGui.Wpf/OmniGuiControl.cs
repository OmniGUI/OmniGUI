using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OmniGui.Xaml;
using OmniXaml.Services;
using WpfApp;
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
        private Container container;

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
                .Subscribe(dc => TrySetDataContext(dc));

            OmniGui.Platform.Current = new WpfPlatform(this);
            Platform = OmniGui.Platform.Current;
            var deps = new FrameworkDependencies(new WpfEventSource(this), new WpfRenderSurface(this));
            var typeLocator = new TypeLocator(() => ControlTemplates, deps);
            XamlLoader = new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), () => ControlTemplates,
                typeLocator);
        }

        private void TrySetDataContext(EventPattern<DependencyPropertyChangedEventArgs> dc)
        {
            if (Layout != null)
            {
                Layout.DataContext = dc.EventArgs.NewValue;
            }
        }

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Container Container => container ??
                                      (container =
                                          CreateContainer(new Uri("Container.xaml", UriKind.RelativeOrAbsolute)));

        public IPlatform Platform { get; }

        public Layout Layout { get; set; }

        public Uri Source
        {
            get { return (Uri) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        public IXamlLoader XamlLoader { get; set; }

        private Container CreateContainer(Uri uri)
        {
            return (Container) XamlLoader.Load(uri.ReadFromContent());
        }

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                var target = (OmniGuiControl) dependencyObject;
                var xaml = (Uri) args.NewValue;

                var flacidLayout = (Layout) target.XamlLoader.Load(xaml.ReadFromContent());
                new TemplateInflator().Inflate(flacidLayout, target.ControlTemplates);
                target.Layout = flacidLayout;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
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
    }
}