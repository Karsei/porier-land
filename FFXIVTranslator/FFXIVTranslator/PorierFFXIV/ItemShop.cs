using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ItemShop
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public Shop Shop { get; set; }
    }
}