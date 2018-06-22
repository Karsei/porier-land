using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class GatheringSubCategoryRow : BaseRow
    {
        public string Name { get; set; }

        public GatheringSubCategoryRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<GatheringSubCategoryRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\gatheringsubcategory.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new GatheringSubCategoryRow(row.ElementAt(0), row.ElementAt(6)));
        }
    }
}