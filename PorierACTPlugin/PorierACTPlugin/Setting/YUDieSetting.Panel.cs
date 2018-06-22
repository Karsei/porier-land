using System.ComponentModel;
using System.Drawing;

namespace PorierACTPlugin
{
    partial class YUDieSetting
    {
        [Browsable(true)]
        [Category("YUDIE_SETTING_CATEGORY_PANEL")]
        [DisplayName("YUDIE_SETTING_DISPLAY_NAME_PANEL_TEXT")]
        [Description("YUDIE_SETTING_DESCRIPTION_PANEL_TEXT")]
        public TextWrapper PanelText
        {
            get
            {
                return panelText;
            }

            set
            {
                panelText = value;
            }
        }
        private TextWrapper panelText = new TextWrapper
        {
            ColorWrapper = Color.FromArgb(230, 230, 230),
            FontWrapper = new Font("맑은 고딕", 8f, FontStyle.Regular)
        };
    }
}