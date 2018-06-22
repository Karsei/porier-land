using System;

namespace PorierACTPlugin
{
    public class SkillInfo
    {
        public bool FromAlly { get; set; }
        public string CasterName { get; set; }
        public string SkillName { get; set; }
        public TimeSpan? DurationTimestamp { get; set; }
        public string Damage { get; set; }
        public string TargetHP { get; set; }

        public SkillInfo(bool fromAlly, string casterName, string skillName, TimeSpan? durationTimestamp, string damageHex, string targetHP)
        {
            FromAlly = fromAlly;
            CasterName = casterName;
            SkillName = skillName;
            DurationTimestamp = durationTimestamp;

            long damage = -1;
            try
            {
                damage = Convert.ToInt64(damageHex, 16);
                damage &= 0x7FFFF;
            }
            catch { }
            Damage = damage.ToString();

            TargetHP = targetHP;
        }

        public override string ToString()
        {
            return (DurationTimestamp != null ? ((TimeSpan)DurationTimestamp).ToString("mm\\:ss") : "..:..")
                + " " + SkillName + " (" + CasterName + ", " + Damage + ") -> " + TargetHP + "HP";
        }
    }
}