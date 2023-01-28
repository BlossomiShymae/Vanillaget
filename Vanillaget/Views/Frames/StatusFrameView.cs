using Vanillaget.ViewModels;
using Terminal.Gui;

namespace Vanillaget.Views.Frames
{
    internal class StatusFrameView : FrameView, IObservingView
    {
        private readonly StatusFrameViewModel _viewModel;
        private readonly TextField _statusField;
        private readonly TextField _uriField;

        public StatusFrameView(StatusFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            _statusField = new TextField
            {
                X = 0,
                Y = 0,
                Height = 1,
                Width = Dim.Fill(),
                ReadOnly = true,
                ColorScheme = Colors.Menu,
            };
            _uriField = new TextField
            {
                X = 0,
                Y = Pos.Bottom(_statusField),
                Height = 1,
                Width = Dim.Fill(),
                ReadOnly = true,
                ColorScheme = Colors.Menu,
            };
            Add(_statusField, _uriField);

            Title = _viewModel.Title;

            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object? sender, EventArgs args)
        {
            _statusField.Text = _viewModel.Status;
            _uriField.Text = _viewModel.Uri;
            Application.Refresh();
        }
    }
}
