using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class RequestFrameView : FrameView
    {
        private readonly RequestFrameViewModel _viewModel;

        public RequestFrameView(RequestFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            Title = _viewModel.Title;
        }
    }
}
