using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ResponseFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		private readonly string _baseTitle = "Response";

		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private string _response = String.Empty;

		public ResponseFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = _baseTitle;

			Messenger.Register<ApplicationStateMessage>(this);
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			var response = request.Response;

			if (response == null) return;

			Response = request.PrettyPrintResponse();
			Title = $"{_baseTitle} - {(int)response.StatusCode}";
		}
	}
}
