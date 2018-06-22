using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public class MainTabSetting : GlobalizedSetting
    {
        [Browsable(true)]
        [Category("MAIN_TAB_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("MAIN_TAB_SETTING_DISPLAY_NAME_CULTURE_NAME")]
        [Description("MAIN_TAB_SETTING_DESCRIPTION_CULTURE_NAME")]
        [TypeConverter(typeof(CultureNameConverter))]
        [Editor(typeof(CultureNameTypeEditor), typeof(UITypeEditor))]
        public string CultureName
        {
            get
            {
                return cultureName;
            }

            set
            {
                cultureName = value;
            }
        }
        private string cultureName = "ko-KR";

        [Browsable(true)]
        [Category("MAIN_TAB_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("MAIN_TAB_SETTING_DISPLAY_NAME_GAME_MODE_HOT_KEY")]
        [Description("MAIN_TAB_SETTING_DESCRIPTION_GAME_MODE_HOT_KEY")]
        public HotKeyWrapper GameModeHotKey
        {
            get
            {
                return gameModeHotKey;
            }

            set
            {
                gameModeHotKey = value;
            }
        }
        private HotKeyWrapper gameModeHotKey = new HotKeyWrapper
        {
            Modifiers = 6,
            KeyCode = Keys.Z,
            KeyString = "Ctrl + Shift + Z"
        };

        [Browsable(true)]
        [Category("MAIN_TAB_SETTING_CATEGORY_FUNCTIONALITY")]
        [DisplayName("MAIN_TAB_SETTING_DISPLAY_NAME_HIDE_ALL_HOT_KEY")]
        [Description("MAIN_TAB_SETTING_DESCRIPTION_HIDE_ALL_HOT_KEY")]
        public HotKeyWrapper HideAllHotKey
        {
            get
            {
                return hideAllHotKey;
            }

            set
            {
                hideAllHotKey = value;
            }
        }
        private HotKeyWrapper hideAllHotKey = new HotKeyWrapper
        {
            Modifiers = 6,
            KeyCode = Keys.H,
            KeyString = "Ctrl + Shift + H"
        };

        [Browsable(true)]
        [Category("MAIN_TAB_SETTING_CATEGORY_OVERLAYS")]
        [DisplayName("MAIN_TAB_SETTING_DISPLAY_NAME_SHOW_OVERLAY")]
        [Description("MAIN_TAB_SETTING_DESCRIPTION_SHOW_OVERLAY")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowOverlay
        {
            get
            {
                return showOverlay;
            }

            set
            {
                showOverlay = value;
            }
        }
        private bool showOverlay = true;

        [Browsable(true)]
        [Category("MAIN_TAB_SETTING_CATEGORY_OVERLAYS")]
        [DisplayName("MAIN_TAB_SETTING_DISPLAY_NAME_SHOW_YUDIE")]
        [Description("MAIN_TAB_SETTING_DESCRIPTION_SHOW_YUDIE")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowYUDie
        {
            get
            {
                return showYUDie;
            }

            set
            {
                showYUDie = value;
            }
        }
        private bool showYUDie = true;
    }
}