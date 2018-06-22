using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public static class Utility
    {
        #region Get Foreground Process Name
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static string GetForegroundProcessName()
        {
            uint procId;
            GetWindowThreadProcessId(GetForegroundWindow(), out procId);
            return Process.GetProcessById((int)procId).ProcessName;
        }
        #endregion

        #region Hide Alt + Tab Preiew
        public const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        private static extern void SetLastError(int dwErrorCode);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        private static IntPtr SetWindowLongA(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;

            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                Int32 result32 = SetWindowLong(hWnd, nIndex, unchecked((int)dwNewLong.ToInt64()));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(result32);
            }
            else
            {
                result = SetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if (result == IntPtr.Zero && error != 0)
            {
                throw new Win32Exception(error);
            }

            return result;
        }

        public static void HideAltTabPreview(IntPtr hWnd)
        {
            SetWindowLongA(
                hWnd,
                GWL_EXSTYLE,
                (IntPtr)(GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_TOOLWINDOW));
        }
        #endregion

        #region Click Through
        private const int WS_EX_TRANSPARENT = 0x00000020;

        public static void SetClickThrough(IntPtr hWnd, bool enable)
        {
            SetWindowLongA(
                hWnd,
                GWL_EXSTYLE,
                enable ?
                    (IntPtr)(GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_TRANSPARENT) :
                    (IntPtr)(GetWindowLong(hWnd, GWL_EXSTYLE) & ~WS_EX_TRANSPARENT));
        }
        #endregion

        #region Screen Capture
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static Bitmap PrintWindow(IntPtr hWnd)
        {
            RECT rect;
            GetWindowRect(hWnd, out rect);

            Bitmap bitmap = new Bitmap(rect.Right - rect.Left, rect.Bottom - rect.Top, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            IntPtr hdcBitmap = graphics.GetHdc();

            PrintWindow(hWnd, hdcBitmap, 0);

            graphics.ReleaseHdc(hdcBitmap);
            graphics.Dispose();

            return bitmap;
        }
        #endregion

        #region Math
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static Point ScreenClamp(this PointWrapper p, Control c)
        {
            return ((Point)p).ScreenClamp(c);
        }

        public static Point ScreenClamp(this Point p, Control c)
        {
            Rectangle workingArea = Screen.GetWorkingArea(c);

            return new Point(p.X.Clamp(workingArea.Left, workingArea.Right), p.Y.Clamp(workingArea.Top, workingArea.Bottom));
        }

        public static Size ScreenClamp(this SizeWrapper s, Control c)
        {
            return ((Size)s).ScreenClamp(c);
        }

        public static Size ScreenClamp(this Size s, Control c)
        {
            Rectangle workingArea = Screen.GetWorkingArea(c);

            return new Size(s.Width.Clamp(0, workingArea.Width), s.Height.Clamp(0, workingArea.Height));
        }

        public static Point AddX(this PointWrapper p, int x)
        {
            return ((Point)p).AddX(x);
        }

        public static Point AddX(this Point p, int x)
        {
            return new Point(p.X + x, p.Y);
        }

        public static Point AddY(this PointWrapper p, int y)
        {
            return ((Point)p).AddY(y);
        }

        public static Point AddY(this Point p, int y)
        {
            return new Point(p.X, p.Y + y);
        }
        #endregion

        #region Round Corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        #endregion
    }
}