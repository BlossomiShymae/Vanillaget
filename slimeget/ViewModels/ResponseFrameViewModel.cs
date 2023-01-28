using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using slimeget.Messages;
using slimeget.Models;

namespace slimeget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ResponseFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		private static readonly string _baseTitle = "Response";
		private RequestMethod? _requestMethod;
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
			if (_requestMethod == null) return;

			var response = _requestMethod.Response;
			if (response == null) return;

			Text = response.Headers.ToString();
		}

		[RelayCommand]
		private void ShowBody()
		{
			Title = $"{_baseTitle} - Body";
			if (_requestMethod == null) return;

			Text = _requestMethod.PrettyPrintResponse();
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			_requestMethod = request;

			ShowBody();
		}
	}
}
