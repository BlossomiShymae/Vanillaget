using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ResponseFrameView : FrameView, IObservingView
    {
        private readonly ResponseFrameViewModel _viewModel;

        private readonly TextView _textView;

        public ResponseFrameView(ResponseFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            // Response view with scroll
            _textView = new TextView
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Fill(),
                ReadOnly = true,
                Text = _viewModel.Response,
                ColorScheme = Colors.Menu,
            };
            Add(_textView);

            Title = viewModel.Title;

            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object? sender, EventArgs args)
        {
            _textView.Text = _viewModel.Response;
            Application.Refresh();
        }
    }
}
