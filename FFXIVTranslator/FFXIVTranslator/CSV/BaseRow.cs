using System;

namespace FFXIVTranslator.CSV
{
    public class BaseRow
    {
        public long Key { get; set; }

        public BaseRow(string Key)
        {
            if (Int64.TryParse(Key, out long _Key))
            {
                this.Key = _Key;
            }
        }
    }
}