using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class BeastTribe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public string SGLKo { get; set; }
        public string SGLJa { get; set; }
        public string SGLEn { get; set; }
        public string SGLFr { get; set; }
        public string SGLDe { get; set; }

        public List<Shop> Shops { get; set; }
    }
}