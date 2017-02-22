using System;
using Windows.UI.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using OmniGui;

namespace UwpApp.Plugin
{
    using System.Numerics;
    using Windows.Graphics.DirectX;
    using Windows.System;
    using OmniGui.Geometry;
    using FontWeights = OmniGui.FontWeights;
    using Vector = OmniGui.Geometry.Vector;

    public static class ConvertExtensions
    {
        public static Windows.Foundation.Rect ToWin2D(this Rect rect)
        {
            return new Windows.Foundation.Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Windows.Foundation.Point ToWin2D(this Point rect)
        {
            return new Windows.Foundation.Point(rect.X, rect.Y);
        }

        public static Vector2 ToWin2D(this Vector vector)
        {
            return new Vector2((float) vector.X, (float) vector.Y);
        }

        public static Windows.Foundation.Point ToWin2D(this Size rect)
        {
            return new Windows.Foundation.Point(rect.Width, rect.Height);
        }

        public static Windows.UI.Color ToWin2D(this Color color)
        {
            return Windows.UI.Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static CanvasSolidColorBrush ToWin2D(this Brush brush, ICanvasResourceCreator resourceCreator)
        {
            return new CanvasSolidColorBrush(resourceCreator, brush.Color.ToWin2D());
        }

        public static CanvasBitmap ToWin2D(this Bitmap bmp, ICanvasResourceCreator resourceCreator)
        {
            return CanvasBitmap.CreateFromBytes(resourceCreator, bmp.Bytes, bmp.Width, bmp.Height, DirectXPixelFormat.B8G8R8A8UIntNormalized);
        }

        public static Windows.UI.Text.FontWeight ToWin2D(this FontWeights fontWeight)
        {
            switch (fontWeight)
            {
                case FontWeights.Normal:
                    return Windows.UI.Text.FontWeights.Normal;
                case FontWeights.Bold:
                    return Windows.UI.Text.FontWeights.Bold;
                case FontWeights.ExtraBold:
                    return Windows.UI.Text.FontWeights.ExtraBold;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fontWeight), fontWeight, null);
            }
        }

        public static MyKey ToOmniGui(this VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.None:
                    break;
                case VirtualKey.LeftButton:
                    break;
                case VirtualKey.RightButton:
                    break;
                case VirtualKey.Cancel:
                    break;
                case VirtualKey.MiddleButton:
                    break;
                case VirtualKey.XButton1:
                    break;
                case VirtualKey.XButton2:
                    break;
                case VirtualKey.Back:
                    return MyKey.Backspace;
                case VirtualKey.Tab:
                    break;
                case VirtualKey.Clear:
                    break;
                case VirtualKey.Enter:
                    return MyKey.Enter;
                case VirtualKey.Shift:
                    break;
                case VirtualKey.Control:
                    break;
                case VirtualKey.Menu:
                    break;
                case VirtualKey.Pause:
                    break;
                case VirtualKey.CapitalLock:
                    break;
                case VirtualKey.Kana:
                    break;
                case VirtualKey.Junja:
                    break;
                case VirtualKey.Final:
                    break;
                case VirtualKey.Hanja:
                    break;
                case VirtualKey.Escape:
                    break;
                case VirtualKey.Convert:
                    break;
                case VirtualKey.NonConvert:
                    break;
                case VirtualKey.Accept:
                    break;
                case VirtualKey.ModeChange:
                    break;
                case VirtualKey.Space:
                    break;
                case VirtualKey.PageUp:
                    break;
                case VirtualKey.PageDown:
                    break;
                case VirtualKey.End:
                    break;
                case VirtualKey.Home:
                    break;
                case VirtualKey.Left:
                    return MyKey.LeftArrow;                    
                case VirtualKey.Up:
                    return MyKey.UpArrow;
                case VirtualKey.Right:
                    return MyKey.RightArrow;
                case VirtualKey.Down:
                    return MyKey.DownArrow;
                case VirtualKey.Select:
                    break;
                case VirtualKey.Print:
                    break;
                case VirtualKey.Execute:
                    break;
                case VirtualKey.Snapshot:
                    break;
                case VirtualKey.Insert:
                    break;
                case VirtualKey.Delete:
                    return MyKey.Delete;
                case VirtualKey.Help:
                    break;
                case VirtualKey.Number0:
                    break;
                case VirtualKey.Number1:
                    break;
                case VirtualKey.Number2:
                    break;
                case VirtualKey.Number3:
                    break;
                case VirtualKey.Number4:
                    break;
                case VirtualKey.Number5:
                    break;
                case VirtualKey.Number6:
                    break;
                case VirtualKey.Number7:
                    break;
                case VirtualKey.Number8:
                    break;
                case VirtualKey.Number9:
                    break;
                case VirtualKey.A:
                    break;
                case VirtualKey.B:
                    break;
                case VirtualKey.C:
                    break;
                case VirtualKey.D:
                    break;
                case VirtualKey.E:
                    break;
                case VirtualKey.F:
                    break;
                case VirtualKey.G:
                    break;
                case VirtualKey.H:
                    break;
                case VirtualKey.I:
                    break;
                case VirtualKey.J:
                    break;
                case VirtualKey.K:
                    break;
                case VirtualKey.L:
                    break;
                case VirtualKey.M:
                    break;
                case VirtualKey.N:
                    break;
                case VirtualKey.O:
                    break;
                case VirtualKey.P:
                    break;
                case VirtualKey.Q:
                    break;
                case VirtualKey.R:
                    break;
                case VirtualKey.S:
                    break;
                case VirtualKey.T:
                    break;
                case VirtualKey.U:
                    break;
                case VirtualKey.V:
                    break;
                case VirtualKey.W:
                    break;
                case VirtualKey.X:
                    break;
                case VirtualKey.Y:
                    break;
                case VirtualKey.Z:
                    break;
                case VirtualKey.LeftWindows:
                    break;
                case VirtualKey.RightWindows:
                    break;
                case VirtualKey.Application:
                    break;
                case VirtualKey.Sleep:
                    break;
                case VirtualKey.NumberPad0:
                    break;
                case VirtualKey.NumberPad1:
                    break;
                case VirtualKey.NumberPad2:
                    break;
                case VirtualKey.NumberPad3:
                    break;
                case VirtualKey.NumberPad4:
                    break;
                case VirtualKey.NumberPad5:
                    break;
                case VirtualKey.NumberPad6:
                    break;
                case VirtualKey.NumberPad7:
                    break;
                case VirtualKey.NumberPad8:
                    break;
                case VirtualKey.NumberPad9:
                    break;
                case VirtualKey.Multiply:
                    break;
                case VirtualKey.Add:
                    break;
                case VirtualKey.Separator:
                    break;
                case VirtualKey.Subtract:
                    break;
                case VirtualKey.Decimal:
                    break;
                case VirtualKey.Divide:
                    break;
                case VirtualKey.F1:
                    break;
                case VirtualKey.F2:
                    break;
                case VirtualKey.F3:
                    break;
                case VirtualKey.F4:
                    break;
                case VirtualKey.F5:
                    break;
                case VirtualKey.F6:
                    break;
                case VirtualKey.F7:
                    break;
                case VirtualKey.F8:
                    break;
                case VirtualKey.F9:
                    break;
                case VirtualKey.F10:
                    break;
                case VirtualKey.F11:
                    break;
                case VirtualKey.F12:
                    break;
                case VirtualKey.F13:
                    break;
                case VirtualKey.F14:
                    break;
                case VirtualKey.F15:
                    break;
                case VirtualKey.F16:
                    break;
                case VirtualKey.F17:
                    break;
                case VirtualKey.F18:
                    break;
                case VirtualKey.F19:
                    break;
                case VirtualKey.F20:
                    break;
                case VirtualKey.F21:
                    break;
                case VirtualKey.F22:
                    break;
                case VirtualKey.F23:
                    break;
                case VirtualKey.F24:
                    break;
                case VirtualKey.NavigationView:
                    break;
                case VirtualKey.NavigationMenu:
                    break;
                case VirtualKey.NavigationUp:
                    break;
                case VirtualKey.NavigationDown:
                    break;
                case VirtualKey.NavigationLeft:
                    break;
                case VirtualKey.NavigationRight:
                    break;
                case VirtualKey.NavigationAccept:
                    break;
                case VirtualKey.NavigationCancel:
                    break;
                case VirtualKey.NumberKeyLock:
                    break;
                case VirtualKey.Scroll:
                    break;
                case VirtualKey.LeftShift:
                    break;
                case VirtualKey.RightShift:
                    break;
                case VirtualKey.LeftControl:
                    break;
                case VirtualKey.RightControl:
                    break;
                case VirtualKey.LeftMenu:
                    break;
                case VirtualKey.RightMenu:
                    break;
                case VirtualKey.GoBack:
                    break;
                case VirtualKey.GoForward:
                    break;
                case VirtualKey.Refresh:
                    break;
                case VirtualKey.Stop:
                    break;
                case VirtualKey.Search:
                    break;
                case VirtualKey.Favorites:
                    break;
                case VirtualKey.GoHome:
                    break;
                case VirtualKey.GamepadA:
                    break;
                case VirtualKey.GamepadB:
                    break;
                case VirtualKey.GamepadX:
                    break;
                case VirtualKey.GamepadY:
                    break;
                case VirtualKey.GamepadRightShoulder:
                    break;
                case VirtualKey.GamepadLeftShoulder:
                    break;
                case VirtualKey.GamepadLeftTrigger:
                    break;
                case VirtualKey.GamepadRightTrigger:
                    break;
                case VirtualKey.GamepadDPadUp:
                    break;
                case VirtualKey.GamepadDPadDown:
                    break;
                case VirtualKey.GamepadDPadLeft:
                    break;
                case VirtualKey.GamepadDPadRight:
                    break;
                case VirtualKey.GamepadMenu:
                    break;
                case VirtualKey.GamepadView:
                    break;
                case VirtualKey.GamepadLeftThumbstickButton:
                    break;
                case VirtualKey.GamepadRightThumbstickButton:
                    break;
                case VirtualKey.GamepadLeftThumbstickUp:
                    break;
                case VirtualKey.GamepadLeftThumbstickDown:
                    break;
                case VirtualKey.GamepadLeftThumbstickRight:
                    break;
                case VirtualKey.GamepadLeftThumbstickLeft:
                    break;
                case VirtualKey.GamepadRightThumbstickUp:
                    break;
                case VirtualKey.GamepadRightThumbstickDown:
                    break;
                case VirtualKey.GamepadRightThumbstickRight:
                    break;
                case VirtualKey.GamepadRightThumbstickLeft:
                    break;
            }

            return MyKey.None;
        }
    }
}