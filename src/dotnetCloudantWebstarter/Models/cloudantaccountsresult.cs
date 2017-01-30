namespace CloudantDotNet.Models
{
    public class CloudantAccountsResult
    {
        public string total_rows { get; set; }
        public CloudantAcouuntsResultsRow[] rows { get; set; }
    }


    public class CloudantAcouuntsResultsRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        Account doc { get; set; }

    }
}