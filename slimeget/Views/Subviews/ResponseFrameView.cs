using slimeget.Interfaces;
using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
    internal class ResponseFrameView : FrameView, IRefreshableView
    {
        private readonly ResponseFrameViewModel _viewModel;

        private readonly TextView _textView;

        private readonly ScrollBarView _scrollBarView;

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
            };
            Add(_textView);
            _scrollBarView = new ScrollBarView(_textView, true);

            Title = viewModel.Title;

            _viewModel.PropertyChanged += RefreshView;
        }

        public void RefreshView(object? sender, EventArgs args)
        {
            _textView.Text = _viewModel.Response;
            Application.Refresh();
        }
    }
}
