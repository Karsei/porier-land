using Advanced_Combat_Tracker;
using System;
using System.Drawing;

namespace PorierACTPlugin
{
    public class OverlayDataGridViewItem
    {
        public string Name { get; set; }
        public string HiddenName { get; set; }
        public string Job { get; set; }
        public Bitmap JobIcon { get; set; }

        public double EncDPS { get; set; }
        public double PetDPS { get; set; }
        public string TotalDPS { get; set; }

        public double EncHPS { get; set; }
        public double PetHPS { get; set; }
        public string TotalHPS { get; set; }

        public long TotalAlliesDamage { get; set; }
        public long Damage { get; set; }
        public long PetDamage { get; set; }
        public string TotalDamage { get; set; }
        public string DamagePercent { get; set; }

        public long TotalAlliesHealed { get; set; }
        public long Healed { get; set; }
        public long PetHealed { get; set; }
        public string TotalHealed { get; set; }
        public string HealedPercent { get; set; }

        public long OverHeal { get; set; }
        public long PetOverHeal { get; set; }
        public string TotalOverHeal { get; set; }
        public string OverHealPercent { get; set; }

        public int Hits { get; set; }
        public int PetHits { get; set; }

        public int DirectHits { get; set; }
        public int PetDirectHits { get; set; }
        public string DirectHitPercent { get; set; }

        public int CritHits { get; set; }
        public int PetCritHits { get; set; }
        public string CritPercent { get; set; }

        public int Heals { get; set; }
        public int PetHeals { get; set; }

        public int CritHeals { get; set; }
        public int PetCritHeals { get; set; }
        public string CritHealPercent { get; set; }

        public long MaxHit { get; set; }
        public long PetMaxHit { get; set; }
        public long TotalMaxHit { get; set; }
        public string MaxHitString { get; set; }
        public string PetMaxHitString { get; set; }
        public string TotalMaxHitString { get; set; }

        public long MaxHeal { get; set; }
        public long PetMaxHeal { get; set; }
        public long TotalMaxHeal { get; set; }
        public string MaxHealString { get; set; }
        public string PetMaxHealString { get; set; }
        public string TotalMaxHealString { get; set; }

        public int Swings { get; set; }
        public int PetSwings { get; set; }
        public string TotalSwings { get; set; }

        public int Misses { get; set; }
        public int PetMisses { get; set; }
        public string TotalMisses { get; set; }

        public int Deaths { get; set; }

        public OverlayDataGridViewItem Master { get; set; }

        public void RefreshPrints()
        {
            TotalDPS = (EncDPS + PetDPS).ToString("N2");
            TotalHPS = (EncHPS + PetHPS).ToString("N2");
            TotalDamage = (Damage + PetDamage).ToString("0,0");

            if ((Damage + PetDamage) > 0 && TotalAlliesDamage > 0)
            {
                DamagePercent = ((int)((Damage + PetDamage) * 100d / TotalAlliesDamage)).ToString() + "%";
            }
            else
            {
                DamagePercent = "0%";
            }

            TotalHealed = (Healed + PetHealed).ToString("0,0");

            if ((Healed + PetHealed) > 0 && TotalAlliesHealed > 0)
            {
                HealedPercent = ((int)((Healed + PetHealed) * 100d / TotalAlliesHealed)).ToString() + "%";
            }
            else
            {
                HealedPercent = "0%";
            }

            TotalOverHeal = (OverHeal + PetOverHeal).ToString("0,0");

            if ((OverHeal + PetOverHeal) > 0 && (Healed + PetHealed) > 0)
            {
                OverHealPercent = ((int)((OverHeal + PetOverHeal) * 100d / (Healed + PetHealed))).ToString() + "%";
            }
            else
            {
                OverHealPercent = "0%";
            }

            if ((DirectHits + PetDirectHits) > 0 && (Hits + PetHits) > 0)
            {
                DirectHitPercent = ((DirectHits + PetDirectHits) * 100f / (Hits + PetHits)).ToString("N1") + "%";
            }
            else
            {
                DirectHitPercent = "0%";
            }

            if ((CritHits + PetCritHits) > 0 && (Hits + PetHits) > 0)
            {
                CritPercent = ((CritHits + PetCritHits) * 100f / (Hits + PetHits)).ToString("N1") + "%";
            }
            else
            {
                CritPercent = "0%";
            }

            if ((CritHeals + PetCritHeals) > 0 && (Heals + PetHeals) > 0)
            {
                CritHealPercent = ((CritHeals + PetCritHeals) * 100f / (Heals + PetHeals)).ToString("N1") + "%";
            }
            else
            {
                CritHealPercent = "0%";
            }

            TotalMaxHit = Math.Max(MaxHit, PetMaxHit);
            TotalMaxHitString = (MaxHit >= PetMaxHit) ? MaxHitString : PetMaxHitString;

            TotalMaxHeal = Math.Max(MaxHeal, PetMaxHeal);
            TotalMaxHealString = (MaxHeal >= PetMaxHeal) ? MaxHealString : PetMaxHealString;

            TotalSwings = (Swings + PetSwings).ToString();
            TotalMisses = (Misses + PetMisses).ToString();
    }

        public static implicit operator OverlayDataGridViewItem(CombatantData combatant)
        {
            OverlayDataGridViewItem item = new OverlayDataGridViewItem
            {
                Name = combatant.Name,
                HiddenName = (combatant.Name == "YOU" || combatant.Name == "Limit Break") ? combatant.Name : "",
                Job = combatant.GetColumnByName("Job"),
                JobIcon = null,
                EncDPS = combatant.EncDPS,
                PetDPS = 0,
                EncHPS = combatant.EncHPS,
                PetHPS = 0,
                Damage = combatant.Damage,
                PetDamage = 0,
                Healed = combatant.Healed,
                PetHealed = 0,
                OverHeal = 0,
                PetOverHeal = 0,
                Hits = combatant.Hits,
                PetHits = 0,
                DirectHits = 0,
                PetDirectHits = 0,
                CritHits = combatant.CritHits,
                PetCritHits = 0,
                Heals = combatant.Heals,
                PetHeals = 0,
                CritHeals = combatant.CritHeals,
                PetCritHeals = 0,
                MaxHit = 0,
                PetMaxHit = 0,
                MaxHitString = "-",
                PetMaxHitString = "-",
                MaxHeal = 0,
                PetMaxHeal = 0,
                MaxHealString = "-",
                PetMaxHealString = "-",
                Swings = combatant.Swings,
                PetSwings = 0,
                Misses = combatant.Misses,
                PetMisses = 0,
                Deaths = combatant.Deaths,
                Master = null
            };

            if (string.IsNullOrEmpty(item.Job))
            {
                if (item.Name == "Limit Break")
                {
                    item.Job = "LIMIT";
                }
                else if (item.Name.Contains("("))
                {
                    item.Job = "PET";
                }
            }
            else
            {
                item.Job = item.Job.ToUpper();
            }
            
            item.JobIcon = (Bitmap)Image.Resource.ResourceManager.GetObject(item.Job);
            if (item.JobIcon == null) item.JobIcon = Image.Resource.PET;

            long overHeal = 0;
            if (combatant.AllOut.ContainsKey("All") && long.TryParse(combatant.AllOut["All"].GetColumnByName("OverHeal").Replace(",", ""), out overHeal))
            {
                item.OverHeal = overHeal;
            }

            int directHits = 0;
            if (int.TryParse(combatant.GetColumnByName("DirectHitCount").Replace(",", ""), out directHits))
            {
                item.DirectHits = directHits;
            }

            long maxHit = 0;
            if (long.TryParse(combatant.GetMaxHit(false).Replace(",", ""), out maxHit))
            {
                item.MaxHit = maxHit;
                item.MaxHitString = combatant.GetMaxHit(true);
            }

            long maxHeal = 0;
            if (long.TryParse(combatant.GetMaxHeal(false, true).Replace(",", ""), out maxHeal))
            {
                item.MaxHeal = maxHeal;
                item.MaxHealString = combatant.GetMaxHeal(true, true);
            }

            item.RefreshPrints();

            return item;
        }
    }
}