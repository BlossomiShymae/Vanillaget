using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using slimeget.Services;
using slimeget.Views;

IHost host = Host
    .CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<ApplicationHostService>();
        services.AddSingleton<MainWindow>();
    })
    .Build();

await host.StartAsync();