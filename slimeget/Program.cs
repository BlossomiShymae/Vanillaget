using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using slimeget.Services;
using slimeget.Views;
using slimeget.Views.Subviews;

IHost host = Host
    .CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<ApplicationHostService>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<ServerFrameView>();
    })
    .Build();

await host.StartAsync();