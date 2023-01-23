using CommunityToolkit.Mvvm.ComponentModel;
using slimeget.Services;

namespace slimeget.ViewModels
{
    internal partial class ResponseFrameViewModel : ObservableObject, IMediatorModule
    {
        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private string _response = String.Empty;

        public ResponseFrameViewModel()
        {
            Title = "Response";
        }

        public void Resolve(ApplicationState state)
        {
            Response = state.SelectedRequest.PrettyPrintResponse();
        }
    }
}
