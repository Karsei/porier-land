using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayForm
    {
        private Dictionary<string, bool> ffxivProcessNames = new Dictionary<string, bool>
        {
            { "ffxiv", true },
            { "ffxiv_dx11", true }
        };

        private Timer focusTimer;

        public virtual void RefreshTimers()
        {
            DisableTimers();

            focusTimer = new Timer
            {
                Interval = 1000
            };
            focusTimer.Tick += FocusTimer_Tick;
            focusTimer.Start();
        }

        public virtual void DisableTimers()
        {
            focusTimer?.Dispose();
            focusTimer = null;
        }

        private void FocusTimer_Tick(object sender, EventArgs e)
        {
            if (ActPlugin.Setting.IsHiding)
            {
                Visible = false;
            }
            else if (ActPlugin.Setting.IsGameMode)
            {
                string procName = Utility.GetForegroundProcessName();

                if (procName == Process.GetCurrentProcess().ProcessName)
                {
                    Visible = true;
                }
                else
                {
                    Visible = ffxivProcessNames.ContainsKey(procName);
                }
            }
            else
            {
                Visible = true;
            }

            backgroundForm.Visible = Visible;
        }
    }
}