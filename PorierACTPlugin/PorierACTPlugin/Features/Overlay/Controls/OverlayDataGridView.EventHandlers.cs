using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayDataGridView
    {
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            ClearSelection();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (setting.AntiAliasing)
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            }
            
            base.OnPaint(e);
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}