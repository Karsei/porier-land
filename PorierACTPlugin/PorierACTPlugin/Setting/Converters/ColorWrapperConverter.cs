using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class ColorWrapperConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(ColorWrapper)) return "";

            return ((ColorWrapper)value).ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class ColorWrapperTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            e.Graphics.FillRectangle(
                new SolidBrush((ColorWrapper)e.Value),
                e.Bounds);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            }

            if (editorService != null)
            {
                ColorWrapperForm colorWrapperForm = new ColorWrapperForm
                {
                    ColorWrapper = (ColorWrapper)value
                };

                if (editorService.ShowDialog(colorWrapperForm) == DialogResult.OK)
                {
                    value = colorWrapperForm.ColorWrapper;
                }

                colorWrapperForm.Dispose();
            }

            return value;
        }
    }

    public class ColorWrapperForm : Form
    {
        public ColorWrapper ColorWrapper;

        protected override void OnShown(EventArgs e)
        {
            Hide();

            TopMost = true;

            ColorDialog colorDialog = new ColorDialog
            {
                AllowFullOpen = true,
                AnyColor = true,
                Color = ColorWrapper,
                FullOpen = true,
                SolidColorOnly = false
            };

            DialogResult = colorDialog.ShowDialog();

            ColorWrapper = colorDialog.Color;

            Close();
        }
    }
}