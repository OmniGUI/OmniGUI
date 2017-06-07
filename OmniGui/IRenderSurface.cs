using System;

namespace OmniGui
{
    public interface IRenderSurface
    {
        void ForceRender();
        void ShowVirtualKeyboard();
        void SetFocusedElement(Layout textBoxView);
        IObservable<Layout> FocusedElement { get; }
    }
}