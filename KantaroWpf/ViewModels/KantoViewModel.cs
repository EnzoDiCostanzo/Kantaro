using CommunityToolkit.Mvvm.ComponentModel;
using Enzo.Music.KantaroWpf.Services;
using Enzo.Music.KantaroWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Enzo.Music.KantaroWpf.ViewModels;

public partial class KantoViewModel : ObservableRecipient
{
    private IDataService dataService;

    public KantoViewModel(IDataService dataService)
    {
        this.dataService = dataService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VariazioneInSemitoni))]
    [NotifyPropertyChangedFor(nameof(IsLoaded))]
    private Canzone? canzone;

    private string? filePath;
    public string? FilePath
    {
        get => filePath;
        set
        {
            SetProperty(ref filePath, value);
            Task.Run(async () =>
            {
                await OnFilePathChangedAsync();
                return true;
            });
        }
    }

    private async Task OnFilePathChangedAsync()
    {
        if (FilePath == null)
        {
            Canzone = null;
            VariazioneInSemitoni = 0;
        }
        else
        {
            Canzone = await dataService.GetCanzoneFromFilePathAsync(FilePath);
            VariazioneInSemitoni = Canzone.VariazioneInSemitoni;
        }

    }

    public int VariazioneInSemitoni
    {
        get => Canzone?.VariazioneInSemitoni ?? 0;
        set
        {
            if(Canzone != null)
            {
                Canzone.VariazioneInSemitoni = value;
                OnPropertyChanged("Canzone");
                OnPropertyChanged();
            }
        }
    }

    [ObservableProperty]
    private double preferredZoom;

    public bool IsLoaded => Canzone != null;

    [RelayCommand]
    public void Close()
    {
        if(Canzone!=null) Canzone = null;
    }
}
