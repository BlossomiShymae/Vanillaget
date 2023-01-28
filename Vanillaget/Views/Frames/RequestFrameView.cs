using Vanillaget.ViewModels;
using Terminal.Gui;

namespace Vanillaget.Views.Subviews
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
            _listView.OpenSelectedItem += viewModel.SelectRequestCommand.Execute;
        }

        public void OnPropertyChanged(object? sender, EventArgs args)
        {
            _listView.SetSource(_viewModel.Requests);
            _listView.SelectedItem = _viewModel.SelectedItem;
            Application.Refresh();
        }
    }
}
