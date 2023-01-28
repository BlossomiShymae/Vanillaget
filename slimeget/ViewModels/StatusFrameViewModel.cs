using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class StatusFrameViewModel : IRecipient<ApplicationStateMessage>, IRecipient<StatusUpdateMessage>
	{
		public readonly string Title = "Status";
		[ObservableProperty]
		private string _status = string.Empty;
		[ObservableProperty]
		private string _uri = string.Empty;

		public StatusFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;

			Messenger.Register<ApplicationStateMessage>(this);
			Messenger.Register<StatusUpdateMessage>(this);
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			var response = request.Response;
			if (response != null)
			{
				Status = $"{request.HttpMethod.Method} {(int)response.StatusCode} - {response.StatusCode}";
			}
		}

		void IRecipient<StatusUpdateMessage>.Receive(StatusUpdateMessage message)
		{
			Status = message.Status;
			Uri = message.Uri;
		}
	}
}
