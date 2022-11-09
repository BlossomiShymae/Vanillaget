namespace slimeget.Models
{

    internal class RequestMethod
    {
        public string ResourcePath { get; set; } = String.Empty;

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
    }
}
