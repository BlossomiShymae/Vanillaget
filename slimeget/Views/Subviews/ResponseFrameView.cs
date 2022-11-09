using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ResponseFrameView : FrameView
    {
        private readonly ResponseFrameViewModel _viewModel;

        public ResponseFrameView(ResponseFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            Title = viewModel.Title;
        }
    }
}
