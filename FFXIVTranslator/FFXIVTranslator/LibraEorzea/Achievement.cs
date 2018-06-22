using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Achievement
    {
        public long Key { get; set; }
        public long? Category { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public string HelpJa { get; set; }
        public string HelpEn { get; set; }
        public string HelpFr { get; set; }
        public string HelpDe { get; set; }
        public long? Point { get; set; }
        public long? Item { get; set; }
        public long? Icon { get; set; }
        public long? Title { get; set; }
        public long? Priority { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
