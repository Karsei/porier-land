using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public bool CanAutoCraft { get; set; }
        public bool CanHQ { get; set; }
        public bool CanMasterpiece { get; set; }

        public Item CraftItem { get; set; }
        public long? CraftNum { get; set; }
        public ClassJob CraftJob { get; set; }

        public long? Level { get; set; }
        public long? LevelView { get; set; }
        public long? LevelDiff { get; set; }
        public RecipeElement Element { get; set; }
        public long? NeedCraftmanship { get; set; }
        public long? NeedControl { get; set; }
        public long? NeedAutoCraftmanship { get; set; }
        public long? NeedAutoControl { get; set; }

        public Status NeedStatus { get; set; }
        public Item NeedEquipItem { get; set; }
        public SecretRecipeBook NeedSecretRecipeBook { get; set; }

        public byte[] Data { get; set; }
        public List<Crystal> Crystals { get; set; }
        public List<RecipeItem> RecipeItems { get; set; }
        public long? QualityMax { get; set; }
        public long? WorkMax { get; set; }

        public string Path { get; set; }
    }
}