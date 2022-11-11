using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using slimeget.Models;
using slimeget.Services;
using slimeget.ViewModels;
using slimeget.Views;
using slimeget.Views.Subviews;

// Use dependency injection
IHost host = Host
    .CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        // Services
        services.AddHostedService<ApplicationHostService>();
        services.AddSingleton<WizardFactoryService>();
        services.AddSingleton<RepositoryService<RequestMethodCollection>>();

        // Views and ViewModels
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

// Start slimeget TUI
await host.StartAsync();