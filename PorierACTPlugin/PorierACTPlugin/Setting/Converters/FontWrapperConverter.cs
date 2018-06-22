using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class FontWrapperConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(FontWrapper)) return "";

            return ((FontWrapper)value).ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class FontWrapperTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

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
                FontWrapperForm fontWrapperForm = new FontWrapperForm
                {
                    FontWrapper = (FontWrapper)value
                };

                if (editorService.ShowDialog(fontWrapperForm) == DialogResult.OK)
                {
                    value = fontWrapperForm.FontWrapper;
                }

                fontWrapperForm.Dispose();
            }

            return value;
        }
    }

    public class FontWrapperForm : Form
    {
        public FontWrapper FontWrapper;

        protected override void OnShown(EventArgs e)
        {
            Hide();

            TopMost = true;

            FontDialog fontDialog = new FontDialog
            {
                Font = FontWrapper
            };

            DialogResult = fontDialog.ShowDialog();

            FontWrapper = fontDialog.Font;

            Close();
        }
    }
}