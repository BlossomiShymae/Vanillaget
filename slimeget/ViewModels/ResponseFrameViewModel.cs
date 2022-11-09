using CommunityToolkit.Mvvm.ComponentModel;

namespace slimeget.ViewModels
{
    internal partial class ResponseFrameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = String.Empty;

        public ResponseFrameViewModel()
        {
            Title = "Response";
        }
    }
}
