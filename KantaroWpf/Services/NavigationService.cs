using CommunityToolkit.Mvvm.ComponentModel;
using Enzo.Music.KantaroWpf.Models;
using Enzo.Music.KantaroWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music.KantaroWpf.Services;

class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ObservableRecipient> _viewModelFactory;
    private ObservableRecipient? _currentView;

    public ObservableRecipient CurrentView
    {
        get => _currentView;
        private set
        {
            SetProperty(ref _currentView, value);
        }
    }

    //ObservableRecipient INavigationService.CurrentView => throw new NotImplementedException();

    public void NavigateTo<T>() where T : ObservableRecipient
    {
        ObservableRecipient vm = _viewModelFactory.Invoke(typeof(T));
        CurrentView = vm;
    }

    public void NavigateToSong(KantoViewModel kantoViewModel) => CurrentView = kantoViewModel;

    public NavigationService(Func<Type, ObservableRecipient> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        _currentView = null;
    }
}
