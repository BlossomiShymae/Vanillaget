using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Interfaces;
using slimeget.Models;
using slimeget.Services;

namespace slimeget.ViewModels
{
    internal partial class RequestFrameViewModel : ObservableObject, IUpdatableViewModel
    {
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private List<string> _requests = new();

        private readonly RepositoryService<RequestMethodCollection> _repositoryService;

        public RequestFrameViewModel(RepositoryService<RequestMethodCollection> repositoryService)
        {
            _repositoryService = repositoryService;

            Title = "Request";
            _repositoryService.RepositoryChanged += UpdateViewModel;
            _repositoryService.SelectionChanged += UpdateViewModel;
        }

        public void UpdateViewModel(object? sender, EventArgs args)
        {
            Requests = _repositoryService.Find(_repositoryService.Selection).RequestMethods.Select(x =>
            {
                return $"{x.HttpMethod.Method.ToUpper()} {x.Name}";
            }).ToList();
        }
    }
}
