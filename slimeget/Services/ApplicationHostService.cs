using Microsoft.Extensions.Hosting;
using slimeget.Views;
using Terminal.Gui;

namespace slimeget.Services
{
    internal class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            Application.Init();
            var toplevel = (_serviceProvider.GetService(typeof(ToplevelView)) as ToplevelView);
            if (toplevel != null)
                Application.Run(toplevel);

            Application.Shutdown();

            await Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
