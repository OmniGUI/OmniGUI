using System;
using Android.Graphics;
using Android.Views;
using OmniGui;
using Color = OmniGui.Color;

namespace AndroidApp.AndPlugin
{
    public static class ConversionExtensions {

        public static PointF ToAndroid(this OmniGui.Geometry.Point point)
        {
            return new PointF((int)point.X, (int)point.Y);
        }

        public static global::Android.Graphics.Color ToAndroid(this OmniGui.Color color)
        {
            return new global::Android.Graphics.Color(color.Red, color.Green, color.Blue, color.Alpha);
        }

        public static RectF ToAndroid(this OmniGui.Geometry.Rect rect)
        {
            return new RectF((float)rect.X, (float)rect.Y, (float)(rect.X + rect.Width), (float)(rect.Y + rect.Height));
        }

        public static MyKey ToOmniGui(this Keycode key)
        {
            switch (key)
            {
                case Keycode.Num0:
                    break;
                case Keycode.Num1:
                    break;
                case Keycode.K11:
                    break;
                case Keycode.K12:
                    break;
                case Keycode.Num2:
                    break;
                case Keycode.Num3:
                    break;
                case Keycode.ThreeDMode:
                    break;
                case Keycode.Num4:
                    break;
                case Keycode.Num5:
                    break;
                case Keycode.Num6:
                    break;
                case Keycode.Num7:
                    break;
                case Keycode.Num8:
                    break;
                case Keycode.Num9:
                    break;
                case Keycode.A:
                    break;
                case Keycode.AltLeft:
                    break;
                case Keycode.AltRight:
                    break;
                case Keycode.Apostrophe:
                    break;
                case Keycode.AppSwitch:
                    break;
                case Keycode.Assist:
                    break;
                case Keycode.At:
                    break;
                case Keycode.AvrInput:
                    break;
                case Keycode.AvrPower:
                    break;
                case Keycode.B:
                    break;
                case Keycode.Back:
                    break;
                case Keycode.Backslash:
                    break;
                case Keycode.Bookmark:
                    break;
                case Keycode.Break:
                    break;
                case Keycode.BrightnessDown:
                    break;
                case Keycode.BrightnessUp:
                    break;
                case Keycode.Button1:
                    break;
                case Keycode.Button10:
                    break;
                case Keycode.Button11:
                    break;
                case Keycode.Button12:
                    break;
                case Keycode.Button13:
                    break;
                case Keycode.Button14:
                    break;
                case Keycode.Button15:
                    break;
                case Keycode.Button16:
                    break;
                case Keycode.Button2:
                    break;
                case Keycode.Button3:
                    break;
                case Keycode.Button4:
                    break;
                case Keycode.Button5:
                    break;
                case Keycode.Button6:
                    break;
                case Keycode.Button7:
                    break;
                case Keycode.Button8:
                    break;
                case Keycode.Button9:
                    break;
                case Keycode.ButtonA:
                    break;
                case Keycode.ButtonB:
                    break;
                case Keycode.ButtonC:
                    break;
                case Keycode.ButtonL1:
                    break;
                case Keycode.ButtonL2:
                    break;
                case Keycode.ButtonMode:
                    break;
                case Keycode.ButtonR1:
                    break;
                case Keycode.ButtonR2:
                    break;
                case Keycode.ButtonSelect:
                    break;
                case Keycode.ButtonStart:
                    break;
                case Keycode.ButtonThumbl:
                    break;
                case Keycode.ButtonThumbr:
                    break;
                case Keycode.ButtonX:
                    break;
                case Keycode.ButtonY:
                    break;
                case Keycode.ButtonZ:
                    break;
                case Keycode.C:
                    break;
                case Keycode.Calculator:
                    break;
                case Keycode.Calendar:
                    break;
                case Keycode.Call:
                    break;
                case Keycode.Camera:
                    break;
                case Keycode.CapsLock:
                    break;
                case Keycode.Captions:
                    break;
                case Keycode.ChannelDown:
                    break;
                case Keycode.ChannelUp:
                    break;
                case Keycode.Clear:
                    break;
                case Keycode.Comma:
                    break;
                case Keycode.Contacts:
                    break;
                case Keycode.CtrlLeft:
                    break;
                case Keycode.CtrlRight:
                    break;
                case Keycode.D:
                    break;
                case Keycode.Del:
                    break;
                case Keycode.DpadCenter:
                    break;
                case Keycode.DpadDown:
                    break;
                case Keycode.DpadLeft:
                    break;
                case Keycode.DpadRight:
                    break;
                case Keycode.DpadUp:
                    break;
                case Keycode.Dvr:
                    break;
                case Keycode.E:
                    break;
                case Keycode.Eisu:
                    break;
                case Keycode.Endcall:
                    break;
                case Keycode.Enter:
                    break;
                case Keycode.Envelope:
                    break;
                case Keycode.Equals:
                    break;
                case Keycode.Escape:
                    break;
                case Keycode.Explorer:
                    break;
                case Keycode.F:
                    break;
                case Keycode.F1:
                    break;
                case Keycode.F10:
                    break;
                case Keycode.F11:
                    break;
                case Keycode.F12:
                    break;
                case Keycode.F2:
                    break;
                case Keycode.F3:
                    break;
                case Keycode.F4:
                    break;
                case Keycode.F5:
                    break;
                case Keycode.F6:
                    break;
                case Keycode.F7:
                    break;
                case Keycode.F8:
                    break;
                case Keycode.F9:
                    break;
                case Keycode.Focus:
                    break;
                case Keycode.Forward:
                    break;
                case Keycode.ForwardDel:
                    break;
                case Keycode.Function:
                    break;
                case Keycode.G:
                    break;
                case Keycode.Grave:
                    break;
                case Keycode.Guide:
                    break;
                case Keycode.H:
                    break;
                case Keycode.Headsethook:
                    break;
                case Keycode.Help:
                    break;
                case Keycode.Henkan:
                    break;
                case Keycode.Home:
                    break;
                case Keycode.I:
                    break;
                case Keycode.Info:
                    break;
                case Keycode.Insert:
                    break;
                case Keycode.J:
                    break;
                case Keycode.K:
                    break;
                case Keycode.Kana:
                    break;
                case Keycode.KatakanaHiragana:
                    break;
                case Keycode.L:
                    break;
                case Keycode.LanguageSwitch:
                    break;
                case Keycode.LastChannel:
                    break;
                case Keycode.LeftBracket:
                    break;
                case Keycode.M:
                    break;
                case Keycode.MannerMode:
                    break;
                case Keycode.MediaAudioTrack:
                    break;
                case Keycode.MediaClose:
                    break;
                case Keycode.MediaEject:
                    break;
                case Keycode.MediaFastForward:
                    break;
                case Keycode.MediaNext:
                    break;
                case Keycode.MediaPause:
                    break;
                case Keycode.MediaPlay:
                    break;
                case Keycode.MediaPlayPause:
                    break;
                case Keycode.MediaPrevious:
                    break;
                case Keycode.MediaRecord:
                    break;
                case Keycode.MediaRewind:
                    break;
                case Keycode.MediaSkipBackward:
                    break;
                case Keycode.MediaSkipForward:
                    break;
                case Keycode.MediaStepBackward:
                    break;
                case Keycode.MediaStepForward:
                    break;
                case Keycode.MediaStop:
                    break;
                case Keycode.MediaTopMenu:
                    break;
                case Keycode.Menu:
                    break;
                case Keycode.MetaLeft:
                    break;
                case Keycode.MetaRight:
                    break;
                case Keycode.Minus:
                    break;
                case Keycode.MoveEnd:
                    break;
                case Keycode.MoveHome:
                    break;
                case Keycode.Muhenkan:
                    break;
                case Keycode.Music:
                    break;
                case Keycode.Mute:
                    break;
                case Keycode.N:
                    break;
                case Keycode.NavigateIn:
                    break;
                case Keycode.NavigateNext:
                    break;
                case Keycode.NavigateOut:
                    break;
                case Keycode.NavigatePrevious:
                    break;
                case Keycode.Notification:
                    break;
                case Keycode.Num:
                    break;
                case Keycode.Numpad0:
                    break;
                case Keycode.Numpad1:
                    break;
                case Keycode.Numpad2:
                    break;
                case Keycode.Numpad3:
                    break;
                case Keycode.Numpad4:
                    break;
                case Keycode.Numpad5:
                    break;
                case Keycode.Numpad6:
                    break;
                case Keycode.Numpad7:
                    break;
                case Keycode.Numpad8:
                    break;
                case Keycode.Numpad9:
                    break;
                case Keycode.NumpadAdd:
                    break;
                case Keycode.NumpadComma:
                    break;
                case Keycode.NumpadDivide:
                    break;
                case Keycode.NumpadDot:
                    break;
                case Keycode.NumpadEnter:
                    break;
                case Keycode.NumpadEquals:
                    break;
                case Keycode.NumpadLeftParen:
                    break;
                case Keycode.NumpadMultiply:
                    break;
                case Keycode.NumpadRightParen:
                    break;
                case Keycode.NumpadSubtract:
                    break;
                case Keycode.NumLock:
                    break;
                case Keycode.O:
                    break;
                case Keycode.P:
                    break;
                case Keycode.PageDown:
                    break;
                case Keycode.PageUp:
                    break;
                case Keycode.Pairing:
                    break;
                case Keycode.Period:
                    break;
                case Keycode.Pictsymbols:
                    break;
                case Keycode.Plus:
                    break;
                case Keycode.Pound:
                    break;
                case Keycode.Power:
                    break;
                case Keycode.ProgBlue:
                    break;
                case Keycode.ProgGreen:
                    break;
                case Keycode.ProgRed:
                    break;
                case Keycode.ProgYellow:
                    break;
                case Keycode.Q:
                    break;
                case Keycode.R:
                    break;
                case Keycode.RightBracket:
                    break;
                case Keycode.Ro:
                    break;
                case Keycode.S:
                    break;
                case Keycode.ScrollLock:
                    break;
                case Keycode.Search:
                    break;
                case Keycode.Semicolon:
                    break;
                case Keycode.Settings:
                    break;
                case Keycode.ShiftLeft:
                    break;
                case Keycode.ShiftRight:
                    break;
                case Keycode.Slash:
                    break;
                case Keycode.Sleep:
                    break;
                case Keycode.SoftLeft:
                    break;
                case Keycode.SoftRight:
                    break;
                case Keycode.Space:
                    break;
                case Keycode.Star:
                    break;
                case Keycode.StbInput:
                    break;
                case Keycode.StbPower:
                    break;
                case Keycode.SwitchCharset:
                    break;
                case Keycode.Sym:
                    break;
                case Keycode.Sysrq:
                    break;
                case Keycode.T:
                    break;
                case Keycode.Tab:
                    break;
                case Keycode.Tv:
                    break;
                case Keycode.TvAntennaCable:
                    break;
                case Keycode.TvAudioDescription:
                    break;
                case Keycode.TvAudioDescriptionMixDown:
                    break;
                case Keycode.TvAudioDescriptionMixUp:
                    break;
                case Keycode.TvContentsMenu:
                    break;
                case Keycode.TvDataService:
                    break;
                case Keycode.TvInput:
                    break;
                case Keycode.TvInputComponent1:
                    break;
                case Keycode.TvInputComponent2:
                    break;
                case Keycode.TvInputComposite1:
                    break;
                case Keycode.TvInputComposite2:
                    break;
                case Keycode.TvInputHdmi1:
                    break;
                case Keycode.TvInputHdmi2:
                    break;
                case Keycode.TvInputHdmi3:
                    break;
                case Keycode.TvInputHdmi4:
                    break;
                case Keycode.TvInputVga1:
                    break;
                case Keycode.TvMediaContextMenu:
                    break;
                case Keycode.TvNetwork:
                    break;
                case Keycode.TvNumberEntry:
                    break;
                case Keycode.TvPower:
                    break;
                case Keycode.TvRadioService:
                    break;
                case Keycode.TvSatellite:
                    break;
                case Keycode.TvSatelliteBs:
                    break;
                case Keycode.TvSatelliteCs:
                    break;
                case Keycode.TvSatelliteService:
                    break;
                case Keycode.TvTeletext:
                    break;
                case Keycode.TvTerrestrialAnalog:
                    break;
                case Keycode.TvTerrestrialDigital:
                    break;
                case Keycode.TvTimerProgramming:
                    break;
                case Keycode.TvZoomMode:
                    break;
                case Keycode.U:
                    break;
                case Keycode.Unknown:
                    break;
                case Keycode.V:
                    break;
                case Keycode.VoiceAssist:
                    break;
                case Keycode.VolumeDown:
                    break;
                case Keycode.VolumeMute:
                    break;
                case Keycode.VolumeUp:
                    break;
                case Keycode.W:
                    break;
                case Keycode.Wakeup:
                    break;
                case Keycode.Window:
                    break;
                case Keycode.X:
                    break;
                case Keycode.Y:
                    break;
                case Keycode.Yen:
                    break;
                case Keycode.Z:
                    break;
                case Keycode.ZenkakuHankaku:
                    break;
                case Keycode.ZoomIn:
                    break;
                case Keycode.ZoomOut:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            return MyKey.None;
        }
    }
}