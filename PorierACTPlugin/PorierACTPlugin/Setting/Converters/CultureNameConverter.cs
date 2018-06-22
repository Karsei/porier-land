using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class CultureNameConverter : TypeConverter
    {
        public static Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "ko-KR", "한국어" },
            { "en-GB", "English" }
        };

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(string)) return "";
            if (!SupportedLanguages.ContainsKey((string)value)) return "LOCALE_NOT_FOUND";

            return SupportedLanguages[(string)value];
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class CultureNameTypeEditor : UITypeEditor
    {
        IWindowsFormsEditorService editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            }

            if (editorService != null)
            {
                CultureNameControl cultureNameControl = new CultureNameControl(editorService)
                {
                    SelectedKey = (string)value
                };
                editorService.DropDownControl(cultureNameControl);
                value = cultureNameControl.SelectedKey;
            }

            return value;
        }
    }

    public class CultureNameControl : Panel
    {
        public string SelectedKey;

        private IWindowsFormsEditorService editorService;
        
        public CultureNameControl(IWindowsFormsEditorService editorService) : base()
        {
            this.editorService = editorService;

            AutoScroll = true;
            
            foreach (string value in CultureNameConverter.SupportedLanguages.Values)
            {
                Button button = new Button
                {
                    Dock = DockStyle.Top,
                    Text = value
                };
                button.Click += Button_Click;
                Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            foreach (string key in CultureNameConverter.SupportedLanguages.Keys)
            {
                if (CultureNameConverter.SupportedLanguages[key] == ((Button)sender).Text)
                {
                    SelectedKey = key;
                    break;
                }
            }

            editorService.CloseDropDown();
        }
    }
}