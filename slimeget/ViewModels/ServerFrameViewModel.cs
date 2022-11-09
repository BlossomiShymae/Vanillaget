using CommunityToolkit.Mvvm.ComponentModel;

namespace slimeget.ViewModels
{
    internal partial class ServerFrameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = String.Empty;

        public ServerFrameViewModel()
        {
            Title = "Server";
        }
    }
}
