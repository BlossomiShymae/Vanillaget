using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Terminal.Gui;
using Vanillaget.Messages;
using Vanillaget.Services;

namespace Vanillaget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class RequestFrameViewModel : IRecipient<ApplicationStateMessage>
	{
		private ApplicationState? _applicationState;
		[ObservableProperty]
		private int _selectedItem = 0;
		[ObservableProperty]
		private string _title = "Request";
		[ObservableProperty]
		private List<string> _requests = new();

		public RequestFrameViewModel(IMessenger messenger)
		{
			Messenger = messenger;

			Messenger.Register<ApplicationStateMessage>(this);
		}

		[RelayCommand]
		private void SendRequest()
		{
			Messenger.Send<SendRequestMessage>(new());
		}

		[RelayCommand]
		private void CreateRequest()
		{
			Messenger.Send<CreateRequestMessage>(new());
		}

		[RelayCommand]
		private void EditRequest()
		{
			Messenger.Send<EditRequestMessage>(new());
		}

		[RelayCommand]
		private void SelectRequest(ListViewItemEventArgs e)
		{
			var applicationState = _applicationState;
			if (applicationState == null) return;

			var methods = applicationState.SelectedCollection.RequestMethods;
			var method = methods.ElementAtOrDefault(e.Item);
			if (method == null) return;

			applicationState.SelectedRequest = method;
			_selectedItem = e.Item;
			Messenger.Send<ApplicationStateMessage>(new(applicationState));
		}

		void IRecipient<ApplicationStateMessage>.Receive(ApplicationStateMessage message)
		{
			_applicationState = message.applicationState;

			var methods = message.applicationState.SelectedCollection.RequestMethods;
			var requests = methods
				.Select(x => x.ToString())
				.ToList();
			if (SelectedItem > requests.Count - 1) SelectedItem = 0;
			Requests = requests;

		}
	}
}
