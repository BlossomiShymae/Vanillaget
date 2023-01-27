using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ResponseFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		private static readonly string _baseTitle = "Response";
		[ObservableProperty]
		private string _title = _baseTitle;
		[ObservableProperty]
		private string _text = string.Empty;

		public ResponseFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;

			Messenger.Register<ApplicationStateMessage>(this);
		}

		[RelayCommand]
		private void ShowHeaders()
		{
			Title = $"{_baseTitle} - Headers";
		}

		[RelayCommand]
		private void ShowBody()
		{
			Title = $"{_baseTitle} - Body";
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			var response = request.Response;

			if (response == null) return;

			Text = request.PrettyPrintResponse();
		}
	}
}
