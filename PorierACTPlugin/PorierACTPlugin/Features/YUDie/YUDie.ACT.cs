using Advanced_Combat_Tracker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PorierACTPlugin
{
    partial class YUDie
    {
        string charName;
        ConcurrentQueue<string> logLines = new ConcurrentQueue<string>();
        ConcurrentDictionary<string, Combatant> combatants = new ConcurrentDictionary<string, Combatant>();
        ConcurrentDictionary<string, bool> allies = new ConcurrentDictionary<string, bool>();
        List<Combatant> deadCombatants = new List<Combatant>();

        long prevPos;

        public void LoadLog(string logPath)
        {
            using (FileStream fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (sr.Peek() != -1)
                {
                    logLines.Enqueue(sr.ReadLine());
                }
            }
        }
        
        private void OFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (isImport) return;
            
            string incomingLog = string.Empty;

            using (FileStream fs = new FileStream(ActGlobals.oFormActMain.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] bytes = new byte[fs.Length - prevPos];
                fs.Seek(prevPos, SeekOrigin.Begin);
                prevPos += fs.Read(bytes, 0, bytes.Length);
                incomingLog = Encoding.UTF8.GetString(bytes);
            }

            string[] lines = incomingLog.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                logLines.Enqueue(line);
            }
        }
        
        private void processLine(string[] columns)
        {
            if (columns[0] == "02")
            {
                charName = columns[3];
                return;
            }

            string targetName = string.Empty;

            switch (columns[0])
            {
                case "21":
                case "22":
                    targetName = columns[7];
                    break;
                case "26":
                case "30":
                    targetName = columns[8];
                    break;
            }

            if (string.IsNullOrEmpty(targetName))
            {
                if (columns[0] != "25") return;

                Combatant combatant = null;
                combatants.TryRemove(columns[3], out combatant);
                if (combatant == null) return;

                combatant.Killed(ActGlobals.oFormActMain.ActiveZone?.ActiveEncounter?.Duration, columns[1]);
                deadCombatants.Add(combatant);

                yUDieHeader.SetDeadCombatants(deadCombatants, allies.Keys);
            }
            else
            {
                if (!combatants.ContainsKey(targetName))
                {
                    combatants.TryAdd(targetName, new Combatant(targetName, combatants));
                }

                Combatant combatant = null;
                combatants.TryGetValue(targetName, out combatant);
                if (combatant == null) return;
                
                switch (columns[0])
                {
                    case "21":
                    case "22":
                        if (columns[3] == targetName) return;
                        combatant.Update(new SkillInfo(
                            /* Caster Name */ allies.ContainsKey(columns[3]), columns[3],
                            /* Skill Name */ columns[5],
                            ActGlobals.oFormActMain.ActiveZone?.ActiveEncounter?.Duration,
                            /* Damage */ columns[9],
                            /* Target HP */ columns[24]));
                        break;
                    case "26":
                        combatant.Update(columns[2], columns[3]);
                        break;
                    case "30":
                        combatant.End(columns[2], columns[1]);
                        break;
                }
            }
        }
    }
}