using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music.KantaroWpf.Services;

public interface INavigationService
{
    ObservableRecipient CurrentView { get; }
    void NavigateTo<T>() where T : ObservableRecipient;
}
