using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ClassJobCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public string NameKo { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }

        public List<Item> Items { get; set; }
        public List<ClassJobClassJobCategory> ClassJobClassJobCategories { get; set; }
    }
}