using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enzo.Music.KantaroWpf.Models;
using Enzo.Music.KantaroWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Enzo.Music.KantaroWpf.ViewModels;

public partial class MainWindowViewModel : ObservableRecipient
{
    private readonly IDataService dataService;


    public MainWindowViewModel(IDataService dataService)
    {
        this.dataService = dataService;
        AllFolderElements = [];
        FilteredFolderElements = [];
        folderPath = string.Empty;
        FolderPath = dataService.GetCurrentFolderAsync().Result.ToString();
        FolderElements = CollectionViewSource.GetDefaultView(FilteredFolderElements);
        selectedFileElement = FileElement.Empty;
        SelectedFileElement = FileElement.Empty;
    }

    public string WindowTitle => $"Kantaro - {Canzone?.Titolo ?? FolderPath}";

    #region Proprietà legate all'elenco di canzoni/files
    [ObservableProperty]
    [DefaultValue(false)]
    private bool isWorking = false;

    private string folderPath;
    public string FolderPath
    {
        get => folderPath;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            if (!string.Equals(value, folderPath))
            {
                SetProperty(ref folderPath, value);
                OnFolderPathChanged();
                OnPropertyChanged(nameof(WindowTitle));
                OnPropertyChanged(nameof(CanGoPrevious));
            }
        }
    }

    private async void OnFolderPathChanged()
    {
        await LoadFromFolderAsync();
        Canzone = null;
    }

    private FileElement selectedFileElement;
    public FileElement SelectedFileElement
    {
        get => selectedFileElement;
        set
        {
            if (SetProperty(ref selectedFileElement, value))
                _ = OnSelectedFileElementChangedAsync();
        }
    }

    public bool CanGoPrevious => !ShowSong && ((SelectedFileElement?.IsContainer ?? false) || AllFolderElements?.FirstOrDefault(f => f.IsPreviousFolder) is not null);

    [RelayCommand]
    public void GoPrevious()
    {
        if (FolderPath is null) return;
        FolderPath = Path.GetDirectoryName(FolderPath) ?? string.Empty;
        SelectedFileElement = FileElement.Empty;
    }

    private async Task OnSelectedFileElementChangedAsync()
    {
        if (SelectedFileElement is null || SelectedFileElement.FilePath is null)
        {
            Canzone = null;
            return;
        }

        var fp = SelectedFileElement.FilePath;
        if (SelectedFileElement.IsContainer)
            FolderPath = fp;
        else if (SelectedFileElement.Exists && !SelectedFileElement.HasErrors)
        {
            Canzone = await dataService.GetCanzoneFromFilePathAsync(fp);
            VariazioneInSemitoni = Canzone?.VariazioneInSemitoni ?? 0;
        }
    }

    private readonly List<FileElement> AllFolderElements;
    public readonly ObservableCollection<FileElement> FilteredFolderElements;

    [ObservableProperty]
    private ICollectionView folderElements;

    private string? filter;
    public string Filter
    {
        get => filter ?? string.Empty;
        set
        {
            SetProperty(ref filter, value);
            OnFilterChanged();
        }
    }
    private void OnFilterChanged()
    {
        var filterValue = Filter;
        FilteredFolderElements.Clear();
        if (string.IsNullOrEmpty(filterValue))
            foreach (var f in AllFolderElements.Where(f => !f.IsPreviousFolder))
            {
                FilteredFolderElements.Add(f);
            }
        else
            foreach (var f in AllFolderElements.Where(f =>
            {
                var elem = (FileElement)f;
                return !elem.IsPreviousFolder && (elem.Title?.Contains(filterValue, StringComparison.CurrentCultureIgnoreCase) ?? false);
            }))
            {
                FilteredFolderElements.Add(f);
            };
    }

    [RelayCommand]
    public async Task Refresh()
    {
        if (Canzone is not null)
        {
            await OnSelectedFileElementChangedAsync();
        }
        else
        {
            if (FolderPath is null) return;
            await dataService.CreateIndexAsync(FolderPath);
            await LoadFromFolderAsync();
        }
    }

    private async Task LoadFromFolderAsync()
    {
        if (string.IsNullOrWhiteSpace(FolderPath)) return;
        bool saveIsWorking = IsWorking;
        try
        {
            IsWorking = true;
            AllFolderElements.Clear();
            var elements = await dataService.GetFileElementsFromFolderAsync(FolderPath);
            AllFolderElements.AddRange(elements);
            // Aggiorna la vista in caso di filtri attivi
            OnFilterChanged();
        }
        finally
        {
            Task.WaitAll();
            OnPropertyChanged(nameof(CanGoPrevious));
            IsWorking = saveIsWorking;
        }
    }
    #endregion

    #region Proprietà legati alla singola canzone

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VariazioneInSemitoni))]
    [NotifyPropertyChangedFor(nameof(ShowSong))]
    [NotifyPropertyChangedFor(nameof(ShowFiles))]
    [NotifyPropertyChangedFor(nameof(WindowTitle))]
    private Canzone? canzone;

    public bool ShowFiles => Canzone is null;
    public bool ShowSong => Canzone is not null;

    public int VariazioneInSemitoni
    {
        get => Canzone?.VariazioneInSemitoni ?? 0;
        set
        {
            if (Canzone is not null)
            {
                Canzone.VariazioneInSemitoni = value;
                OnPropertyChanged(nameof(Canzone));
                OnPropertyChanged();
            }
        }
    }

    [ObservableProperty]
    private double preferredZoom;

    [RelayCommand]
    public void CloseSong()
    {
        if (Canzone is not null) Canzone = null;
    }
    #endregion
}
