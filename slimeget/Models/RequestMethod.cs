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

        public string ResponseJson { get; set; } = string.Empty;

        public string PrettyPrintResponse()
        {
            if (Response == null) return String.Empty;

            try
            {
                var json = JToken.Parse(Response.Content.ReadAsStringAsync().Result).ToString();
                ResponseJson = json;
                return json;
            }
            catch (JsonReaderException)
            {
                if (string.IsNullOrEmpty(ResponseJson)) return String.Empty;
                return ResponseJson;
            }
        }
    }
}
