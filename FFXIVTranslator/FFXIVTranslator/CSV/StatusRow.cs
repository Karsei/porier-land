using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class StatusRow : BaseRow
    {
        public string Name { get; set; }

        public StatusRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<StatusRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\status.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new StatusRow(row.ElementAt(0), row.ElementAt(1)));
        }
    }
}