using Enzo.Music.KantaroWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Ink;

namespace Enzo.Music.KantaroWpf.Converters;

internal class StrofeTextConverter : IValueConverter
{
    private const string StrofaSeparator = "§";
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value is not List<Strofa> strofe)
            throw new ArgumentException("Expected a List<Strofa>", nameof(value));
        var sb = new StringBuilder();
        bool first = true;
        foreach (var strofa in strofe)
        {
            if (!first)
                sb.AppendLine(StrofaSeparator);
            first = false;
            foreach (var parte in strofa.Parti)
            {
                if (parte is null) continue;
                if (parte.Accordo is not null)
                    sb.Append($"[{parte.Accordo}] ");
                sb.AppendLine(parte.Testo);
            }
        }
        return sb.ToString().TrimEnd();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value is not string testoCanzone)
            throw new ArgumentException("Expected a string", nameof(value));
        var strofeTesto = testoCanzone.Split([StrofaSeparator], StringSplitOptions.None);
        List<Strofa> strofe = new();
        bool primaStrofa = true;
        foreach (var strofaTesto in strofeTesto)
        {
            bool primaRiga = true;
            var strofa = new Strofa();
            if (primaStrofa && testoCanzone.StartsWith(StrofaSeparator))
            {
                primaStrofa = false;
                continue; // lo split ha generato una strofaTesto non valida come primo elemento.
            }
            strofe.Add(strofa);
            var righe = strofaTesto.Replace("\r", "").Split('\n');
            if (righe.Length > 0 && string.IsNullOrEmpty(righe.Last()))
                righe = righe.SkipLast(1).ToArray();
            foreach (var riga in righe)
            {
                if (primaStrofa && primaRiga && testoCanzone.StartsWith(StrofaSeparator))
                {
                    strofa.Nome = riga;
                }
                else if (!primaStrofa && primaRiga)
                {
                    strofa.Nome = riga;
                }
                if (primaRiga)
                {
                    primaRiga = false;
                    continue;
                }
                if (string.IsNullOrWhiteSpace(riga)) continue;
                var testiParti = new List<string>();

                // Split della linea in base agli accordi: [Accordo] Testo [Accordo 2] Testo, come 2 parti "[Accordo1] Testo" e "[Accordo2] Testo"
                Regex re = new Regex("\\[[A-Za-z1-9-]+\\]");
                var matches = re.Matches(riga);
                if (matches.Count > 0)
                {
                    foreach (Match m in matches)
                    {
                        if (testiParti.Count == 0 && m.Index > 0)
                        {
                            testiParti.Add(riga.Substring(0, m.Index));
                        }
                        int posAccordoSucc = riga.IndexOf('[', m.Index + 1);
                        testiParti.Add(riga.Substring(m.Index, posAccordoSucc > 0 ? posAccordoSucc - m.Index : riga.Length - m.Index));
                    }
                }
                else
                {
                    testiParti.Add(riga);
                }
                foreach (var testoParte in testiParti)
                {
                    // Parsing accordo inline: [Accordo] Testo"
                    string testo = testoParte;
                    string? accordoTxt = null;
                    if (testo.StartsWith("[") && testo.Contains(']'))
                    {
                        int idx = testo.IndexOf(']');
                        accordoTxt = testo.Substring(1, idx - 1);
                        testo = testo[(idx + 1)..];
                    }
                    Accordo? accordo;
                    if (accordoTxt is null || !Accordo.TryParse(accordoTxt, out accordo))
                    {
                        accordo = null;
                    }
                    strofa.Parti.Add(new Parte { Testo = testo, Accordo = accordo });
                }
            }
            primaStrofa = false;
        }
        return strofe;
    }
}
