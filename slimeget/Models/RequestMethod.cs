using slimeget.Interfaces;

namespace slimeget.Models
{

    internal class RequestMethod : IIndexable
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string ResourcePath { get; set; } = String.Empty;

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
    }
}
