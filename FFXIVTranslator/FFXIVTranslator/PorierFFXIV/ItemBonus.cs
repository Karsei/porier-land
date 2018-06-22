using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ItemBonus
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public BaseParam BaseParam { get; set; }
        public QualityType QualityType { get; set; }
        public long Amount { get; set; }
    }
}