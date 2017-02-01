namespace CloudantDotNet.Models
{
    public class Check
    {
        public string payee { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public decimal amount { get; set; }
        public string desc { get; set; }
        public int date { get; set; }
        public string id { get; set; }

        public string rev { get; set; }
    }


}