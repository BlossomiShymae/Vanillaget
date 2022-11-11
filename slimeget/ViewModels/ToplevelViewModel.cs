using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using slimeget.Models;
using slimeget.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Terminal.Gui;

namespace slimeget.ViewModels
{
    internal partial class ToplevelViewModel : ObservableObject
    {
        private readonly RepositoryService<RequestMethodCollection> _repositoryService;

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

        public ToplevelViewModel(RepositoryService<RequestMethodCollection> repositoryService)
        {
            _repositoryService = repositoryService;

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
            Trace.WriteLine("saving request method collection");
            var id = 0;
            try
            {
                id = _repositoryService.Get().Last().Id + 1;
            }
            catch (Exception e) { Trace.WriteLine(e.Message); }

            var collection = new RequestMethodCollection
            {
                Id = id,
                Name = ServerName,
                Hostname = ServerHostname,
                Port = ServerPort,
            };

            _repositoryService.Add(collection);
            _repositoryService.Selection = collection.Id;
        }

        [RelayCommand]
        private void SaveRequestMethod()
        {
            Trace.WriteLine("saving request method");
            try
            {
                Predicate<RequestMethodCollection> match = x => x.Id == _repositoryService.Selection;
                var collections = _repositoryService.Get();
                var collection = collections.Find(match);

                var id = 0;
                if (collection.RequestMethods.Count > 0)
                {
                    var last = collection.RequestMethods.Last();
                    id = last.Id + 1;
                }
                var requestMethod = new RequestMethod
                {
                    Id = id,
                    Name = RequestName,
                    ResourcePath = RequestResourcePath,
                    HttpMethod = RequestHttpMethod
                };
                collection.RequestMethods.Add(requestMethod);
                _repositoryService.Update(collection);
            }
            catch (Exception e) { Trace.WriteLine(e); }
        }

        [RelayCommand]
        private void SendRequestNow()
        {

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
