using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ItemAction
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public BaseParam BaseParam { get; set; }
        public QualityType QualityType { get; set; }
        public int Rate { get; set; }
        public int Limit { get; set; }
        public int FixedAmount { get; set; }
    }
}