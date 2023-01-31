using Terminal.Gui;
using Vanillaget.ViewModels;
using Vanillaget.Views.Controls;

namespace Vanillaget.Views.Subviews
{
	internal class RequestFrameView : FrameView, IObservingView
	{
		private readonly RequestFrameViewModel _viewModel;
		private readonly ButtonMenu _buttonMenu = new();
		private readonly ButtonMenu _secondaryButtonMenu = new();
		private readonly ListView _listView;

		public RequestFrameView(RequestFrameViewModel viewModel)
		{
			_viewModel = viewModel;

			Title = _viewModel.Title;

			// Button menu
			var newButton = _buttonMenu.Add("New");
			var editButton = _buttonMenu.Add("Edit");
			var deleteButton = _buttonMenu.Add("Delete");

			// Request list
			_listView = new ListView
			{
				X = 0,
				Y = Pos.Bottom(_buttonMenu),
				Height = Dim.Fill() - _secondaryButtonMenu.Height,
				Width = Dim.Fill(),
			};
			_listView.SetSource(_viewModel.Requests);

			// Secondary button menu
			_secondaryButtonMenu.Y = Pos.Bottom(_listView);
			var sendButton = _secondaryButtonMenu.Add("Send");

			Add(_buttonMenu, _listView, _secondaryButtonMenu);

			_viewModel.PropertyChanged += OnPropertyChanged;
			_listView.OpenSelectedItem += viewModel.SelectRequestCommand.Execute;
			sendButton.Clicked += () => viewModel.SendRequestCommand.Execute(this);
		}

		public void OnPropertyChanged(object? sender, EventArgs args)
		{
			_listView.SetSource(_viewModel.Requests);
			_listView.SelectedItem = _viewModel.SelectedItem;
			Application.Refresh();
		}
	}
}
