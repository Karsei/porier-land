using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class ClassJobRow : BaseRow
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public ClassJobRow(string Key, string Name, string Abbreviation) : base(Key)
        {
            this.Name = Name.Trim('"');
            this.Abbreviation = Abbreviation.Trim('"');
        }

        public static IEnumerable<ClassJobRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\classjob.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new ClassJobRow(row.ElementAt(0), row.ElementAt(1), row.ElementAt(3)));
        }
    }
}