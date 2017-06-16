using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace OmniGui
{
    public abstract class RenderSurface : IRenderSurface
    {
        private readonly ISubject<Layout> focusedElementSubject = new Subject<Layout>();
        public IObservable<Layout> FocusedElement => focusedElementSubject.DistinctUntilChanged();

        public abstract void ForceRender();
        public abstract void ShowVirtualKeyboard();

        public void SetFocusedElement(Layout layout)
        {
            focusedElementSubject.OnNext(layout);
        }
    }
}