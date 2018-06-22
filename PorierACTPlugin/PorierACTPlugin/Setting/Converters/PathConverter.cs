using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class PathConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class PathTypeEditor : UITypeEditor
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
                PathForm pathForm = new PathForm
                {
                    SelectedPath = (string)value
                };

                if (editorService.ShowDialog(pathForm) == DialogResult.OK)
                {
                    value = pathForm.SelectedPath;
                }

                pathForm.Dispose();
            }

            return value;
        }
    }

    public class PathForm : Form
    {
        public string SelectedPath;

        protected override void OnShown(EventArgs e)
        {
            Hide();

            TopMost = true;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                SelectedPath = SelectedPath
            };

            DialogResult = folderBrowserDialog.ShowDialog();

            SelectedPath = folderBrowserDialog.SelectedPath;

            Close();
        }
    }
}