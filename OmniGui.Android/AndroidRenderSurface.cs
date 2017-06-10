using Android.App;
using Android.Content;
using Android.Views.InputMethods;

namespace OmniGui.Android
{
    internal class AndroidRenderSurface : RenderSurfaceBase
    {
        private readonly OmniGuiViewReloaded view;
        private readonly Activity activity;

        public AndroidRenderSurface(OmniGuiViewReloaded view, Activity activity)
        {
            this.view = view;
            this.activity = activity;
        }

        public override void ForceRender()
        {
            activity.RunOnUiThread(() => view.Invalidate());
        }

        public override void ShowVirtualKeyboard()
        {
            var imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.ShowSoftInput(view, ShowFlags.Forced);
        }       
    }
}