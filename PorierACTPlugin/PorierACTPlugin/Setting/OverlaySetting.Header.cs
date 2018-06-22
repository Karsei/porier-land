using System.ComponentModel;
using System.Drawing;

namespace PorierACTPlugin
{
    public partial class OverlaySetting
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
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_BACK_COLOR")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_BACK_COLOR")]
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
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_HEIGHT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_HEIGHT")]
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
        private int headerHeight = 50;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_COLLAPSED_HEIGHT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_COLLAPSED_HEIGHT")]
        public int HeaderCollapsedHeight
        {
            get
            {
                return headerCollapsedHeight;
            }

            set
            {
                headerCollapsedHeight = value;
            }
        }
        private int headerCollapsedHeight = 25;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_COLLAPSED_TEXT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_COLLAPSED_TEXT")]
        public TextWrapper HeaderCollapsedText
        {
            get
            {
                return headerCollapsedText;
            }

            set
            {
                headerCollapsedText = value;
            }
        }
        private TextWrapper headerCollapsedText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 10f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(230, 230, 230)
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_DURATION")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_DURATION")]
        [TypeConverter(typeof(BoolConverter))]
        public bool HeaderShowDuration
        {
            get
            {
                return headerShowDuration;
            }

            set
            {
                headerShowDuration = value;
            }
        }
        private bool headerShowDuration = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_DURATION_TEXT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_DURATION_TEXT")]
        public TextWrapper HeaderDurationText
        {
            get
            {
                return headerDurationText;
            }

            set
            {
                headerDurationText = value;
            }
        }
        private TextWrapper headerDurationText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 15f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(87, 34, 255)
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_ENCOUNTER_NAME")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_ENCOUNTER_NAME")]
        [TypeConverter(typeof(BoolConverter))]
        public bool HeaderShowEncounterName
        {
            get
            {
                return headerShowEncounterName;
            }

            set
            {
                headerShowEncounterName = value;
            }
        }
        private bool headerShowEncounterName = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_ENCOUNTER_NAME_TEXT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_ENCOUNTER_NAME_TEXT")]
        public TextWrapper HeaderEncounterNameText
        {
            get
            {
                return headerEncounterNameText;
            }

            set
            {
                headerEncounterNameText = value;
            }
        }
        private TextWrapper headerEncounterNameText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 15f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(225, 225, 225)
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_RDPS")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_RDPS")]
        [TypeConverter(typeof(BoolConverter))]
        public bool HeaderShowRdps
        {
            get
            {
                return headerShowRdps;
            }

            set
            {
                headerShowRdps = value;
            }
        }
        private bool headerShowRdps = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_RDPS_TEXT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_RDPS_TEXT")]
        public TextWrapper HeaderRdpsText
        {
            get
            {
                return headerRdpsText;
            }

            set
            {
                headerRdpsText = value;
            }
        }
        private TextWrapper headerRdpsText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 10f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(102, 77, 127)
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_RHPS")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_RHPS")]
        [TypeConverter(typeof(BoolConverter))]
        public bool HeaderShowRhps
        {
            get
            {
                return headerShowRhps;
            }

            set
            {
                headerShowRhps = value;
            }
        }
        private bool headerShowRhps = true;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_HEADER")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_HEADER_RHPS_TEXT")]
        [Description("OVERLAY_SETTING_DESCRIPTION_HEADER_RHPS_TEXT")]
        public TextWrapper HeaderRhpsText
        {
            get
            {
                return headerRhpsText;
            }

            set
            {
                headerRhpsText = value;
            }
        }
        private TextWrapper headerRhpsText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 10f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(127, 77, 102)
        };
    }
}