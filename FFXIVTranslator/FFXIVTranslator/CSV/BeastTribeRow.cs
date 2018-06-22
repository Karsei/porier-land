using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class BeastTribeRow : BaseRow
    {
        public string Name { get; set; }

        public BeastTribeRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<BeastTribeRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\beasttribe.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new BeastTribeRow(row.ElementAt(0), row.ElementAt(9)));
        }
    }
}