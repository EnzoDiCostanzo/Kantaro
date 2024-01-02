using CommunityToolkit.Mvvm.ComponentModel;
using Enzo.Music.KantaroWpf.Services;
using Enzo.Music.KantaroWpf.ViewModels;
using Enzo.Music.KantaroWpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Enzo.Music.KantaroWpf;

public partial class AppServices : ObservableObject
{
    [ObservableProperty]
    private IServiceProvider services;

    public AppServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainWindow>();
        // provider=>new MainWindow
        //{
        //    DataContext = provider.GetRequiredService<MainWindowViewModel>()
        //});
        //services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<IDataService, FileSystemDataService>();
        services.AddSingleton<MainWindowViewModel>();
        //services.AddSingleton<KantojViewModel>();
        //services.AddScoped<KantoViewModel>();

        services.AddSingleton<Func<Type, ObservableRecipient>>(serviceProvider => viewModelType => (ObservableRecipient)serviceProvider.GetRequiredService(viewModelType));

        Services = services.BuildServiceProvider();
    }

    #region ViewModels esposti
    public MainWindowViewModel MainWindowViewModel => Services.GetRequiredService<MainWindowViewModel>();
    //public KantojViewModel KantojViewModel => Services.GetRequiredService<KantojViewModel>();
    //public KantoViewModel KantoViewModel => Services.GetRequiredService<KantoViewModel>();
    #endregion
}
