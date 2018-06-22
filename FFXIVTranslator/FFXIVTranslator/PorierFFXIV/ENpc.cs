using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ENpc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public string SGLKo { get; set; }
        public string SGLJa { get; set; }
        public string SGLEn { get; set; }
        public string SGLFr { get; set; }
        public string SGLDe { get; set; }

        public bool HasShop { get; set; }
        public bool HasConditionShop { get; set; }
        public List<ENpcShop> ENpcShops { get; set; }
        
        public List<ENpcPlaceName> ENpcPlaceNames { get; set; }

        public byte[] Data { get; set; }

        public string Path { get; set; }

        public List<ItemENpc> ItemENpcs { get; set; }
    }
}