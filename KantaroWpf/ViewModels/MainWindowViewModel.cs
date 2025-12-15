using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enzo.Music.KantaroWpf.Converters;
using Enzo.Music.KantaroWpf.Models;
using Enzo.Music.KantaroWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml.Linq;

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

    private FileElement? selectedFileElement;
    public FileElement? SelectedFileElement
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
            }
        ;
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

    [RelayCommand]
    public async Task PrintFilteredSongsAsync()
    {
        if (FilteredFolderElements.Count == 0) return;
        FlowDocument document = new FlowDocument();
        var cfdConv = new CanzoneFlowDocumentConverter();
        var description = (Path.GetExtension(FolderPath) == ".kantoj") ? $"Kantoj - {Path.GetFileNameWithoutExtension(FolderPath)}" : SelectedFileElement?.FileName ?? "Folder";
        foreach (var fe in FilteredFolderElements.Where(f => f.Exists && !f.HasErrors && !f.IsContainer))
        {
            var canzone = await dataService.GetCanzoneFromFilePathAsync(fe.FilePath!);
            if (canzone is not null)
            {
                document.Blocks.Add(cfdConv.GetSection(canzone));
            }
        }
        PrintFlowDocument(document, description);
    }
    private void PrintFlowDocument(FlowDocument document, string description)
    {
        var pd = new PrintDialog() { };
        if (pd.ShowDialog() == true)
        {
            var printDoc = CloneFlowDocument(document);
            printDoc.PageHeight = pd.PrintableAreaHeight;
            printDoc.PageWidth = pd.PrintableAreaWidth;
            IDocumentPaginatorSource idpSource = printDoc;
            pd.PrintDocument(idpSource.DocumentPaginator, description);
        }
    }
    private FlowDocument CloneFlowDocument(FlowDocument original)
    {
        string xaml = System.Windows.Markup.XamlWriter.Save(original);
        using (var stringReader = new System.IO.StringReader(xaml))
        using (var xmlReader = System.Xml.XmlReader.Create(stringReader))
        {
            return (FlowDocument)System.Windows.Markup.XamlReader.Load(xmlReader);
        }
    }
    #endregion

    #region Proprietà legati alla singola canzone

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VariazioneInSemitoni))]
    [NotifyPropertyChangedFor(nameof(ShowSong))]
    [NotifyPropertyChangedFor(nameof(ShowFiles))]
    [NotifyPropertyChangedFor(nameof(WindowTitle))]
    [NotifyPropertyChangedFor(nameof(CanGoPrevious))]
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
        if (SelectedFileElement is not null) SelectedFileElement = null;
        if (Canzone is not null) Canzone = null;
    }

    [RelayCommand]
    public void PrintSong()
    {
        if (Canzone is null) return;
        PrintSong(Canzone);
    }

    private void PrintSong(Canzone canzone)
    {
        var cfdConv = new CanzoneFlowDocumentConverter();
        var d = (FlowDocument)cfdConv.Convert(canzone, new FlowDocument().GetType(), string.Empty, System.Globalization.CultureInfo.CurrentCulture);
        PrintFlowDocument(d, $"Kanto {canzone.Titolo}");
    }
    #endregion
}
