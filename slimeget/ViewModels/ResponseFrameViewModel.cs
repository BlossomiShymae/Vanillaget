using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ResponseFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private string _response = String.Empty;

		public ResponseFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Response";

			Messenger.Register<ApplicationStateMessage>(this);
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			Response = message.applicationState.SelectedRequest.PrettyPrintResponse();
		}
	}
}
