using System.ComponentModel;
using System.Drawing;

namespace PorierACTPlugin
{
    public partial class YUDieSetting
    {
        [Browsable(false)]
        public bool HeaderIsCollapsed
        {
            get
            {
                return headerIsCollapsed;
            }

            set
            {
                headerIsCollapsed = value;
            }
        }
        private bool headerIsCollapsed = false;

        [Browsable(true)]
        [Category("YUDIE_SETTING_CATEGORY_HEADER")]
        [DisplayName("YUDIE_SETTING_DISPLAY_NAME_HEADER_BACK_COLOR")]
        [Description("YUDIE_SETTING_DESCRIPTION_HEADER_BACK_COLOR")]
        public ColorWrapper HeaderBackColor
        {
            get
            {
                return headerBackColor;
            }

            set
            {
                headerBackColor = value;
            }
        }
        private ColorWrapper headerBackColor = Color.FromArgb(25, 25, 25);

        [Browsable(true)]
        [Category("YUDIE_SETTING_CATEGORY_HEADER")]
        [DisplayName("YUDIE_SETTING_DISPLAY_NAME_HEADER_HEIGHT")]
        [Description("YUDIE_SETTING_DESCRIPTION_HEADER_HEIGHT")]
        public int HeaderHeight
        {
            get
            {
                return headerHeight;
            }

            set
            {
                headerHeight = value;
            }
        }
        private int headerHeight = 30;

        [Browsable(true)]
        [Category("YUDIE_SETTING_CATEGORY_HEADER")]
        [DisplayName("YUDIE_SETTING_DISPLAY_NAME_HEADER_TEXT")]
        [Description("YUDIE_SETTING_DESCRIPTION_HEADER_TEXT")]
        public TextWrapper HeaderText
        {
            get
            {
                return headerText;
            }

            set
            {
                headerText = value;
            }
        }
        private TextWrapper headerText = new TextWrapper
        {
            ColorWrapper = Color.FromArgb(230, 230, 230),
            FontWrapper = new Font("맑은 고딕", 15f, FontStyle.Regular)
        };
    }
}