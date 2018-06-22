using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class YUDie
    {
        private Timer logTimer;
        private Timer combatantTimer;

        public override void RefreshTimers()
        {
            base.RefreshTimers();

            logTimer = new Timer
            {
                Interval = 1000
            };
            logTimer.Tick += LogTimer_Tick;
            logTimer.Start();

            combatantTimer = new Timer
            {
                Interval = 1000
            };
            combatantTimer.Tick += CombatantTimer_Tick;
            combatantTimer.Start();
        }
        
        public override void DisableTimers()
        {
            base.DisableTimers();

            logTimer?.Dispose();
            logTimer = null;

            combatantTimer?.Dispose();
            combatantTimer = null;
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

        private void CombatantTimer_Tick(object sender, EventArgs e)
        {
            if (!ActGlobals.oFormActMain.InCombat) return;
            if (ActGlobals.oFormActMain.ActiveZone == null || ActGlobals.oFormActMain.ActiveZone.ActiveEncounter == null) return;

            List<CombatantData> allyList = ActGlobals.oFormActMain.ActiveZone.ActiveEncounter.GetAllies();

            foreach (CombatantData ally in allyList)
            {
                string name = (!string.IsNullOrEmpty(charName) && ally.Name == "YOU") ? charName : ally.Name;
                
                if (!allies.ContainsKey(name))
                {
                    allies.TryAdd(name, true);
                }
            }
        }
    }
}