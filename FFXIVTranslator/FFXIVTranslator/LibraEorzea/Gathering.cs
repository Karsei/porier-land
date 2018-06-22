using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Gathering
    {
        public long Key { get; set; }
        public long? Item { get; set; }
        public long? GatheringType { get; set; }
        public long? GatheringSubCategory { get; set; }
        public long? Level { get; set; }
        public long? LevelView { get; set; }
        public long? LevelDiff { get; set; }
        public string IsHidden { get; set; }
        public byte[] Data { get; set; }
        public long? GatheringNotebookList { get; set; }
        public long? GatheringItemNo { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
