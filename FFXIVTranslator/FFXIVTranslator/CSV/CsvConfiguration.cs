namespace FFXIVTranslator.CSV
{
    public static class CsvConfiguration
    {
        public static string EXD_LOCATION { get; set; }
        public static string SPLIT_REGEX = ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";
    }
}