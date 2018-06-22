using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class JobColorsConverter : TypeConverter
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

    public class JobColorsTypeEditor : UITypeEditor
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
                JobColorsForm jobColorsForm = new JobColorsForm((List<JobColorWrapper>)value);

                if (editorService.ShowDialog(jobColorsForm) == DialogResult.OK)
                {
                    value = new List<JobColorWrapper>();

                    foreach (JobColorWrapper jobColorWrapper in jobColorsForm.JobColorWrappers)
                    {
                        ((List<JobColorWrapper>)value).Add(jobColorWrapper);
                    }
                }

                jobColorsForm.Dispose();
            }

            return value;
        }
    }

    public class JobColorsListBoxItem
    {
        public string Name { get; set; }
        public string GlobalizedName { get; set; }
    }

    public class JobColorsForm : Form
    {
        public List<JobColorWrapper> JobColorWrappers;

        public JobColorsForm(List<JobColorWrapper> jobColorWrappers) : base()
        {
            JobColorWrappers = jobColorWrappers;

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
                DisplayMember = "GlobalizedName",
                Dock = DockStyle.Fill
            };
            List<JobColorsListBoxItem> listBoxItems = new List<JobColorsListBoxItem>();
            foreach (JobColorWrapper jobColorWrapper in JobColorWrappers)
            {
                listBoxItems.Add(new JobColorsListBoxItem
                {
                    Name = jobColorWrapper.Name,
                    GlobalizedName = Language.Resource.GetString(jobColorWrapper.Name)
                });
            }
            listBox.DataSource = new BindingList<JobColorsListBoxItem>(listBoxItems);
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
            JobColorWrapper selectedWrapper = JobColorWrappers.FirstOrDefault(x => x.Name == ((JobColorsListBoxItem)listBox.SelectedItem).Name);

            if (selectedWrapper != null)
            {
                propertyGrid.SelectedObject = selectedWrapper;
            }
        }

        private ListBox listBox;
        private PropertyGrid propertyGrid;
        private TableLayoutPanel tableLayoutPanel;
        private Button okButton;
    }
}