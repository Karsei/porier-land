using System.ComponentModel;
using System.Drawing;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(SizeWrapperConverter))]
    public class SizeWrapper : GlobalizedSetting
    {
        [Browsable(true)]
        [DisplayName("SIZE_WRAPPER_DISPLAY_NAME_WIDTH")]
        [Description("SIZE_WRAPPER_DESCRIPTION_WIDTH")]
        public int Width { get; set; }

        [Browsable(true)]
        [DisplayName("SIZE_WRAPPER_DISPLAY_NAME_HEIGHT")]
        [Description("SIZE_WRAPPER_DESCRIPTION_HEIGHT")]
        public int Height { get; set; }

        public static implicit operator Size(SizeWrapper sizeWrapper)
        {
            return new Size(sizeWrapper.Width, sizeWrapper.Height);
        }

        public void SetSize(Size size)
        {
            Width = size.Width;
            Height = size.Height;
        }
        
        public override string ToString()
        {
            return "(" + Width + ", " + Height + ")";
        }
    }
}