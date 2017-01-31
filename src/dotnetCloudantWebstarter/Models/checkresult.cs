namespace CloudantDotNet.Models
{
    public class CheckResult
    {
        public string status { get; set; }
        public int totalchecks { get; set; }
        public Check[] checks { get; set; }
    }

}