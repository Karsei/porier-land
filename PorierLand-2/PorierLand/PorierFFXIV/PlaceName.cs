﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class PlaceName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public PlaceName Region { get; set; }

        public string SGLKo { get; set; }
        public string SGLJa { get; set; }
        public string SGLEn { get; set; }
        public string SGLFr { get; set; }
        public string SGLDe { get; set; }

        public List<GatheringPoint> GatheringPoints { get; set; }
        public List<ENpcPlaceName> ENpcPlaceNames { get; set; }
    }
}