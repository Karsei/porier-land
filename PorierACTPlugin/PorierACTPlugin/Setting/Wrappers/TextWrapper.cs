using System.ComponentModel;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(TextWrapperConverter))]
    public class TextWrapper : GlobalizedSetting
    {
        [Browsable(true)]
        [DisplayName("TEXT_WRAPPER_DISPLAY_NAME_COLOR_WRAPPER")]
        [Description("TEXT_WRAPPER_DESCRIPTION_COLOR_WRAPPER")]
        public ColorWrapper ColorWrapper { get; set; }

        [Browsable(true)]
        [DisplayName("TEXT_WRAPPER_DISPLAY_NAME_FONT_WRAPPER")]
        [Description("TEXT_WRAPPER_DESCRIPTION_FONT_WRAPPER")]
        public FontWrapper FontWrapper { get; set; }
    }
}