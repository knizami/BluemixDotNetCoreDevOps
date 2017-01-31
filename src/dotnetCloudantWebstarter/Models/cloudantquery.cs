namespace CloudantDotNet.Models
{
    public class CloudantQuery
    {
        public object selector { get; set; }
        public string[] fields;

        public CloudantQuery(object query, string[] filterquery)
        {
            selector = query;
            fields = filterquery;
        }
    }
}