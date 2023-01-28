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
			var method = _requestMethod;
			if (method != null) Title = $"{_requestMethod} - {_baseTitle} - Headers";
			else
			{
				Title = $"{_baseTitle} - Headers";
				return;
			}

			var response = method.Response;
			if (response == null) return;

			Text = response.Headers.ToString();
		}

		[RelayCommand]
		private void ShowBody()
		{
			var method = _requestMethod;
			if (method != null) Title = $"{_requestMethod} - {_baseTitle} - Body";
			else
			{
				Title = $"{_baseTitle} - Body";
				return;
			}

			Text = method.PrettyPrintResponse();
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			var request = message.applicationState.SelectedRequest;
			_requestMethod = request;

			ShowBody();
		}
	}
}
