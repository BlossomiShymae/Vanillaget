using Terminal.Gui;
using Vanillaget.ViewModels;
using Vanillaget.Views.Controls;

namespace Vanillaget.Views.Subviews
{
	internal class ServerFrameView : FrameView, IObservingView
	{
		private readonly ServerFrameViewModel _viewModel;
		private readonly ButtonMenu _buttonMenu = new();
		private readonly ListView _listView;

		public ServerFrameView(ServerFrameViewModel viewModel)
		{
			_viewModel = viewModel;

			Title = _viewModel.Title;

			// Button menu
			var newButton = _buttonMenu.Add("New");
			var editButton = _buttonMenu.Add("Edit");
			var deleteButton = _buttonMenu.Add("Delete");

			// Server list
			_listView = new ListView
			{
				X = 0,
				Y = Pos.Bottom(_buttonMenu),
				Height = Dim.Fill(),
				Width = Dim.Fill(),
			};
			_listView.SetSource(_viewModel.Servers);
			Add(_buttonMenu, _listView);

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
