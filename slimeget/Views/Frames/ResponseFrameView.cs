using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ResponseFrameView : FrameView, IObservingView
    {
        private readonly ResponseFrameViewModel _viewModel;
        private readonly View _buttonPanel;
        private readonly Button _showHeadersButton;
        private readonly Button _showBodyButton;
        private readonly TextView _textView;

        public ResponseFrameView(ResponseFrameViewModel viewModel)
        {
            _viewModel = viewModel;

            // Button panel
            _buttonPanel = new()
            {
                X = 0,
                Y = 0,
                Height = 1,
                Width = Dim.Fill()
            };
            _showBodyButton = new()
            {
                X = 0,
                Y = 0,
                AutoSize = true,
                Text = "Body"
            };
            _showHeadersButton = new()
            {
                X = Pos.Right(_showBodyButton),
                Y = 0,
                AutoSize = true,
                Text = "Headers"
            };
            _buttonPanel.Add(_showHeadersButton, _showBodyButton);

            // Response view with scroll
            _textView = new TextView
            {
                X = 0,
                Y = Pos.Bottom(_buttonPanel),
                Height = Dim.Fill(),
                Width = Dim.Fill(),
                ReadOnly = true,
                Text = _viewModel.Text,
                ColorScheme = Colors.Menu,
            };
            Add(_buttonPanel, _textView);

            Title = viewModel.Title;

            _viewModel.PropertyChanged += OnPropertyChanged;
            _showHeadersButton.Clicked += () => _viewModel.ShowHeadersCommand.Execute(this);
            _showBodyButton.Clicked += () => _viewModel.ShowBodyCommand.Execute(this);
        }

        public void OnPropertyChanged(object? sender, EventArgs args)
        {
            _textView.Text = _viewModel.Text;
            Title = _viewModel.Title;
            Application.Refresh();
        }
    }
}
