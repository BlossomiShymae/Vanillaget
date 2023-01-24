using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Services;

namespace slimeget.ViewModels
{
    internal partial class RequestFrameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private List<string> _requests = new();

        public RequestFrameViewModel()
        {
            Title = "Request";
        }

        public void Resolve(ApplicationState state)
        {
            Requests = state.SelectedCollection.RequestMethods
                .Select(x => $"{x.HttpMethod.Method.ToUpper()} {x.Name}")
                .ToList();
        }
    }
}
