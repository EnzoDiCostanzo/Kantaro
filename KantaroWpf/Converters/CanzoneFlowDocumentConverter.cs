using Enzo.Music;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Enzo.Music.KantaroWpf.Converters;

class CanzoneFlowDocumentConverter : IValueConverter
{
    #region Proprietà per la rappresentazione grafica del documento
    public int TitoloFontSize = 14;
    public FontStyle TitoloFontStyle = FontStyles.Normal;
    public FontWeight TitoloFontWeight = FontWeights.Bold;
    public int AutoreFontSize = 12;
    public FontStyle AutoreFontStyle = FontStyles.Italic;
    public FontWeight AutoreFontWeight = FontWeights.Regular;
    #endregion
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null || value is not Canzone) return DependencyProperty.UnsetValue;
        if (targetType != typeof(FlowDocument))
            throw new InvalidOperationException("Il tipo di destinazione deve essere un FlowDocument");
        var strofeDict = new Dictionary<string, Strofa>();

        var doc = new FlowDocument();
        Canzone song = (Canzone)value;
        if (!string.IsNullOrWhiteSpace(song.Titolo))
        {
            var par = new Paragraph()
            {
                FontSize = TitoloFontSize,
                FontStyle = TitoloFontStyle,
                FontWeight = TitoloFontWeight
            };
            par.Inlines.Add(song.Titolo);
            doc.Blocks.Add(par);
        }
        if (!string.IsNullOrWhiteSpace(song.Autore))
        {
            var par = new Paragraph()
            {
                FontSize = AutoreFontSize,
                FontStyle = AutoreFontStyle,
                FontWeight = AutoreFontWeight
            };
            par.Inlines.Add(song.Autore);
            doc.Blocks.Add(par);
        }
        var noteTrovate = (from strofa in song.GetStrofe()
                           from p in strofa.Parti
                           where p?.Accordo is not null
                           select p.Accordo?.Scala.NotaFondamentale).ToArray();
        var noteNaturaliTrovate = (from n in noteTrovate where n.IsNaturale select n.Valore).Distinct().ToArray();
        var altreNoteTrovate = (from n in noteTrovate where !n.IsNaturale select n.Valore).Distinct().ToArray();
        var noteBemolli = new Dictionary<NotaEnum, bool>();
        foreach (var nota in altreNoteTrovate)
            noteBemolli.Add(nota, false);
        bool trovatoBemolle;
        do
        {
            trovatoBemolle = false;
            foreach (var nota in altreNoteTrovate)
            {
                Nota nuovaNota = new Nota(nota);
                var existItsNat = noteNaturaliTrovate.Contains((nuovaNota - Semitono.Value).Valore) ||
                                  noteBemolli.ContainsKey((nuovaNota - Tono.Value).Valore) &&
                                  noteBemolli[(nuovaNota - Tono.Value).Valore];
                if (!nuovaNota.IsNaturale && existItsNat && !noteBemolli[nota])
                {
                    noteBemolli[nota] = true;
                    trovatoBemolle = true;
                }
            }
        } while (trovatoBemolle);
        foreach (var strofa in song.GetStrofe())
        {
            Paragraph b = new Paragraph();
            if (!string.IsNullOrWhiteSpace(strofa.Nome) && !strofeDict.ContainsKey(strofa.Nome))
                strofeDict.Add(strofa.Nome, strofa);
            Strofa strofaDaConvertire;
            if (strofa is StrofaRipetuta)
                strofaDaConvertire = strofeDict[((StrofaRipetuta)strofa).Riferimento];
            else
                strofaDaConvertire = strofa;
            foreach (var p in strofaDaConvertire.Parti)
            {
                if (p is not null)
                {
                    string testoAccordo;
                    if (p.Accordo is null)
                    {
                        testoAccordo = string.Empty;
                    }
                    else
                    {
                        var nuovaNota = p.Accordo.Scala.NotaFondamentale;
                        var bemolle = noteBemolli.ContainsKey(nuovaNota.Valore) && noteBemolli[nuovaNota.Valore];
                        var partiConAccordiSimili = from parte in strofaDaConvertire.Parti
                                                where parte?.Accordo?.Scala?.NotaFondamentale == nuovaNota
                                                select parte;
                        if (bemolle && partiConAccordiSimili.FirstOrDefault() is not null)
                            bemolle = false;
                        testoAccordo = p.Accordo.ToString(bemolle);
                    }
                    TextBlock tb = new TextBlock();
                    tb.Inlines.Add($"{testoAccordo}\r\n{p.Testo}");
                    b.Inlines.Add(tb);
                }
                else
                {
                    b.Inlines.Add("\r\n");
                }
            }
            doc.Blocks.Add(b);
        }
        return doc;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
