using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFXIVTranslator.PorierFFXIV
{
    public class SecretRecipeBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Key { get; set; }

        public string TextKo { get; set; }
        public string TextJa { get; set; }
        public string TextEn { get; set; }
        public string TextFr { get; set; }
        public string TextDe { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}