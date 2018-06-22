using System;
using System.Collections.Generic;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class Colosseum
    {
        public long Key { get; set; }
        public long? WinPoint { get; set; }
        public long? WinToken { get; set; }
        public long? LosePoint { get; set; }
        public long? LoseToken { get; set; }
    }
}
