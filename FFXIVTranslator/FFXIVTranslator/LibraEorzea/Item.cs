using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Item
    {
        public long Key { get; set; }
        public long? Category { get; set; }
        public long? Uicategory { get; set; }
        public string UinameJa { get; set; }
        public string UinameEn { get; set; }
        public string UinameFr { get; set; }
        public string UinameDe { get; set; }
        public string HelpJa { get; set; }
        public string HelpEn { get; set; }
        public string HelpFr { get; set; }
        public string HelpDe { get; set; }
        public long? Level { get; set; }
        public long? EquipLevel { get; set; }
        public long? Rarity { get; set; }
        public string Hq { get; set; }
        public long? SpecialBonus { get; set; }
        public long? Series { get; set; }
        public long? Slot { get; set; }
        public long? Damage { get; set; }
        public long? DamageHq { get; set; }
        public long? MagicDamage { get; set; }
        public long? MagicDamageHq { get; set; }
        public long? Defense { get; set; }
        public long? DefenseHq { get; set; }
        public long? MagicDefense { get; set; }
        public long? MagicDefenseHq { get; set; }
        public long? ShieldRate { get; set; }
        public long? ShieldRateHq { get; set; }
        public long? ShieldBlockRate { get; set; }
        public long? ShieldBlockRateHq { get; set; }
        public double? AttackInterval { get; set; }
        public double? AttackIntervalHq { get; set; }
        public double? AutoAttack { get; set; }
        public double? AutoAttackHq { get; set; }
        public long? Price { get; set; }
        public long? PriceMin { get; set; }
        public long? MirageItem { get; set; }
        public string Icon { get; set; }
        public string IconHq { get; set; }
        public string Classjob { get; set; }
        public long? Salvage { get; set; }
        public long? Purify { get; set; }
        public long? SearchCategory { get; set; }
        public string MateriaProhibition { get; set; }
        public long? SpecialBonusArg { get; set; }
        public byte[] Data { get; set; }
        public string Legacy { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
        public long? SortId { get; set; }
    }
}
