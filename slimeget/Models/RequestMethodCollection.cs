namespace slimeget.Models
{
    internal class RequestMethodCollection
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Hostname { get; set; } = String.Empty;

        public uint Port { get; set; }

        public List<RequestMethod> RequestMethods { get; set; } = new();
    }
}
