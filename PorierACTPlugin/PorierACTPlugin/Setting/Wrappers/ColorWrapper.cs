using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(ColorWrapperConverter))]
    [Editor(typeof(ColorWrapperTypeEditor), typeof(UITypeEditor))]
    public class ColorWrapper
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public static implicit operator Color(ColorWrapper colorWrapper)
        {
            return Color.FromArgb(colorWrapper.R, colorWrapper.G, colorWrapper.B);
        }

        public static implicit operator ColorWrapper(Color color)
        {
            return new ColorWrapper
            {
                R = color.R,
                G = color.G,
                B = color.B
            };
        }

        public override string ToString()
        {
            return "R: " + R.ToString() + ", G: " + G.ToString() + ", B: " + B.ToString();
        }
    }
}