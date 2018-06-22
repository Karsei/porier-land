using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ItemClassJob
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public ClassJob ClassJob { get; set; }
    }
}