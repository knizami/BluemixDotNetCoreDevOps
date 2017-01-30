namespace CloudantDotNet.Models
{
    public class AuthResponse : CloudantDoc
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}