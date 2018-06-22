using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ENpcPlaceName
    {
        [Key]
        public long Key { get; set; }

        public ENpc ENpc { get; set; }
        public PlaceName PlaceName { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}