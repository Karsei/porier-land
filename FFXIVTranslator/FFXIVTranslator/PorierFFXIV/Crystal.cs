﻿using System.ComponentModel.DataAnnotations;

namespace FFXIVTranslator.PorierFFXIV
{
    public class Crystal
    {
        [Key]
        public long Key { get; set; }

        public Item Item { get; set; }
        public Recipe Recipe { get; set; }
        public long Amount { get; set; }
    }
}