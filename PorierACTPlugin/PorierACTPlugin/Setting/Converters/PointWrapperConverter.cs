using System;
using System.ComponentModel;
using System.Globalization;

namespace PorierACTPlugin
{
    public class PointWrapperConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(PointWrapper)) return "";

            return ((PointWrapper)value).ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }
}