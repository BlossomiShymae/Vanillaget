using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Services;

namespace slimeget.ViewModels
{
    internal partial class ServerFrameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private List<string> _serverNames = new();

        public ServerFrameViewModel()
        {
            Title = "Server";
        }

        public void Resolve(ApplicationState state)
        {
            ServerNames = state.RequestMethodCollections.Select(x => x.Name).ToList();
        }
    }
}
