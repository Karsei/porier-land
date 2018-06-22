using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class JournalGenre
    {
        public long Key { get; set; }
        public long? Category { get; set; }
        public string Label { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
    }
}
