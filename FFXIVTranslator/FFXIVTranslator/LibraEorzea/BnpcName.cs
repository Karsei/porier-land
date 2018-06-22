using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class BnpcName
    {
        public long Key { get; set; }
        public string SglJa { get; set; }
        public string SglEn { get; set; }
        public string SglFr { get; set; }
        public string SglDe { get; set; }
        public string Area { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
