using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class ShopRow : BaseRow
    {
        public string Name { get; set; }

        public ShopRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<ShopRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\shop.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new ShopRow(row.ElementAt(0), row.ElementAt(49)));
        }
    }
}