using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class RequestFrameView : FrameView, IObservingView
    {
        private readonly RequestFrameViewModel _viewModel;

        private readonly ListView _listView;

        public RequestFrameView(RequestFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            Title = _viewModel.Title;

            // Request list
            _listView = new ListView
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };
            _listView.SetSource(_viewModel.Requests);
            Add(_listView);

            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object? sender, EventArgs args)
        {
            _listView.SetSource(_viewModel.Requests);
            Application.Refresh();
        }
    }
}
