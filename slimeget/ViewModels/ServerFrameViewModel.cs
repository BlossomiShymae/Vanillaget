using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Interfaces;
using slimeget.Models;
using slimeget.Services;

namespace slimeget.ViewModels
{
    internal partial class ServerFrameViewModel : ObservableObject, IUpdatableViewModel
    {
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private List<string> _serverNames = new();

        private readonly RepositoryService<RequestMethodCollection> _repositoryService;

        public ServerFrameViewModel(RepositoryService<RequestMethodCollection> repositoryService)
        {
            _repositoryService = repositoryService;

            Title = "Server";
            _repositoryService.RepositoryChanged += UpdateViewModel;
        }

        public void UpdateViewModel(object? sender, EventArgs args)
        {
            var data = _repositoryService.Get();
            ServerNames = _repositoryService.Get().Select(x => x.Name).ToList();
        }
    }
}
