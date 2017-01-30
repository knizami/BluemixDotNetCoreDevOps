namespace CloudantDotNet.Models
{
    public class AccountResult
    {
        public string status { get; set; }
        public int totalaccounts { get; set; }
        public Account[] accounts { get; set; }
    }


}