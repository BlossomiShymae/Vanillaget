using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class RequestFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		[ObservableProperty]
		private string _title = String.Empty;

		[ObservableProperty]
		private List<string> _requests = new();

		public RequestFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;
			Title = "Request";

			Messenger.Register<ApplicationStateMessage>(this);
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			Requests = message.applicationState.SelectedCollection.RequestMethods
				.Select(x => $"{x.HttpMethod.Method.ToUpper()} {x.Name}")
				.ToList();
		}
	}
}
