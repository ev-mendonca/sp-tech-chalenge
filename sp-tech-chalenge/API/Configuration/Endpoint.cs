
namespace API.Configuration
{
    public class Endpoint
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Endpoint(){}
        public Endpoint(string url)
        {
            Url = url;
        }
        
    }
}
