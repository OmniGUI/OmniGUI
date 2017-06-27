using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using OmniGui.Xaml;
using OmniXaml.Services;
using Serilog;
using ControlTemplate = OmniGui.Xaml.Templates.ControlTemplate;
using Point = OmniGui.Geometry.Point;
using Rect = OmniGui.Geometry.Rect;
using Size = OmniGui.Geometry.Size;

namespace OmniGui.Uwp
{
    public class OmniGuiControl : Control
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(OmniGuiControl), new PropertyMetadata(default(Uri), OnSourceChanged));

        private static Exception setSourceException;
        public CanvasControl CanvasControl { get; private set; } = new CanvasControl();
        private ResourceStore resourceStore;
        private readonly UwpTextEngine uwpTextEngine;

        static OmniGuiControl()
        {
            OmniGuiPlatform.PropertyEngine = new OmniGuiPropertyEngine();
        }

        protected override void OnApplyTemplate()
        {
            CanvasControl = GetTemplateChild("CanvasControl") as CanvasControl;
            Observable.FromEventPattern<TypedEventHandler<CanvasControl, CanvasDrawEventArgs>, CanvasDrawEventArgs>(
                    ev => CanvasControl.Draw += ev, ev => CanvasControl.Draw -= ev)
                .Subscribe(pattern => OnDraw(pattern.EventArgs.DrawingSession));
        }

        public OmniGuiControl()
        {
            DefaultStyleKey = typeof(OmniGuiControl);
            Log.Information("Creado control");
         
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

            uwpTextEngine = new UwpTextEngine();
            var platform = new Platform(new UwpEventSource(this), new UwpRenderSurface(this), uwpTextEngine);
            var typeLocator = new TypeLocator(() => ResourceStore, platform);
            XamlLoader = new OmniGuiXamlLoader(Assemblies, typeLocator);
        }

        private IList<Assembly> Assemblies { get; } =
            new[]
            {
                Assembly.Load(new AssemblyName("OmniGui")),
                Assembly.Load(new AssemblyName("OmniGui.Uwp")),
                Assembly.Load(new AssemblyName("OmniGui.Xaml")),
                Assembly.Load(new AssemblyName("WindowsApp")),
                Assembly.Load(new AssemblyName("Common"))
            };

        public ICollection<ControlTemplate> ControlTemplates => ResourceStore.ControlTemplates;

        public ResourceStore ResourceStore => resourceStore ??
                                      (resourceStore =
                                          CreateContainer(new Uri("ms-appx:///ResourceStore.xaml",
                                              UriKind.RelativeOrAbsolute)));


        public Layout Layout { get; set; }

        public Uri Source
        {
            get => (Uri) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }


        public IXamlLoader XamlLoader { get; }

        private Exception LoadException
        {
            get => setSourceException;
            set => setSourceException = value;
        }

        private void OnDraw(CanvasDrawingSession drawingSession)
        {
            if (LoadException != null)
            {
                DrawException(drawingSession);
            }

            if (Layout == null)
            {
                return;
            }

            uwpTextEngine.DrawingSession = drawingSession;

            var width = ActualWidth;
            var height = ActualHeight;

            var availableSize = new Size(width, height);
            Layout.Measure(availableSize);
            Layout.Arrange(new Rect(Point.Zero, availableSize));
            
            Layout.Render(new UwpDrawingContext(drawingSession));
        }

        private void DrawException(CanvasDrawingSession drawingSession)
        {
            var text = $"XAML load error in {Source}: {LoadException}";
            drawingSession.DrawText(text, new Vector2(0,0), Windows.UI.Color.FromArgb(255, 255, 0, 0));
        }

        private void TrySetDataContext(object dc)
        {
            if (Layout != null)
            {
                Layout.DataContext = dc;
            }
        }

        private ResourceStore CreateContainer(Uri uri)
        {
            Log.Debug("Loading ResourceStore");
            var readFromContent = Task.Run(uri.ReadFromContent).Result;
            var load = XamlLoader.Load(readFromContent);
            Log.Debug("ResourceStore loaded");
            return (ResourceStore) load;
        }

        private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var target = (OmniGuiControl) dependencyObject;
            var uri = (Uri) args.NewValue;

            target.OnSourceChanged(uri);
        }

        private void OnSourceChanged(Uri uri)
        {
            try
            {
                LoadException = null;
                Log.Information("Cargando archivo XAML desde {Uri}", uri);
                var xaml = Task.Run(uri.ReadFromContent).Result;
                var flacidLayout = (Layout)XamlLoader.Load(xaml);
                Log.Information("Archivo cargado", uri);

                Log.Information("Inflando...");
                new TemplateInflator().Inflate(flacidLayout, ControlTemplates);
                Log.Information("Inflado: fin de carga");
                Layout = flacidLayout;
            }
            catch (Exception e)
            {
                LoadException = e;
            }
        }        
    }
}