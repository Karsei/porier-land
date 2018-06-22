using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public class Combatant
    {
        public string Name { get { return name; } }

        private string name;
        private TimeSpan? killedDuration;
        private string killedTimestamp;
        private Queue<SkillInfo> lastSkills;
        private Dictionary<string, StatusEffect> statusEffects;

        private Timer removeTimer;
        private ConcurrentDictionary<string, Combatant> combatants;

        public Combatant(string name, ConcurrentDictionary<string, Combatant> combatants)
        {
            this.name = name;
            this.combatants = combatants;

            lastSkills = new Queue<SkillInfo>();
            statusEffects = new Dictionary<string, StatusEffect>();

            removeTimer = new Timer
            {
                Interval = 30000
            };
            removeTimer.Tick += RemoveTimer_Tick;
            removeTimer.Start();
        }

        private void RemoveTimer_Tick(object sender, EventArgs e)
        {
            Combatant combatant;
            combatants.TryRemove(name, out combatant);
            removeTimer.Dispose();
        }

        public void Update(SkillInfo skillInfo)
        {
            removeTimer.Stop();

            lastSkills.Enqueue(skillInfo);

            if (lastSkills.Count > 50) lastSkills.Dequeue();

            removeTimer.Start();
        }

        public void Update(string id, string name)
        {
            removeTimer.Stop();

            if (!statusEffects.ContainsKey(id))
            {
                statusEffects.Add(id, new StatusEffect(id, name));
            }
            
            removeTimer.Start();
        }

        public void End(string id, string timestamp)
        {
            removeTimer.Stop();

            timestamp = timestamp.Substring(0, timestamp.LastIndexOf('.'));

            DateTime dateTime = new DateTime();

            if (DateTime.TryParseExact(timestamp, "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out dateTime))
            {
                if (statusEffects.ContainsKey(id))
                {
                    statusEffects[id].LastEnded = dateTime;
                }
            }
            
            removeTimer.Start();
        }

        public void Killed(TimeSpan? killedDuration, string killedTimestamp)
        {
            removeTimer.Dispose();

            this.killedDuration = killedDuration;

            killedTimestamp = killedTimestamp.Substring(0, killedTimestamp.LastIndexOf('.'));
            this.killedTimestamp = killedTimestamp;

        }

        public override string ToString()
        {
            return name + (killedDuration != null ? " (" + ((TimeSpan)killedDuration).ToString("mm\\:ss") + ")" : "");
        }

        public string[] GetSkills(FilterType filter)
        {
            List<string> strs = new List<string>();
            List<SkillInfo> skillInfos = lastSkills.ToList();

            for (int i = skillInfos.Count - 1; i >= 0; i--)
            {
                switch (filter)
                {
                    case FilterType.All:
                        strs.Add(skillInfos[i].ToString());
                        break;
                    case FilterType.Allies:
                        if (skillInfos[i].FromAlly)
                        {
                            strs.Add(skillInfos[i].ToString());
                        }
                        break;
                    case FilterType.Enemies:
                        if (!skillInfos[i].FromAlly)
                        {
                            strs.Add(skillInfos[i].ToString());
                        }
                        break;
                }

                if (strs.Count == 10) return strs.ToArray();
            }
            
            return strs.ToArray();
        }

        public string[] GetStatusEffects()
        {
            List<string> strs = new List<string>();

            if (!string.IsNullOrEmpty(killedTimestamp))
            {
                DateTime dateTime = new DateTime();

                if (DateTime.TryParseExact(killedTimestamp, "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out dateTime))
                {
                    foreach (string key in statusEffects.Keys)
                    {
                        if (statusEffects[key].LastEnded == null || dateTime - statusEffects[key].LastEnded < TimeSpan.FromSeconds(5))
                        {
                            strs.Add(statusEffects[key].Name);
                        }
                    }
                }
            }

            return strs.ToArray();
        }
    }
}