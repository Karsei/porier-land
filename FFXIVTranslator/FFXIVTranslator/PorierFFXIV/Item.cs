using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public enum QualityType
    {
        NQ,
        HQ
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public ItemUICategory UICategory { get; set; }
        public string UINameKo { get; set; }
        public string UINameJa { get; set; }
        public string UINameEn { get; set; }
        public string UINameFr { get; set; }
        public string UINameDe { get; set; }
        public string HelpKo { get; set; }
        public string HelpJa { get; set; }
        public string HelpEn { get; set; }
        public string HelpFr { get; set; }
        public string HelpDe { get; set; }

        public long? Level { get; set; }
        public long? EquipLevel { get; set; }
        public long? Rarity { get; set; }
        public bool HQ { get; set; }
        public long? Damage { get; set; }
        public long? DamageHQ { get; set; }
        public long? MagicDamage { get; set; }
        public long? MagicDamageHQ { get; set; }
        public long? Defense { get; set; }
        public long? DefenseHQ { get; set; }
        public long? MagicDefense { get; set; }
        public long? MagicDefenseHQ { get; set; }
        public long? ShieldRate { get; set; }
        public long? ShieldRateHQ { get; set; }
        public long? ShieldBlockRate { get; set; }
        public long? ShieldBlockRateHQ { get; set; }
        public double? AttackInterval { get; set; }
        public double? AttackIntervalHQ { get; set; }
        public double? AutoAttack { get; set; }
        public double? AutoAttackHQ { get; set; }
        public List<ItemBonus> ItemBonuses { get; set; }
        public List<ItemAction> ItemActions { get; set; }
        public int MateriaSocket { get; set; }

        public List<ItemENpc> ItemENpcs { get; set; }
        public List<ItemShop> ItemShops { get; set; }
        public long? Price { get; set; }
        
        public List<Recipe> CraftRecipes { get; set; }
        public List<Recipe> NeedEquipRecipes { get; set; }
        public List<Crystal> Crystals { get; set; }
        public List<RecipeItem> RecipeItems { get; set; }

        public List<Gathering> Gatherings { get; set; }

        public List<ItemClassJob> ItemClassJobs { get; set; }
        public ClassJobCategory ClassJobCategory { get; set; }

        public byte[] Data { get; set; }
        public string Path { get; set; }

        public string IconPath { get; set; }
    }
}