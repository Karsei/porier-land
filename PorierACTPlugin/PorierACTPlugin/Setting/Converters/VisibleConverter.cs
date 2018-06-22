using System;
using System.ComponentModel;
using System.Globalization;

namespace PorierACTPlugin
{
    public class VisibleConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return (bool)value ? Language.Resource.GetString("TRUE_VISIBLE") : Language.Resource.GetString("FALSE_VISIBLE");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (string)value == Language.Resource.GetString("TRUE_VISIBLE");
        }
    }
}