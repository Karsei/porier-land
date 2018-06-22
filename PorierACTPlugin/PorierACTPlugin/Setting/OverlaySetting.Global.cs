using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace PorierACTPlugin
{
    public partial class OverlaySetting
    {
        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_LOCATION")]
        [Description("OVERLAY_SETTING_DESCRIPTION_LOCATION")]
        public PointWrapper Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }
        private PointWrapper location = new PointWrapper
        {
            X = 0,
            Y = 0
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_SIZE")]
        [Description("OVERLAY_SETTING_DESCRIPTION_SIZE")]
        public SizeWrapper Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }
        private SizeWrapper size = new SizeWrapper
        {
            Width = 500,
            Height = 400
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_AUTO_SIZE")]
        [Description("OVERLAY_SETTING_DESCRIPTION_AUTO_SIZE")]
        public AutoSizeWrapper AutoSize
        {
            get
            {
                return autoSize;
            }

            set
            {
                autoSize = value;
            }
        }
        private AutoSizeWrapper autoSize = new AutoSizeWrapper
        {
            EnableAutoSize = true,
            AnchorLocation = new PointWrapper
            {
                X = 0,
                Y = 0
            },
            AnchorType = AnchorType.TopLeft
        };

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_AUTO_SIZE_TABLE_DISTANCE")]
        [Description("OVERLAY_SETTING_DESCRIPTION_AUTO_SIZE_TABLE_DISTANCE")]
        public int AutoSizeTableDistance
        {
            get
            {
                return autoSizeTableDistance;
            }

            set
            {
                autoSizeTableDistance = value;
            }
        }
        private int autoSizeTableDistance = 50;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_BACK_COLOR")]
        [Description("OVERLAY_SETTING_DESCRIPTION_BACK_COLOR")]
        public ColorWrapper BackColor
        {
            get
            {
                return backColor;
            }

            set
            {
                backColor = value;
            }
        }
        private ColorWrapper backColor = Color.FromArgb(200, 50, 50);

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_BOUNDARY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_BOUNDARY")]
        public int Boundary
        {
            get
            {
                return boundary;
            }

            set
            {
                boundary = value;
            }
        }
        private int boundary = 2;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_GAME_MODE_BACK_COLOR")]
        [Description("OVERLAY_SETTING_DESCRIPTION_GAME_MODE_BACK_COLOR")]
        public ColorWrapper GameModeBackColor
        {
            get
            {
                return gameModeBackColor;
            }

            set
            {
                gameModeBackColor = value;
            }
        }
        private ColorWrapper gameModeBackColor = Color.FromArgb(25, 25, 25);

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_GAME_MODE_BOUNDARY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_GAME_MODE_BOUNDARY")]
        public int GameModeBoundary
        {
            get
            {
                return gameModeBoundary;
            }

            set
            {
                gameModeBoundary = value;
            }
        }
        private int gameModeBoundary = 2;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_BACKGROUND_FORM_BACK_COLOR")]
        [Description("OVERLAY_SETTING_DESCRIPTION_BACKGROUND_FORM_BACK_COLOR")]
        public ColorWrapper BackgroundFormBackColor
        {
            get
            {
                return backgroundFormBackColor;
            }

            set
            {
                backgroundFormBackColor = value;
            }
        }
        private ColorWrapper backgroundFormBackColor = Color.FromArgb(0, 0, 0);

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_BACKGROUND_FORM_OPACITY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_BACKGROUND_FORM_OPACITY")]
        [Editor(typeof(OpacityTypeEditor), typeof(UITypeEditor))]
        public double BackgroundFormOpacity
        {
            get
            {
                return backgroundFormOpacity;
            }

            set
            {
                backgroundFormOpacity = value;
            }
        }
        private double backgroundFormOpacity = 0.75d;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_OPACITY")]
        [Description("OVERLAY_SETTING_DESCRIPTION_OPACITY")]
        [Editor(typeof(OpacityTypeEditor), typeof(UITypeEditor))]
        public double Opacity
        {
            get
            {
                return opacity;
            }

            set
            {
                opacity = value;
            }
        }
        private double opacity = 0.9d;

        [Browsable(true)]
        [Category("OVERLAY_SETTING_CATEGORY_GLOBAL")]
        [DisplayName("OVERLAY_SETTING_DISPLAY_NAME_ROUND_CORNER")]
        [Description("OVERLAY_SETTING_DESCRIPTION_ROUND_CORNER")]
        public SizeWrapper RoundCorner
        {
            get
            {
                return roundCorner;
            }

            set
            {
                roundCorner = value;
            }
        }
        private SizeWrapper roundCorner = new SizeWrapper
        {
            Width = 5,
            Height = 5
        };
    }
}