using System;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayForm
    {
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        
        private Rectangle leftArea { get { return new Rectangle(0, 0, boundary, ClientSize.Height); } }
        private Rectangle rightArea { get { return new Rectangle(ClientSize.Width - boundary, 0, boundary, ClientSize.Height); } }
        private Rectangle topArea { get { return new Rectangle(0, 0, ClientSize.Width, boundary); } }
        private Rectangle topLeftArea { get { return new Rectangle(0, 0, boundary, boundary); } }
        private Rectangle topRightArea { get { return new Rectangle(ClientSize.Width - boundary, 0, boundary, boundary); } }
        private Rectangle bottomArea { get { return new Rectangle(0, ClientSize.Height - boundary, ClientSize.Width, boundary); } }
        private Rectangle bottomLeftArea { get { return new Rectangle(0, ClientSize.Height - boundary, boundary, boundary); } }
        private Rectangle bottomRightArea { get { return new Rectangle(ClientSize.Width - boundary, ClientSize.Height - boundary, boundary, boundary); } }
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (autoSize == null) return;

            if (ActPlugin.Setting.IsGameMode) return;
            if (autoSize.EnableAutoSize) return;

            if (m.Msg == 0x84)
            {
                Point cursor = PointToClient(Cursor.Position);

                if (topLeftArea.Contains(cursor)) m.Result = (IntPtr)HTTOPLEFT;
                else if (topRightArea.Contains(cursor)) m.Result = (IntPtr)HTTOPRIGHT;
                else if (bottomLeftArea.Contains(cursor)) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (bottomRightArea.Contains(cursor)) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (leftArea.Contains(cursor)) m.Result = (IntPtr)HTLEFT;
                else if (rightArea.Contains(cursor)) m.Result = (IntPtr)HTRIGHT;
                else if (topArea.Contains(cursor)) m.Result = (IntPtr)HTTOP;
                else if (bottomArea.Contains(cursor)) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            backgroundForm.Size = ClientSize;

            if (roundCorner != null)
            {
                Region = Region.FromHrgn(Utility.CreateRoundRectRgn(0, 0, Width, Height, roundCorner.Width, roundCorner.Height));
                backgroundForm.Region = Region.FromHrgn(Utility.CreateRoundRectRgn(0, 0, backgroundForm.Width, backgroundForm.Height, roundCorner.Width, roundCorner.Height));
            }

            redrawContainingPanel();
            
            if (autoSize != null && !autoSize.EnableAutoSize)
            {
                sizeWrapper.SetSize(Size);
            }
        }

        private void redrawContainingPanel()
        {
            if (roundCorner != null)
            {
                containerPanel.Region = Region.FromHrgn(Utility.CreateRoundRectRgn(topLeftArea.Width, topLeftArea.Height, bottomRightArea.Left, bottomRightArea.Top, roundCorner.Width, roundCorner.Height));
            }

            Invalidate();
        }
    }
}