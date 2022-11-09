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
            var mainWindow = (_serviceProvider.GetService(typeof(MainWindow)) as MainWindow);
            if (mainWindow != null)
                Application.Run(mainWindow.Toplevel);

            Application.Shutdown();

            await Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
