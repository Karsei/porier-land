﻿using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Shop
    {
        public long Key { get; set; }
        public string NameJa { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string NameDe { get; set; }
        public long? BeastTribe { get; set; }
        public long? Quest { get; set; }
    }
}