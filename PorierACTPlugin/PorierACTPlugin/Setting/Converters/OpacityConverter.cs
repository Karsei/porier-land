using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class OpacityTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

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
                OpacityControl opacityControl = new OpacityControl(editorService, (double)value);

                editorService.DropDownControl(opacityControl);

                value = opacityControl.TrackBar.Value / 100d;
            }

            return value;
        }
    }

    public class OpacityControl : Panel
    {
        public TrackBar TrackBar;

        private IWindowsFormsEditorService editorService;
        private Label label;

        public OpacityControl(IWindowsFormsEditorService editorService, double value) : base()
        {
            this.editorService = editorService;

            Height = 40;

            TrackBar = new TrackBar
            {
                Left = 10,
                Maximum = 100,
                Minimum = 1,
                TickFrequency = 1,
                TickStyle = TickStyle.None,
                Top = 10,
                Value = (int)(value * 100d)
            };
            TrackBar.ValueChanged += TrackBar_ValueChanged;
            TrackBar.MouseUp += TrackBar_MouseUp;
            Controls.Add(TrackBar);

            label = new Label
            {
                AutoSize = true,
                Left = TrackBar.Right,
                Text = TrackBar.Value.ToString() + "%",
                Top = 15
            };
            Controls.Add(label);
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            label.Text = TrackBar.Value.ToString() + "%";
        }

        private void TrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            editorService.CloseDropDown();
        }
    }
}