using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfUI.ViewModels;

namespace WpfUI;

public partial class App : Application
{
    private IHost? _appHost;
    private IServiceScope? _appScope;

    public App()
    {
        try
        {
            _appHost = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<MainViewModel>();
                    services.AddScoped(sp => new MainWindow(sp.GetRequiredService<MainViewModel>()));
                })
                .Build();
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Console.WriteLine($"Unexpected error: {ex.Message}");
            Application.Current.Shutdown();
        }
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        if (_appHost is null)
        {
            Application.Current.Shutdown();
            base.OnStartup(e);
            return;
        }

        await _appHost.StartAsync();
        _appScope = _appHost.Services.CreateScope();
        MainWindow mainWindow = _appScope.ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            _appScope?.Dispose();
            _appScope = null;

            if (_appHost != null)
            {
                await _appHost.StopAsync();
                _appHost.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        finally
        {
            base.OnExit(e);
        }
    }
}
