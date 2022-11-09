using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using slimeget.Services;
using slimeget.ViewModels;
using slimeget.Views;
using slimeget.Views.Subviews;

IHost host = Host
    .CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<ApplicationHostService>();

        services.AddSingleton<ToplevelView>();
        services.AddSingleton<ToplevelViewModel>();
        services.AddSingleton<ResponseFrameView>();
        services.AddSingleton<ResponseFrameViewModel>();
        services.AddSingleton<RequestFrameView>();
        services.AddSingleton<RequestFrameViewModel>();
        services.AddSingleton<ServerFrameView>();
        services.AddSingleton<ServerFrameViewModel>();
    })
    .Build();

await host.StartAsync();