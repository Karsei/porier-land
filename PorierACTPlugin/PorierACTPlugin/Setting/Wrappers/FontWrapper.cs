using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(FontWrapperConverter))]
    [Editor(typeof(FontWrapperTypeEditor), typeof(UITypeEditor))]
    public class FontWrapper
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public FontStyle Style { get; set; }

        public static implicit operator Font(FontWrapper fontWrapper)
        {
            return new Font(fontWrapper.Name, fontWrapper.Size, fontWrapper.Style);
        }

        public static implicit operator FontWrapper(Font font)
        {
            return new FontWrapper
            {
                Name = font.Name,
                Size = (float)Math.Round(font.Size),
                Style = font.Style
            };
        }

        public override string ToString()
        {
            return "(" + Name + ", " + Size + ")";
        }
    }
}