using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class InstanceContent
    {
        public long Key { get; set; }
        public long? Type { get; set; }
        public long? Sortkey { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public string DescriptionJa { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionFr { get; set; }
        public string DescriptionDe { get; set; }
        public long? LevelMin { get; set; }
        public long? LevelMax { get; set; }
        public long? Time { get; set; }
        public string Halfway { get; set; }
        public long? RandomContentType { get; set; }
        public string Alliance { get; set; }
        public long? FinderPartyCondition { get; set; }
        public long? PartyMemberCount { get; set; }
        public long? TankCount { get; set; }
        public long? HealerCount { get; set; }
        public long? AttackerCount { get; set; }
        public long? RangeCount { get; set; }
        public string DifferentiateDps { get; set; }
        public long? PartyCount { get; set; }
        public string FreeRole { get; set; }
        public long? ItemLevel { get; set; }
        public long? ItemLevelMax { get; set; }
        public long? Colosseum { get; set; }
        public long? Area { get; set; }
        public long? ForceCount { get; set; }
        public long? ExVersion { get; set; }
        public string EnableUsingItem { get; set; }
        public string SmallParty { get; set; }
        public string EnableLootMode { get; set; }
        public string EnableItemLimit { get; set; }
        public long? EntryPartyMemberNum { get; set; }
        public string RateMatch { get; set; }
        public string RateChange { get; set; }
        public long? ContentType { get; set; }
        public long? ContentSortKey { get; set; }
        public long? RaidFinderJobMatching { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
        public string IsKoeruUsually { get; set; }
        public string IsKoeruAnnihilation { get; set; }
        public string IsFeast { get; set; }
        public string ContentsJa { get; set; }
        public string ContentsEn { get; set; }
        public string ContentsFr { get; set; }
        public string ContentsDe { get; set; }
        public long? AcceptClassJobCategory { get; set; }
        public string IndexJa { get; set; }
        public string IndexEn { get; set; }
        public string IndexFr { get; set; }
        public string IndexDe { get; set; }
    }
}
