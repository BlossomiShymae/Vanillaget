using slimeget.ViewModels;
using Terminal.Gui;

namespace slimeget.Views.Subviews
{
	internal class ServerFrameView : FrameView, IObservingView
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
			_listView.SetSource(_viewModel.Servers);
			Add(_listView);

			_viewModel.PropertyChanged += OnPropertyChanged;
			_listView.OpenSelectedItem += viewModel.SelectServerCommand.Execute;
		}

		public void OnPropertyChanged(object? sender, EventArgs args)
		{
			_listView.SetSource(_viewModel.Servers);
			_listView.SelectedItem = _viewModel.SelectedItem;
			Application.Refresh();
		}
	}
}
