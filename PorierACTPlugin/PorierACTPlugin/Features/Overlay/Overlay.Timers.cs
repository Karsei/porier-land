using Advanced_Combat_Tracker;
using System;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class Overlay
    {
        private Timer logTimer;
        private Timer inCombatTimer;

        public override void RefreshTimers()
        {
            base.RefreshTimers();

            logTimer = new Timer
            {
                Interval = 1000
            };
            logTimer.Tick += LogTimer_Tick;
            logTimer.Start();

            inCombatTimer = new Timer
            {
                Interval = 1000
            };
            inCombatTimer.Tick += InCombatTimer_Tick;
            inCombatTimer.Start();
        }
        
        public override void DisableTimers()
        {
            base.DisableTimers();

            logTimer?.Dispose();
            logTimer = null;

            inCombatTimer?.Dispose();
            inCombatTimer = null;
        }

        private void LogTimer_Tick(object sender, EventArgs e)
        {
            string logLine = string.Empty;

            while (logLines.TryDequeue(out logLine))
            {
                if (string.IsNullOrEmpty(logLine)) return;

                processLine(logLine.Split('|'));
            }
        }

        private void InCombatTimer_Tick(object sender, EventArgs e)
        {
            if (ActGlobals.oFormActMain.InCombat)
            {
                populateOverlay();
            }
        }
    }
}