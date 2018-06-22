using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayHeader
    {
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }
    }
}