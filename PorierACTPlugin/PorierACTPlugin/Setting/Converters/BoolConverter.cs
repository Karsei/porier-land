using System;
using System.ComponentModel;
using System.Globalization;

namespace PorierACTPlugin
{
    public class BoolConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return (bool)value ? Language.Resource.GetString("TRUE") : Language.Resource.GetString("FALSE");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (string)value == Language.Resource.GetString("TRUE");
        }
    }
}