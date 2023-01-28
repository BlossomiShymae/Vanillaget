using Newtonsoft.Json;
using Vanillaget.Models;

namespace Vanillaget.Services
{
    internal class ApplicationState
    {
        public List<RequestMethodCollection> RequestMethodCollections = new();
        public RequestMethodCollection SelectedCollection = new();
        public RequestMethod? SelectedRequest = new();

        public void Add(ref RequestMethodCollection data)
        {
            try
            {
                data.Id = RequestMethodCollections.Last().Id + 1;
            }
            catch (InvalidOperationException) { data.Id = 0; }
            RequestMethodCollections.Add(data);
        }

        public void Update(RequestMethodCollection data)
        {
            var index = RequestMethodCollections.FindIndex(x => x.Id == data.Id);
            if (index == -1)
                throw new Exception("Request method collection does not exist in application state.");
            RequestMethodCollections[index] = data;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            if (json == null) return;

            App.Default.ApplicationState = json;
            App.Default.Save();
        }

        public static ApplicationState? LoadInstance()
        {
            var json = App.Default.ApplicationState;
            if (string.IsNullOrEmpty(json)) return null;

            var data = JsonConvert.DeserializeObject<ApplicationState>(json);
            if (data == null) return null;

            return data;
        }
    }
}
