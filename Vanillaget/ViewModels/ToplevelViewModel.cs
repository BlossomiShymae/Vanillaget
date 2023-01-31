using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Terminal.Gui;
using Vanillaget.Messages;
using Vanillaget.Models;
using Vanillaget.Services;

namespace Vanillaget.ViewModels
{
	[ObservableRecipient]
	[ObservableObject]
	internal partial class ToplevelViewModel : IRecipient<SendRequestMessage>
	{
		public event EventHandler<MenuItemClickedEventArgs>? MenuItemClicked;
		public ObservableCollection<MenuBarItem> MenuBarItems = new();
		[ObservableProperty]
		private ApplicationState _applicationState = new();
		[ObservableProperty]
		private string _serverName = String.Empty;
		[ObservableProperty]
		private string _serverHostname = String.Empty;
		[ObservableProperty]
		private uint _serverPort;
		[ObservableProperty]
		private string _requestName = String.Empty;
		[ObservableProperty]
		private string _requestResourcePath = String.Empty;
		[ObservableProperty]
		private HttpMethod _requestHttpMethod = HttpMethod.Get;

		private readonly HttpClient _httpClient;

		public ToplevelViewModel(IMessenger messenger, HttpClient httpClient)
		{
			Messenger = messenger;
			_httpClient = httpClient;

			MenuBarItems = new()
			{
				new MenuBarItem("_File", new MenuItem[]
				{
					new MenuItem("_Close", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.FileClose }))
				}),
				new MenuBarItem("_Server", new MenuItem[]
				{
					new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new () { MenuItem = MenuItems.ServerNew }))
				}),
				new MenuBarItem("_Request", new MenuItem[]
				{
					new MenuItem("_New", "", () => MenuItemClicked?.Invoke(this, new() { MenuItem = MenuItems.RequestNew })),
					new MenuItem("_Send Now", "", () => MenuItemClicked?.Invoke(this, new() { MenuItem = MenuItems.RequestSendNow })),
				})
			};

			Messenger.Register<SendRequestMessage>(this);
		}

		public void LoadApplication()
		{
			var applicationState = ApplicationState.LoadInstance();
			if (applicationState != null)
			{
				_applicationState = applicationState;
				Messenger.Send<ApplicationStateMessage>(new(_applicationState));
			}
		}

		[RelayCommand]
		private void CloseApplication()
		{
			_applicationState.Save();
			Application.RequestStop();
		}

		[RelayCommand]
		private void SaveRequestMethodCollection()
		{
			var collection = new RequestMethodCollection
			{
				Name = ServerName,
				Hostname = ServerHostname,
				Port = ServerPort,
			};

			var applicationState = _applicationState;
			applicationState.Add(ref collection);
			applicationState.SelectedCollection = collection;
			applicationState.SelectedRequest = null;

			_applicationState = applicationState;
			Messenger.Send<ApplicationStateMessage>(new(applicationState));
		}

		[RelayCommand]
		private void SaveRequestMethod()
		{
			var applicationState = _applicationState;
			var collection = applicationState.SelectedCollection;
			if (collection == null)
				throw new Exception("A server collection must be selected!");
			var requestMethod = new RequestMethod
			{
				Name = RequestName,
				ResourcePath = RequestResourcePath,
				HttpMethod = RequestHttpMethod,
			};
			collection.Add(ref requestMethod);
			applicationState.Update(collection);
			applicationState.SelectedRequest = requestMethod;

			_applicationState = applicationState;
			Messenger.Send<ApplicationStateMessage>(new(applicationState));
		}

		[RelayCommand]
		private async Task SendRequestNow()
		{
			var applicationState = _applicationState;
			var collection = applicationState.SelectedCollection;
			if (collection == null)
				throw new Exception("A server collection must be selected!");

			// Deal BaseAddress oddities with "/" character
			var hostname = collection.Hostname;
			var port = $":{collection.Port}";
			// Append "/" as needed for hostname
			if (!hostname.EndsWith("/"))
			{
				hostname = $"{hostname}{port}";
				hostname = hostname.Insert(hostname.Length, "/");
			}
			else
			{
				hostname = hostname.Insert(hostname.Length - 1, port);
			}

			// Remove "/" as needed for resource path
			var request = applicationState.SelectedRequest;
			if (request == null)
				throw new Exception("Cannot send request without a selected request!");
			var resourcePath = request.ResourcePath;
			if (resourcePath.StartsWith("/"))
				resourcePath = resourcePath.Remove(0, 1);

			// Prepare to send request
			HttpResponseMessage? response = null;
			var uri = hostname + resourcePath;
			Messenger.Send<StatusUpdateMessage>(new($"Sending {request.HttpMethod.Method} request to {uri}...", uri));
			if (request.HttpMethod == HttpMethod.Get)
			{
				response = await _httpClient.GetAsync(uri);
			}

			// Prepare to process response
			if (response == null)
				throw new Exception("Failed to received a response!");
			request.Response = response;
			applicationState.SelectedRequest = request;
			collection.Update(request);
			applicationState.SelectedCollection = collection;
			applicationState.Update(collection);

			_applicationState = applicationState;
			Messenger.Send<ApplicationStateMessage>(new(applicationState));
		}

		void IRecipient<SendRequestMessage>.Receive(SendRequestMessage message)
		{
			SendRequestNowCommand.Execute(this);
		}
	}

	internal enum MenuItems
	{
		FileClose,
		ServerNew,
		RequestNew,
		RequestSendNow,
	}

	internal class MenuItemClickedEventArgs : EventArgs
	{
		public MenuItems MenuItem { get; set; }
	}
}
