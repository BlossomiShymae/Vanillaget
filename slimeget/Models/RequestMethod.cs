using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace slimeget.Models
{

    internal class RequestMethod
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string ResourcePath { get; set; } = String.Empty;

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

        public HttpResponseMessage? Response { get; set; } = null;

        public string PrettyPrintResponse()
        {
            if (Response == null) return String.Empty;

            try
            {
                return JToken.Parse(Response.Content.ReadAsStringAsync().Result).ToString();
            }
            catch (JsonReaderException) { return String.Empty; }
        }
    }
}
