namespace CloudantDotNet.Models
{
    public class CloudantQuery
    {
        public object selector { get; set; }

        public CloudantQuery(object query)
        {
            selector = query;
        }
    }
}