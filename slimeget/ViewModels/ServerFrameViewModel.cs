using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ServerFrameViewModel : IRecipient<ApplicationStateUpdatedMessage>
	{
		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private List<string> _serverNames = new();

		public ServerFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Server";

			Messenger.Register<ApplicationStateUpdatedMessage>(this);
		}

		void IRecipient<ApplicationStateUpdatedMessage>.Receive(ApplicationStateUpdatedMessage message)
		{
			ServerNames = message.applicationState.RequestMethodCollections.Select(x => x.Name).ToList();
		}
	}
}
