using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Quest
    {
        public long Key { get; set; }
        public long? Genre { get; set; }
        public long? Area { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public long? CompanyPointType { get; set; }
        public long? CompanyPointNum { get; set; }
        public long? Gil { get; set; }
        public long? Client { get; set; }
        public long? ExpBonus { get; set; }
        public long? Header { get; set; }
        public long? ClassLevel { get; set; }
        public long? ClassLevel2 { get; set; }
        public long? ClassJob { get; set; }
        public long? ClassJob2 { get; set; }
        public long? QuestLevelOffset { get; set; }
        public long? Sort { get; set; }
        public long? BeastTribe { get; set; }
        public long? BeastReputationValueNum { get; set; }
        public long? WebType { get; set; }
        public long? AllaganTomestoneCondition { get; set; }
        public long? ClassLevelUpperLimit { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
