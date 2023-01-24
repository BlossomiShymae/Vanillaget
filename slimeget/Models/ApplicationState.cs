using slimeget.Models;

namespace slimeget.Services
{
	internal class ApplicationState
    {
        public List<RequestMethodCollection> RequestMethodCollections = new();

        public RequestMethodCollection SelectedCollection = new();

        public RequestMethod SelectedRequest = new();

        public void AddRequestMethodCollection(ref RequestMethodCollection data)
        {
            try
            {
                data.Id = RequestMethodCollections.Last().Id + 1;
            }
            catch (InvalidOperationException) { data.Id = 0; }
            RequestMethodCollections.Add(data);
        }

        public void UpdateRequestMethodCollection(RequestMethodCollection data)
        {
            var index = RequestMethodCollections.FindIndex(x => x.Id == data.Id);
            if (index == -1)
                throw new Exception("Request method collection does not exist in application state.");
            RequestMethodCollections[index] = data;
        }
    }
}
