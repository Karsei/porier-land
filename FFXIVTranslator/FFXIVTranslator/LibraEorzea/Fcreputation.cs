using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Fcreputation
    {
        public long Key { get; set; }
        public long? Color { get; set; }
        public long? CurrentPoint { get; set; }
        public long? NextPoint { get; set; }
        public string TextJa { get; set; }
        public string TextEn { get; set; }
        public string TextFr { get; set; }
        public string TextDe { get; set; }
        public byte[] Data { get; set; }
    }
}
