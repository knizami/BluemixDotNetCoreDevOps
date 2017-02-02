namespace CloudantDotNet.Models
{
    public class Account : CloudantDoc
    {
        public string acctid { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public decimal balance { get; set; }
        public string type { get; set; }
    }
}