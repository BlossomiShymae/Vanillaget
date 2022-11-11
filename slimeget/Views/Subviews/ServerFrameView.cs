using slimeget.Interfaces;
using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ServerFrameView : FrameView, IRefreshableView
    {
        private readonly ServerFrameViewModel _viewModel;

        private readonly ListView _listView;

        public ServerFrameView(ServerFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            Title = _viewModel.Title;

            // Server list
            _listView = new ListView
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };
            _listView.SetSource(_viewModel.ServerNames);
            Add(_listView);

            _viewModel.PropertyChanged += RefreshView;
        }

        public void RefreshView(object? sender, EventArgs args)
        {
            _listView.SetSource(_viewModel.ServerNames);
            Application.Refresh();
        }
    }
}
