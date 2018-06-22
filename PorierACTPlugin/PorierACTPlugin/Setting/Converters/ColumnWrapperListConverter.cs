using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class ColumnWrapperListConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";

            return "(" + Language.Resource.GetString("LIST") + ")";
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class ColumnWrapperListTypeEditor : UITypeEditor
    {
        IWindowsFormsEditorService editorService;

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
                ColumnWrapperListForm columnWrapperListForm = new ColumnWrapperListForm((List<ColumnWrapper>)value);

                if (editorService.ShowDialog(columnWrapperListForm) == DialogResult.OK)
                {
                    value = new List<ColumnWrapper>();

                    foreach (ColumnWrapper columnWrapper in columnWrapperListForm.ColumnWrappers)
                    {
                        ((List<ColumnWrapper>)value).Add(columnWrapper);
                    }
                }

                columnWrapperListForm.Dispose();
            }

            return value;
        }
    }

    public class ColumnWrapperListForm : Form
    {
        public List<ColumnWrapper> ColumnWrappers;

        public ColumnWrapperListForm(List<ColumnWrapper> columnWrappers) : base()
        {
            ColumnWrappers = columnWrappers;

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(500, 500);
            Text = Language.Resource.GetString("LIST");

            tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                RowCount = 1
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle
            {
                SizeType = SizeType.Percent,
                Width = 0.5f
            });
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle
            {
                SizeType = SizeType.Percent,
                Width = 0.5f
            });
            Controls.Add(tableLayoutPanel);

            listBox = new ListBox
            {
                DataSource = new BindingList<ColumnWrapper>(ColumnWrappers),
                DisplayMember = "Name",
                Dock = DockStyle.Fill
            };
            listBox.SelectedValueChanged += ListBox_SelectedValueChanged;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);

            propertyGrid = new PropertyGrid
            {
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(propertyGrid, 1, 0);

            okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
                Text = Language.Resource.GetString("APPLY_BUTTON")
            };
            Controls.Add(okButton);
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = listBox.SelectedItem;
        }

        private ListBox listBox;
        private PropertyGrid propertyGrid;
        private TableLayoutPanel tableLayoutPanel;
        private Button okButton;
    }
}