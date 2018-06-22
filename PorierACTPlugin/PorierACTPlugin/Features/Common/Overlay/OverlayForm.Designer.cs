using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayForm
    {
        private void InitializeComponent()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            SetStyle(ControlStyles.ResizeRedraw, true);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            TransparencyKey = Color.FromArgb(1, 1, 1);

            containerPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(containerPanel);
            
            backgroundForm = new BackgroundForm();
            backgroundForm.GotFocus += BackgroundForm_GotFocus;
            backgroundForm.Show();
        }

        protected Panel containerPanel;
        protected BackgroundForm backgroundForm;
    }
}