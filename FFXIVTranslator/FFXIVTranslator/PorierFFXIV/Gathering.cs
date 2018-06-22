using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class Gathering
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public Item Item { get; set; }
        public GatheringType GatheringType { get; set; }
        public GatheringSubCategory GatheringSubCategory { get; set; }
        public ClassJob GatheringJob { get; set; }

        public long? Level { get; set; }
        public long? LevelView { get; set; }
        public long? LevelDiff { get; set; }
        public bool IsHidden { get; set; }

        public byte[] Data { get; set; }
        public List<GatheringPoint> GatheringPoints { get; set; }

        public string Path { get; set; }
    }
}