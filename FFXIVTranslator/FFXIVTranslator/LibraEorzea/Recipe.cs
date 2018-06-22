using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Recipe
    {
        public long Key { get; set; }
        public long? CanAutoCraft { get; set; }
        public long? CanHq { get; set; }
        public long? CanMasterpiece { get; set; }
        public long? CraftItemId { get; set; }
        public long? CraftNum { get; set; }
        public long? CraftType { get; set; }
        public long? Level { get; set; }
        public long? LevelView { get; set; }
        public long? LevelDiff { get; set; }
        public long? Element { get; set; }
        public long? NeedCraftmanship { get; set; }
        public long? NeedControl { get; set; }
        public long? NeedAutoCraftmanship { get; set; }
        public long? NeedAutoControl { get; set; }
        public long? Number { get; set; }
        public long? NeedStatus { get; set; }
        public long? NeedEquipItem { get; set; }
        public long? Meister { get; set; }
        public long? NeedSecretRecipeBook { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
