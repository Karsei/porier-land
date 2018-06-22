using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FFXIVTranslator.CSV
{
    public class QuestRow : BaseRow
    {
        public string Name { get; set; }

        public QuestRow(string Key, string Name) : base(Key)
        {
            this.Name = Name.Trim('"');
        }

        public static IEnumerable<QuestRow> GetRows()
        {
            return File.ReadAllLines(CsvConfiguration.EXD_LOCATION + @"\quest.exh_ko.csv")
                .Skip(1)
                .Select(line => line.Substring(0, line.IndexOf('"', line.IndexOf('"') + 1) + 1))
                .Select(line => Regex.Split(line, CsvConfiguration.SPLIT_REGEX))
                .Select(row => new QuestRow(row.ElementAt(0), row.ElementAt(1)));
        }
    }
}