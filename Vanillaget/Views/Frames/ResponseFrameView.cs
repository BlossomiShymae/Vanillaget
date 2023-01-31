using Terminal.Gui;
using Vanillaget.ViewModels;
using Vanillaget.Views.Controls;

namespace Vanillaget.Views.Subviews
{
	internal class ResponseFrameView : FrameView, IObservingView
	{
		private readonly ResponseFrameViewModel _viewModel;
		private readonly ButtonMenu _buttonMenu = new();
		private readonly TextView _textView;

		public ResponseFrameView(ResponseFrameViewModel viewModel)
		{
			_viewModel = viewModel;

			// Button menu
			var showBodyButton = _buttonMenu.Add("Body");
			var showHeadersButton = _buttonMenu.Add("Headers");

			// Response view with scroll
			_textView = new TextView
			{
				X = 0,
				Y = Pos.Bottom(_buttonMenu),
				Height = Dim.Fill(),
				Width = Dim.Fill(),
				ReadOnly = true,
				Text = _viewModel.Text,
				ColorScheme = Colors.Menu,
			};
			Add(_buttonMenu, _textView);

			Title = viewModel.Title;

			_viewModel.PropertyChanged += OnPropertyChanged;
			showHeadersButton.Clicked += () => _viewModel.ShowHeadersCommand.Execute(this);
			showBodyButton.Clicked += () => _viewModel.ShowBodyCommand.Execute(this);
		}

		public void OnPropertyChanged(object? sender, EventArgs args)
		{
			_textView.Text = _viewModel.Text;
			Title = _viewModel.Title;
			Application.Refresh();
		}
	}
}
