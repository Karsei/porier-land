using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class CurrentGraphDataPropertyNameConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(string)) return "";

            foreach (DataGridViewColumn column in ActPlugin.Overlay.DpsDataGridView.Columns)
            {
                if (column.DataPropertyName == (string)value)
                {
                    return column.Name;
                }
            }

            foreach (DataGridViewColumn column in ActPlugin.Overlay.HpsDataGridView.Columns)
            {
                if (column.DataPropertyName == (string)value)
                {
                    return column.Name;
                }
            }

            return "";
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class CurrentGraphDataPropertyNameTypeEditor : UITypeEditor
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
                CurrentGraphDataPropertyNameControl currentGraphDataPropertyNameControl = new CurrentGraphDataPropertyNameControl(editorService)
                {
                    DataPropertyName = (string)value
                };
                editorService.DropDownControl(currentGraphDataPropertyNameControl);
                value = currentGraphDataPropertyNameControl.DataPropertyName;
            }

            return value;
        }
    }

    public class CurrentGraphDataPropertyNameControl : Panel
    {
        public string DataPropertyName;

        IWindowsFormsEditorService editorService;
        List<string> items;

        public CurrentGraphDataPropertyNameControl(IWindowsFormsEditorService editorService) : base()
        {
            this.editorService = editorService;

            AutoScroll = true;

            items = new List<string>();
            
            foreach (DataGridViewColumn column in ActPlugin.Overlay.DpsDataGridView.Columns)
            {
                if (!items.Contains(column.Name))
                {
                    items.Add(column.Name);

                    Button button = new Button
                    {
                        Dock = DockStyle.Top,
                        Text = column.Name
                    };
                    button.Click += Button_Click;
                    Controls.Add(button);
                }
            }

            foreach (DataGridViewColumn column in ActPlugin.Overlay.HpsDataGridView.Columns)
            {
                if (!items.Contains(column.Name))
                {
                    items.Add(column.Name);

                    Button button = new Button
                    {
                        Dock = DockStyle.Top,
                        Text = column.Name
                    };
                    button.Click += Button_Click;
                    Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in ActPlugin.Overlay.DpsDataGridView.Columns)
            {
                if (column.Name == ((Button)sender).Text)
                {
                    DataPropertyName = column.DataPropertyName;
                    break;
                }
            }

            foreach (DataGridViewColumn column in ActPlugin.Overlay.HpsDataGridView.Columns)
            {
                if (column.Name == ((Button)sender).Text)
                {
                    DataPropertyName = column.DataPropertyName;
                    break;
                }
            }

            editorService.CloseDropDown();
        }
    }
}