using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class ClassJobCategoryRow : BaseRow
    {
        public string Name { get; set; }

        public ClassJobCategoryRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<ClassJobCategoryRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\classjobcategory.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new ClassJobCategoryRow(row.ElementAt(0), row.ElementAt(1)));
        }
    }
}