using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ServerFrameView : FrameView
    {
        private readonly ServerFrameViewModel _viewModel;

        private readonly ToplevelViewModel _toplevelViewModel;

        private readonly ListView _listView;

        public ServerFrameView(ServerFrameViewModel viewModel, ToplevelViewModel toplevelViewModel)
        {
            _viewModel = viewModel;
            _toplevelViewModel = toplevelViewModel;

            Title = _viewModel.Title;

            // Server list
            _listView = new ListView
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };
            _listView.SetSource(_toplevelViewModel.RequestMethodCollections.Select(x => x.Name).ToList());
            Add(_listView);

            // Request method collection update
            _toplevelViewModel.RequestMethodCollections.CollectionChanged += (s, e) =>
            {
                _listView.SetSource(_toplevelViewModel.RequestMethodCollections.Select(x => x.Name).ToList());
                Application.Refresh();
            };
        }
    }
}
