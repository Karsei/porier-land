using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ItemENpc
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public ENpc ENpc { get; set; }
    }
}