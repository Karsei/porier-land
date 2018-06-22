using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class ItemRow : BaseRow
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }

        public ItemRow(string Key, string Name, string Description, string ItemNumber) : base(Key)
        {
            this.Name = Name.Trim('"');
            this.Description = Description.Trim('"');
            this.ItemNumber = ItemNumber;
        }

        public static IEnumerable<ItemRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\item.exh_ko.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new ItemRow(row.ElementAt(0), row.ElementAt(1), row.ElementAt(9), row.ElementAt(11)));
        }

        public static IEnumerable<ItemRow> GetGlobalRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\item.exh_en.csv")
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Skip(1)
                .Select(row => new ItemRow(row.ElementAt(0), row.ElementAt(1), row.ElementAt(9), row.ElementAt(11)));
        }
    }
}