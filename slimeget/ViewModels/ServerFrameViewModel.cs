using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ServerFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private List<string> _serverNames = new();

		public ServerFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Server";

			Messenger.Register<ApplicationStateMessage>(this);
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			ServerNames = message.applicationState.RequestMethodCollections.Select(x => x.Name).ToList();
		}
	}
}
