using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class ClassJob
    {
        public long Key { get; set; }
        public string Label { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public string AbbreviationJa { get; set; }
        public string AbbreviationEn { get; set; }
        public string AbbreviationFr { get; set; }
        public string AbbreviationDe { get; set; }
        public long? IsJob { get; set; }
        public long? Uipriority { get; set; }
        public string Filebase { get; set; }
    }
}
