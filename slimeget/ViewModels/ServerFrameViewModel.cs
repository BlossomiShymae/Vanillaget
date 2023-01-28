using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;
using slimeget.Services;
using Terminal.Gui;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ServerFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		private ApplicationState? _applicationState;
		[ObservableProperty]
		private string _title = String.Empty;
		[ObservableProperty]
		private List<string> _servers = new();
		[ObservableProperty]
		private int _selectedItem = 0;

		public ServerFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Server";

			Messenger.Register<ApplicationStateMessage>(this);
		}

		[RelayCommand]
		private void SelectServer(ListViewItemEventArgs e)
		{
			var applicationState = _applicationState;
			if (applicationState == null) return;

			var servers = applicationState.RequestMethodCollections;
			var server = servers.ElementAtOrDefault(e.Item);
			if (server == null) return;

			applicationState.SelectedCollection = server;
			_selectedItem = e.Item;
			Messenger.Send<ApplicationStateMessage>(new(applicationState));
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			_applicationState = message.applicationState;

			var collections = message.applicationState.RequestMethodCollections;
			var servers = collections.Select(x => x.Name).ToList();
			if (SelectedItem > servers.Count - 1) SelectedItem = 0;
			Servers = servers;
		}
	}
}
