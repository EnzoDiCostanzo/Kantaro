using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enzo.Music;
using Enzo.Music.KantaroWpf.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Documents;
using System.Windows.Input;

namespace Enzo.Music.KantaroWpf.ViewModels;

public partial class ParteViewModel : ObservableObject
{
    [ObservableProperty]
    private string? testo;

    [ObservableProperty]
    private string? accordo;
}

public partial class StrofaViewModel : ObservableObject
{
    [ObservableProperty]
    private string? nome;

    public ObservableCollection<ParteViewModel> Parti { get; } = new();
}

public partial class EditSongViewModel : ObservableObject
{
    [ObservableProperty]
    private string? nomeFile;

    [ObservableProperty]
    private string? titolo;

    [ObservableProperty]
    private string? autore;

    [ObservableProperty]
    private string? testoCanzone;

    public ObservableCollection<StrofaViewModel> Strofe { get; } = new();

    public ICommand AnalizzaTestoCommand { get; }
    public ICommand SalvaCanzoneCommand { get; }

    public event System.Action<Canzone, string>? CanzoneSalvata;

    public EditSongViewModel()
    {
        AnalizzaTestoCommand = new RelayCommand(AnalizzaTesto);
        SalvaCanzoneCommand = new RelayCommand(SalvaCanzone, PuoiSalvare);
    }

    public void SetCanzone(Canzone canzone, string nomeFile)
    {
        NomeFile = nomeFile;
        Titolo = canzone.Titolo;
        Autore = canzone.Autore;
        var conv = new StrofeTextConverter();
        TestoCanzone = (string)conv.Convert(canzone.Strofe, typeof(string), new object(), CultureInfo.InvariantCulture);
        AnalizzaTesto();
    }

    private void AnalizzaTesto()
    {
        Strofe.Clear();
        if (string.IsNullOrWhiteSpace(TestoCanzone))
            return;

        var conv = new StrofeTextConverter();
        var strofe = (List<Strofa>)conv.ConvertBack(TestoCanzone!, TestoCanzone.GetType(), new object(), CultureInfo.InvariantCulture);
        foreach (Strofa s in strofe)
        {
            var strofaVm = new StrofaViewModel();
            if (!string.IsNullOrEmpty(s.Nome)) strofaVm.Nome = s.Nome;
            foreach (var p in s.Parti)
            {
                strofaVm.Parti.Add(new ParteViewModel() { Testo = p!.Testo, Accordo = p.Accordo?.ToString() });
            }
            Strofe.Add(strofaVm);
        }
    }

    private bool PuoiSalvare() => !string.IsNullOrWhiteSpace(NomeFile) && !string.IsNullOrWhiteSpace(Titolo) && Strofe.Count > 0;

    private void SalvaCanzone()
    {
        var canzone = new Canzone
        {
            Titolo = Titolo,
            Autore = Autore
        };
        foreach (var strofaVm in Strofe)
        {
            var strofa = new Strofa
            {
                Nome = strofaVm.Nome
            };
            foreach (var parteVm in strofaVm.Parti)
            {
                Accordo? accordoObj = null;
                if (!string.IsNullOrWhiteSpace(parteVm.Accordo))
                {
                    Accordo.TryParse(parteVm.Accordo, out accordoObj);
                }
                strofa.Parti.Add(new Parte
                {
                    Testo = parteVm.Testo,
                    Accordo = accordoObj
                });
            }
            canzone.Strofe.Add(strofa);
        }
        CanzoneSalvata?.Invoke(canzone, NomeFile!);
    }
}