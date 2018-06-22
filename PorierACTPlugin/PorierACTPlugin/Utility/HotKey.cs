using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public class HotKey : NativeWindow, IDisposable
    {
        private static int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public HotKey(HotKeyWrapper hotKeyWrapper)
        {
            CreateHandle(new CreateParams());

            if (hotKeyWrapper != null) RegisterHotKey(Handle, 0, hotKeyWrapper.Modifiers, (uint)hotKeyWrapper.KeyCode);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                KeyPressed(this, null);
            }
        }

        public event EventHandler<EventArgs> KeyPressed;

        public void Dispose()
        {
            UnregisterHotKey(Handle, 0);

            DestroyHandle();
        }
    }
}