using Terminal.Gui;
using Vanillaget.ViewModels;
using Vanillaget.Views.Frames;
using Vanillaget.Views.Subviews;
using Vanillaget.Views.Wizards;

namespace Vanillaget.Views
{
	internal class ToplevelView : Toplevel
	{
		private readonly ToplevelViewModel _viewModel;
		private readonly MenuBar _menuBar;
		private readonly FrameView _serverFrame;
		private readonly FrameView _requestFrame;
		private readonly FrameView _responseFrame;
		private readonly FrameView _statusFrame;
		private readonly View _leftPanel;
		private readonly View _rightPanel;
		private readonly View _rootPanel;

		public ToplevelView(ToplevelViewModel viewModel,
			ServerFrameView serverFrame,
			RequestFrameView requestFrame,
			ResponseFrameView responseFrame,
			StatusFrameView statusFrame)
		{
			_viewModel = viewModel;
			_viewModel.MenuItemClicked += OnMenuItemClicked;

			X = 0;
			Y = 0;
			Width = Dim.Fill();
			Height = Dim.Fill();

			_serverFrame = serverFrame;
			_requestFrame = requestFrame;
			_responseFrame = responseFrame;
			_statusFrame = statusFrame;

			_menuBar = new(_viewModel.MenuBarItems.ToArray());

			// Panel that contains both left-right panels
			_rootPanel = new()
			{
				X = 0,
				Y = Pos.Bottom(_menuBar),
				Width = Dim.Fill(),
				Height = Dim.Fill() - 4
			};

			// Left panel the user sees
			_leftPanel = new()
			{
				X = 0,
				Y = 0,
				Width = Dim.Percent(25f),
				Height = Dim.Fill()
			};
			_serverFrame.X = 0;
			_serverFrame.Y = 0;
			_serverFrame.Height = Dim.Percent(25f);
			_serverFrame.Width = Dim.Fill();
			_requestFrame.X = 0;
			_requestFrame.Y = Pos.Bottom(_serverFrame);
			_requestFrame.Height = Dim.Fill();
			_requestFrame.Width = Dim.Fill();
			_leftPanel.Add(_serverFrame, _requestFrame);

			// Right panel the user sees
			_rightPanel = new()
			{
				X = Pos.Right(_leftPanel),
				Y = 0,
				Width = Dim.Fill(),
				Height = Dim.Fill()
			};
			_responseFrame.X = 0;
			_responseFrame.Y = 0;
			_responseFrame.Height = Dim.Fill();
			_responseFrame.Width = Dim.Fill();
			_rightPanel.Add(_responseFrame);
			var scrollBarFrame = new ScrollBarView(_responseFrame, true, true);
			_rightPanel.Add(scrollBarFrame);

			_rootPanel.Add(_leftPanel, _rightPanel);

			_statusFrame.X = 0;
			_statusFrame.Y = Pos.Bottom(_rootPanel);
			_statusFrame.Height = Dim.Fill();
			_statusFrame.Width = Dim.Fill();

			Add(_rootPanel, _menuBar, _statusFrame);
		}

		public void Load()
		{
			_viewModel.LoadApplication();
		}

		private void OnMenuItemClicked(object? sender, MenuItemClickedEventArgs args)
		{
			switch (args.MenuItem)
			{
				case MenuItems.FileClose:
					_viewModel.CloseApplicationCommand.Execute(this);
					break;
				case MenuItems.ServerNew:
					var serverWizard = ServerWizard.CreateFromType(ServerWizard.WizardType.New);
					Action<Wizard.WizardButtonEventArgs> serverHandler = (args) =>
					{
						_viewModel.ServerName = serverWizard.Name;
						_viewModel.ServerHostname = serverWizard.Hostname;
						_viewModel.ServerPort = serverWizard.Port;
						_viewModel.SaveRequestMethodCollectionCommand.Execute(this);
					};
					serverWizard.Finished += serverHandler;
					Application.Run(serverWizard);
					serverWizard.Finished -= serverHandler;
					break;
				case MenuItems.RequestNew:
					var requestWizard = RequestWizard.CreateFromType(RequestWizard.WizardType.New);
					Action<Wizard.WizardButtonEventArgs> requestHandler = (args) =>
					{
						_viewModel.RequestName = requestWizard.Name;
						_viewModel.RequestResourcePath = requestWizard.Path;
						_viewModel.RequestHttpMethod = requestWizard.Method;
						_viewModel.SaveRequestMethodCommand.Execute(this);
					};
					requestWizard.Finished += requestHandler;
					Application.Run(requestWizard);
					requestWizard.Finished -= requestHandler;
					break;
				case MenuItems.RequestSendNow:
					_viewModel.SendRequestNowCommand.Execute(this);
					break;
				default:
					break;
			}
		}

		private static void TryErrorQuery(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch (Exception e)
			{
				MessageBox.ErrorQuery($"Error - {e.GetType().Name}", $"{e.Message}\n{e.StackTrace}", "Ok");
			}
		}
	}
}
