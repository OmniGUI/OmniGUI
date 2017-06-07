using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OmniGui.Xaml;
using OmniXaml.Services;
using WpfApp;
using Container = OmniGui.Xaml.Container;
using ControlTemplate = OmniGui.Xaml.Templates.ControlTemplate;

namespace OmniGui.Wpf
{
    public class OmniGuiControl : Control
    {
        static OmniGuiControl()
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
        }

        public OmniGuiControl()
        {
            OmniGui.Platform.Current = new WpfPlatform(this);
            Platform = OmniGui.Platform.Current;
            XamlLoader = new OmniGuiXamlLoader(Assemblies.AssembliesInAppFolder.ToArray(), () => ControlTemplates, new TypeLocator(() => ControlTemplates));
        }

        public ICollection<ControlTemplate> ControlTemplates => Container.ControlTemplates;

        public Container Container
        {
            get
            {
                return container ?? (container = CreateContainer(new Uri("Container.xaml", UriKind.RelativeOrAbsolute)));
            }
        }

        private Container CreateContainer(Uri uri)
        {
            return (Container) XamlLoader.Load(uri.ReadFromContent());
        }

        public IPlatform Platform { get; }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(OmniGuiControl), new PropertyMetadata(default(Uri), OnSourceChanged));

        private static Layout layout;
        private Container container;

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var target = (OmniGuiControl) dependencyObject;
            var xaml = (Uri)args.NewValue;
            var flacidLayout = (Layout) target.XamlLoader.Load(xaml.ReadFromContent());
            new TemplateInflator().Inflate(flacidLayout, target.ControlTemplates);
            target.Layout = flacidLayout;
        }

        public Layout Layout { get; set; }

        public Uri Source
        {
            get { return (Uri) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        public IXamlLoader XamlLoader { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Layout == null)
            {
                return;
            }

            var width = this.ActualWidth;
            var height = this.ActualHeight;

            var availableSize = new Geometry.Size(width, height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Geometry.Rect(Geometry.Point.Zero, availableSize));
            Layout.Render(new WpfDrawingContext(drawingContext));
        }
    }
}