using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ClassJobClassJobCategory
    {
        [Key]
        public long Key { get; set; }

        public ClassJob ClassJob { get; set; }
        public ClassJobCategory ClassJobCategory { get; set; }
    }
}