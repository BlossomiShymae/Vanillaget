using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ResponseFrameViewModel : IRecipient<ApplicationStateUpdatedMessage>
	{
		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private string _response = String.Empty;

		public ResponseFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Response";

			Messenger.Register<ApplicationStateUpdatedMessage>(this);
		}

		void IRecipient<ApplicationStateUpdatedMessage>.Receive(ApplicationStateUpdatedMessage message)
		{
			Response = message.applicationState.SelectedRequest.PrettyPrintResponse();
		}
	}
}
