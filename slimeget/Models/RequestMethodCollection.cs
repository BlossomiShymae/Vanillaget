namespace slimeget.Models
{
    internal class RequestMethodCollection
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Hostname { get; set; } = String.Empty;

        public uint Port { get; set; }

        public List<RequestMethod> RequestMethods { get; set; } = new();

        public void Add(ref RequestMethod data)
        {
            try
            {
                data.Id = RequestMethods.Last().Id + 1;
            }
            catch (InvalidOperationException) { data.Id = 0; }
            RequestMethods.Add(data);
        }

        public void Update(RequestMethod data)
        {
            var index = RequestMethods.FindIndex(x => x.Id == data.Id);
            if (index == -1)
                throw new Exception("Request method collection does not exist in application state.");
            RequestMethods[index] = data;
        }
    }
}
