using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class ClassJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public string Label { get; set; }
        public string NameKo { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public string AbbreviationKo { get; set; }
        public string AbbreviationJa { get; set; }
        public string AbbreviationEn { get; set; }
        public string AbbreviationFr { get; set; }
        public string AbbreviationDe { get; set; }

        public bool IsJob { get; set; }

        public List<Recipe> Recipes { get; set; }
        
        public List<ItemClassJob> ItemClassJobs { get; set; }
        public List<ClassJobClassJobCategory> ClassJobClassJobCategories { get; set; }
        public List<Gathering> Gatherings { get; set; }
    }
}