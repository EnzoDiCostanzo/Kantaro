using CommunityToolkit.Mvvm.ComponentModel;
using Enzo.Music.KantaroWpf.Services;
using Enzo.Music.KantaroWpf.ViewModels;
using Enzo.Music.KantaroWpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Enzo.Music.KantaroWpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
partial class App : Application
{

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public static new App Current => (App)Application.Current;

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = new AppServices().Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
