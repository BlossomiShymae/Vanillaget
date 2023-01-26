using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class StatusFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		public readonly string Title = "Status";
		[ObservableProperty]
		private ushort _progress = 0;
		[ObservableProperty]
		private string _statusText = string.Empty;

		public StatusFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;

			Messenger.Register<ApplicationStateMessage>(this);
		}

		public float GetProgress()
		{
			var value = Progress / 100f;
			if (value > 1.0f) return 1.0f;
			return value;
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			var response = request.Response;
			if (response != null)
			{
				StatusText = $"{(int)response.StatusCode} - {response.StatusCode}";
			}
		}
	}
}
