using System.ComponentModel;
using System.Drawing;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(PointWrapperConverter))]
    public class PointWrapper : GlobalizedSetting
    {
        [Browsable(true)]
        [DisplayName("POINT_WRAPPER_DISPLAY_NAME_X")]
        [Description("POINT_WRAPPER_DESCRIPTION_X")]
        public int X { get; set; }

        [Browsable(true)]
        [DisplayName("POINT_WRAPPER_DISPLAY_NAME_Y")]
        [Description("POINT_WRAPPER_DESCRIPTION_Y")]
        public int Y { get; set; }

        public static implicit operator Point(PointWrapper pointWrapper)
        {
            return new Point(pointWrapper.X, pointWrapper.Y);
        }

        public void SetPoint(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        
        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}