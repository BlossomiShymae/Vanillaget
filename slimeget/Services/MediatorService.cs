using Microsoft.Extensions.DependencyInjection;
using slimeget.ViewModels;
using System.Diagnostics;

namespace slimeget.Services
{

	internal interface IMediatorService
	{
		void Mediate(ApplicationState state);
	}

	internal interface IMediatorModule
	{
		void Resolve(ApplicationState state);
	}

	internal class MediatorService : IMediatorService
	{
		private readonly IServiceProvider _services;

		private Lazy<IMediatorModule> _toplevelModule;

		private Lazy<IMediatorModule> _serverModule;

		private Lazy<IMediatorModule> _responseModule;

		private Lazy<IMediatorModule> _requestModule;

		public MediatorService(IServiceProvider services)
		{
			_services = services;
			_toplevelModule = new Lazy<IMediatorModule>(GetService<ToplevelViewModel>());
			_serverModule = new Lazy<IMediatorModule>(GetService<ServerFrameViewModel>());
			_responseModule = new Lazy<IMediatorModule>(GetService<ResponseFrameViewModel>());
			_requestModule = new Lazy<IMediatorModule>(GetService<RequestFrameViewModel>());
		}

		public void Mediate(ApplicationState state)
		{
			TryResolve<ToplevelViewModel>(_toplevelModule, state);
			TryResolve<ServerFrameViewModel>(_serverModule, state);
			TryResolve<ResponseFrameViewModel>(_responseModule, state);
			TryResolve<RequestFrameViewModel>(_requestModule, state);
		}

		private Func<T> GetService<T>()
		{
			return () => _services.GetRequiredService<T>();
		}

		private static void TryResolve<T>(Lazy<IMediatorModule> module, ApplicationState state)
		{
			try
			{
				module.Value.Resolve(state);
			}
			catch (InvalidOperationException) { Trace.WriteLine($"Failed to resolve module: {typeof(T)}"); }
		}
	}
}
