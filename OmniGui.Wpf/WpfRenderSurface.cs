using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OmniGui.Layouts;

namespace OmniGui.Wpf
{
    public class WpfRenderSurface : IRenderSurface
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();

        public UIElement UiElement { get; }

        public WpfRenderSurface(UIElement uiElement)
        {
            UiElement = uiElement;
        }

        public void ForceRender()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UiElement.InvalidateVisual();
            }, DispatcherPriority.Render);
        }

        public void ShowVirtualKeyboard()
        {            
        }

        public IObservable<Layout> FocusedElement => focusedElementSubject.DistinctUntilChanged();

        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }
    }
}