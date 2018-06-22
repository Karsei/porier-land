using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class GatheringPoint
    {
        [Key]
        public long Key { get; set; }

        public Gathering Gathering { get; set; }

        public PlaceName PlaceName { get; set; }
        public long? Level { get; set; }

        public GatheringPoint Parent { get; set; }
        public List<GatheringPoint> Children { get; set; }
    }
}