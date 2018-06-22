using Advanced_Combat_Tracker;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class Overlay
    {
        private HotKey captureHotKey;
        private HotKey endCombatHotKey;

        public void RefreshHotKeys()
        {
            DisableHotKeys();
            
            captureHotKey = new HotKey(ActPlugin.Setting.OverlaySetting.CaptureHotKey);
            captureHotKey.KeyPressed += CaptureHotKey_KeyPressed;

            endCombatHotKey = new HotKey(ActPlugin.Setting.OverlaySetting.EndCombatHotKey);
            endCombatHotKey.KeyPressed += EndCombatHotKey_KeyPressed;
        }

        public void DisableHotKeys()
        {
            captureHotKey?.Dispose();
            captureHotKey = null;

            endCombatHotKey?.Dispose();
            endCombatHotKey = null;
        }
        
        private void CaptureHotKey_KeyPressed(object sender, EventArgs e)
        {
            bool originalDpsNameHidden = ActPlugin.Setting.OverlaySetting.DpsTable.IsNameHidden;
            bool originalHpsNameHidden = ActPlugin.Setting.OverlaySetting.HpsTable.IsNameHidden;

            if (ActPlugin.Setting.OverlaySetting.HideNamesWhenCapturing)
            {
                if (!originalDpsNameHidden)
                {
                    DpsDataGridView.ToggleHideNames();
                }

                if (!originalHpsNameHidden)
                {
                    HpsDataGridView.ToggleHideNames();
                }
            }

            try
            {
                Bitmap bitmap = Utility.PrintWindow(Handle);
                bitmap.Save(
                    Path.Combine(
                        ActPlugin.Setting.OverlaySetting.CaptureSavePath,
                        DateTime.UtcNow.ToString("yyyyMMdd_HHmmss_") + currentEncounterData.Title + ".png"),
                    ImageFormat.Png);

                if (ActPlugin.Setting.OverlaySetting.PutCaptureInClipboard)
                {
                    Clipboard.SetImage(bitmap);
                }

                ActPlugin.Overlay.Visible = false;

                Thread.Sleep(100);

                ActPlugin.Overlay.Visible = true;
            }
            finally
            {
                if (ActPlugin.Setting.OverlaySetting.HideNamesWhenCapturing)
                {
                    if (!originalDpsNameHidden)
                    {
                        DpsDataGridView.ToggleHideNames();
                    }

                    if (!originalHpsNameHidden)
                    {
                        HpsDataGridView.ToggleHideNames();
                    }
                }
            }
        }

        private void EndCombatHotKey_KeyPressed(object sender, EventArgs e)
        {
            ActGlobals.oFormActMain.EndCombat(true);
        }
    }
}