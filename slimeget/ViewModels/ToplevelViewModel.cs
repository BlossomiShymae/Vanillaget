using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using slimeget.Models;
using slimeget.Services;
using System.Collections.ObjectModel;
using Terminal.Gui;

namespace slimeget.ViewModels
{
    internal partial class ToplevelViewModel : ObservableObject
    {
        [ObservableProperty]
        private ApplicationState _applicationState = new();

        private readonly HttpClient _httpClient;

        public ObservableCollection<MenuBarItem> MenuBarItems = new();

        public event EventHandler<MenuItemClickedEventArgs>? MenuItemClicked;

        [ObservableProperty]
        private string _serverName = String.Empty;

        [ObservableProperty]
        private string _serverHostname = String.Empty;

        [ObservableProperty]
        private uint _serverPort;

        [ObservableProperty]
        private string _requestName = String.Empty;

        [ObservableProperty]
        private string _requestResourcePath = String.Empty;

        [ObservableProperty]
        private HttpMethod _requestHttpMethod = HttpMethod.Get;

        public ToplevelViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;

            MenuBarItems = new()
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("_Close", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.FileClose }))
                }),
                new MenuBarItem("_Server", new MenuItem[]
                {
                    new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.ServerNew }))
                }),
                new MenuBarItem("_Request", new MenuItem[]
                {
                    new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new() { MenuItem = MenuItems.RequestNew })),
                    new MenuItem("_Send Now", "", () => MenuItemClicked?.Invoke(this, new() { MenuItem = MenuItems.RequestSendNow })),
                })
            };
        }

        [RelayCommand]
        private void SaveRequestMethodCollection()
        {
            var collection = new RequestMethodCollection
            {
                Name = ServerName,
                Hostname = ServerHostname,
                Port = ServerPort,
            };

            _applicationState.AddRequestMethodCollection(ref collection);
            _applicationState.SelectedCollection = collection;
        }

        [RelayCommand]
        private void SaveRequestMethod()
        {
            var collection = _applicationState.SelectedCollection;
            if (collection == null)
                throw new Exception("A server collection must be selected!");
            var requestMethod = new RequestMethod
            {
                Name = RequestName,
                ResourcePath = RequestResourcePath,
                HttpMethod = RequestHttpMethod,
            };
            collection.AddRequestMethod(ref requestMethod);
            _applicationState.UpdateRequestMethodCollection(collection);
            _applicationState.SelectedRequest = requestMethod;
        }

        [RelayCommand]
        private void SendRequestNow()
        {
            var collection = _applicationState.SelectedCollection;
            if (collection == null)
                throw new Exception("A server collection must be selected!");

            // Deal BaseAddress oddities with "/" character
            var hostname = collection.Hostname;
            var port = $":{collection.Port}";
            // Append "/" as needed for hostname
            if (!hostname.EndsWith("/"))
            {
                //hostname = $"{hostname}{port}";
                hostname = hostname.Insert(hostname.Length, "/");
            }
            else
            {
                //hostname = hostname.Insert(hostname.Length - 1, port);
            }
            var uri = new Uri(hostname);

            // Remove "/" as needed for resource path
            var request = _applicationState.SelectedRequest;
            if (request == null)
                throw new Exception("Cannot send request without a selected request!");
            var resourcePath = request.ResourcePath;
            if (resourcePath.StartsWith("/"))
                resourcePath = resourcePath.Remove(0, 1);

            // Prepare to send request
            HttpResponseMessage response = null;
            if (request.HttpMethod == HttpMethod.Get)
            {
                response = _httpClient.GetAsync($"{hostname}{resourcePath}").Result;
            }

            // Prepare to process response
            if (response == null)
                throw new Exception("Failed to received a response!");
            request.Response = response;
            _applicationState.SelectedRequest = request;
            collection.UpdateRequestMethod(request);
            _applicationState.SelectedCollection = collection;
            _applicationState.UpdateRequestMethodCollection(collection);
        }

        public void Resolve(ApplicationState state)
        {
            _applicationState = state;
        }
    }

    internal enum MenuItems
    {
        FileClose,
        ServerNew,
        RequestNew,
        RequestSendNow,
    }

    internal class MenuItemClickedEventArgs : EventArgs
    {
        public MenuItems MenuItem { get; set; }
    }
}
