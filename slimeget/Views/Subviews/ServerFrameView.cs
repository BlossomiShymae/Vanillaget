using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ServerFrameView : FrameView
    {
        private readonly ServerFrameViewModel _viewModel;

        public ServerFrameView(ServerFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            Title = _viewModel.Title;
        }
    }
}
