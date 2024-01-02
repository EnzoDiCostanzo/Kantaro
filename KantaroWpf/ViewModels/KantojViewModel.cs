using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enzo.Music.KantaroWpf.Services;
using Enzo.Music.KantaroWpf.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Enzo.Music.KantaroWpf.ViewModels;

public partial class KantojViewModel : ObservableRecipient
{

    IDataService dataService;
    private readonly INavigationService navigationService;

    public KantojViewModel(IDataService dataService, INavigationService navigationService)
    {
        this.dataService = dataService;
        this.navigationService = navigationService;
        AllFolderElements = new List<FileElement>();
        folderPath = string.Empty;
        FolderPath = dataService.GetCurrentFolderAsync().Result.ToString();
        FolderElements = CollectionViewSource.GetDefaultView(AllFolderElements);
    }

    #region Proprietà di input e output
    [ObservableProperty]
    [DefaultValue(false)]
    private bool isWorking = false;

    private string folderPath;
    public string FolderPath
    {
        get => folderPath;
        set
        {
            if (!string.Equals(value, folderPath))
            {
                SetProperty(ref folderPath, value);
                OnFolderPathChanged();
            }
        }
    }

    private async void OnFolderPathChanged()
    {
        await LoadFromFolderAsync();
    }

    private FileElement? selectedFileElement;
    public FileElement? SelectedFileElement
    {
        get => selectedFileElement;
        set
        {
            SetProperty(ref selectedFileElement, value);
            OnSelectedFileElementChanged();
        }
    }

    private void OnSelectedFileElementChanged()
    {
        if (SelectedFileElement == null || SelectedFileElement.FilePath == null) return;
        var fp = SelectedFileElement.FilePath;
        if (SelectedFileElement.IsPreviousFolder)
            FolderPath = System.IO.Path.GetDirectoryName(FolderPath) ?? string.Empty;
        else if (SelectedFileElement.IsContainer)
            FolderPath = fp;
        else if (SelectedFileElement.Exists && !SelectedFileElement.HasErrors)
        {
            // File .kanto da aprire
            var cvm = new KantoViewModel(dataService);
            cvm.FilePath = fp;
            if (cvm.IsLoaded) ((NavigationService)navigationService).NavigateToSong(cvm);
        }
    }

    private List<FileElement> AllFolderElements;

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
        if (FolderElements == null) return;
        FolderElements.Filter = (e) => ((FileElement) e).Title?.ToLower().Contains(Filter.ToLower()) ?? false; //IsVisible
        FolderElements.Refresh();
    }
    #endregion

    #region Comandi

    [RelayCommand]
    public async Task Refresh()
    {
        if (FolderPath == null) return;
        await dataService.CreateIndexAsync(FolderPath);
        await LoadFromFolderAsync();
    }
    #endregion

    public async Task LoadFromFolderAsync()
    {
        if (FolderPath == null) return;
        bool saveIsWorking = IsWorking;
        try
        {
            IsWorking = true;
            AllFolderElements.Clear();
            var elements = await dataService.GetFileElementsFromFolderAsync(FolderPath);
            AllFolderElements.AddRange(elements);
            // Aggiorna la vista in caso di filtri attivi
            Filter = Filter;
        }
        finally
        {
            Task.WaitAll();
            IsWorking = saveIsWorking;
        }
    }
}
