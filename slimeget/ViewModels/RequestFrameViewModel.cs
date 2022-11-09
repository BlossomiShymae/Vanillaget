using CommunityToolkit.Mvvm.ComponentModel;

namespace slimeget.ViewModels
{
    internal partial class RequestFrameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = String.Empty;

        public RequestFrameViewModel()
        {
            Title = "Request";
        }
    }
}
