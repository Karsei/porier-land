using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ENpcShop
    {
        [Key]
        public long Key { get; set; }

        public ENpc ENpc { get; set; }
        public Shop Shop { get; set; }
    }
}