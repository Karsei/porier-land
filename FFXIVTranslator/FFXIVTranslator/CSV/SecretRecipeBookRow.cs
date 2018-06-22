using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class SecretRecipeBookRow : BaseRow
    {
        public string Name { get; set; }

        public SecretRecipeBookRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<SecretRecipeBookRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\secretrecipebook.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new SecretRecipeBookRow(row.ElementAt(0), row.ElementAt(2)));
        }
    }
}