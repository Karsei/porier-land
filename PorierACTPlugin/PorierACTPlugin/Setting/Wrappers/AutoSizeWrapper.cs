using System.ComponentModel;
using System.Drawing.Design;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(AutoSizeWrapperConverter))]
    public class AutoSizeWrapper : GlobalizedSetting
    {
        [Browsable(true)]
        [DisplayName("AUTO_SIZE_WRAPPER_DISPLAY_NAME_ENABLE_AUTO_SIZE")]
        [Description("AUTO_SIZE_WRAPPER_DESCRIPTION_ENABLE_AUTO_SIZE")]
        [TypeConverter(typeof(BoolConverter))]
        public bool EnableAutoSize { get; set; }

        [Browsable(true)]
        [DisplayName("AUTO_SIZE_WRAPPER_DISPLAY_NAME_ANCHOR_LOCATION")]
        [Description("AUTO_SIZE_WRAPPER_DESCRIPTION_ANCHOR_LOCATION")]
        public PointWrapper AnchorLocation { get; set; }

        [Browsable(true)]
        [DisplayName("AUTO_SIZE_WRAPPER_DISPLAY_NAME_ANCHOR_TYPE")]
        [Description("AUTO_SIZE_WRAPPER_DESCRIPTION_ANCHOR_TYPE")]
        [TypeConverter(typeof(AnchorTypeConverter))]
        [Editor(typeof(AnchorTypeTypeEditor), typeof(UITypeEditor))]
        public AnchorType AnchorType { get; set; }
        
        public override string ToString()
        {
            return "(" + (EnableAutoSize ? Language.Resource.GetString("TRUE") : Language.Resource.GetString("FALSE")) + ")";
        }
    }

    public enum AnchorType
    {
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3,
        BottomRight = 4
    }
}