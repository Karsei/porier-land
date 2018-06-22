using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class RecipeElementRow : BaseRow
    {
        public string Name { get; set; }

        public RecipeElementRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<RecipeElementRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\recipeelement.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new RecipeElementRow(row.ElementAt(0), row.ElementAt(1)));
        }
    }
}