using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class HeaderPanel
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = parentForm.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (dragging)
            {
                parentForm.Location = Point.Add(
                    dragFormPoint,
                    new Size(Point.Subtract(
                        Cursor.Position,
                        new Size(dragCursorPoint))));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            dragging = false;
        }
    }
}