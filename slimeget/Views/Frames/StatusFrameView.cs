using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Frames
{
	internal class StatusFrameView : FrameView, IObservingView
	{
		private readonly StatusFrameViewModel _viewModel;
		private readonly TextField _statusField;
		private readonly ProgressBar _progressBar;

		public StatusFrameView(StatusFrameViewModel viewModel)
		{
			_viewModel = viewModel;

			_statusField = new TextField
			{
				X = 0,
				Y = 0,
				Height = Dim.Fill(),
				Width = Dim.Fill(),
				ReadOnly = true,
				ColorScheme = Colors.Menu,
			};
			_progressBar = new ProgressBar
			{
				X = 0,
				Y = Pos.Bottom(_statusField),
				Height = Dim.Fill(),
				Width = Dim.Fill(),
				Fraction = 0.0f
			};
			Add(_statusField, _progressBar);

			Title = _viewModel.Title;

			_viewModel.PropertyChanged += OnPropertyChanged;
		}

		public void OnPropertyChanged(object? sender, EventArgs args)
		{
			_statusField.Text = _viewModel.StatusText;
			_progressBar.Fraction = _viewModel.GetProgress();
			Application.Refresh();
		}
	}
}
