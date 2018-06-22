using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class OverlaySetting
    {
        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_CAPTURE_HOT_KEY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_CAPTURE_HOT_KEY")]
        public HotKeyWrapper CaptureHotKey
        {
            get
            {
                return captureHotKey;
            }

            set
            {
                captureHotKey = value;
            }
        }
        private HotKeyWrapper captureHotKey = new HotKeyWrapper
        {
            Modifiers = 6,
            KeyCode = Keys.C,
            KeyString = "Ctrl + Shift + C"
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_CAPTURE_SAVE_PATH")]
        [Description("OVERLAY_SETTING_DESCRIPTION_CAPTURE_SAVE_PATH")]
        [TypeConverter(typeof(PathConverter))]
        [Editor(typeof(PathTypeEditor), typeof(UITypeEditor))]
        public string CaptureSavePath
        {
            get
            {
                return captureSavePath;
            }

            set
            {
                captureSavePath = value;
            }
        }
        private string captureSavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_PUT_CAPTURE_IN_CLIPBOARD")]
        [Description("OVERLAY_SETTING_DESCRIPTION_PUT_CAPTURE_IN_CLIPBOARD")]
        [TypeConverter(typeof(BoolConverter))]
        public bool PutCaptureInClipboard
        {
            get
            {
                return putCaptureInClipboard;
            }

            set
            {
                putCaptureInClipboard = value;
            }
        }
        private bool putCaptureInClipboard = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HIDE_NAMES_WHEN_CAPTURING")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HIDE_NAMES_WHEN_CAPTURING")]
        [TypeConverter(typeof(BoolConverter))]
        public bool HideNamesWhenCapturing
        {
            get
            {
                return hideNamesWhenCapturing;
            }

            set
            {
                hideNamesWhenCapturing = value;
            }
        }
        private bool hideNamesWhenCapturing = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_END_COMBAT_HOT_KEY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_END_COMBAT_HOT_KEY")]
        public HotKeyWrapper EndCombatHotKey
        {
            get
            {
                return endCombatHotKey;
            }

            set
            {
                endCombatHotKey = value;
            }
        }
        private HotKeyWrapper endCombatHotKey = new HotKeyWrapper
        {
            Modifiers = 6,
            KeyCode = Keys.E,
            KeyString = "Ctrl + Shift + E"
        };
    }
}