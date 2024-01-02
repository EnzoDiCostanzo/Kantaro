using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;

namespace Enzo.Music.KantaroWpf.Converters;

public partial class StrofeFlowDocumentConverter : IValueConverter
{

    private const string LinkPrefix = "kanto://accordo#";

    private string LinkFormat
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append(LinkPrefix);
            sb.Append("{0}");
            return sb.ToString();
        }
    }

    public static string? GetAccordoFromUri(Uri navigateUri)
    {
        if (navigateUri is null)
            return null;
        // Verifica la corretta valorizzazione dell'uri passato come parametro
        if (!navigateUri.AbsolutePath.StartsWith(LinkPrefix))
        {
            throw new InvalidCastException("Unrecognized uri format");
        }
        return navigateUri.AbsolutePath.Remove(0, LinkPrefix.Length);
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null || !(value is IList<Strofa>))
            return DependencyProperty.UnsetValue;
        if (targetType != typeof(FlowDocument))
        {
            throw new InvalidOperationException("Il tipo di destinazione deve essere un FlowDocument");
        }
        var doc = new FlowDocument();
        IList<Strofa> strofe = (IList<Strofa>)value;
        foreach (var strofa in strofe)
        {
            var b = new Paragraph();
            if (strofa.Nome is not null)
            {
                b.Tag = strofa.Nome;
                b.FontStyle = FontStyles.Italic;
            }
            if (strofa is StrofaRipetuta)
            {
                b.Padding = new Thickness(10, 5, 0, 5);
                b.FontWeight = FontWeights.Bold;
                b.FontStyle = FontStyles.Italic;
                var lnk = new Hyperlink(new Run(((StrofaRipetuta)strofa).Riferimento));
                b.Inlines.Add(lnk);
            }
            else
            {
                foreach (var p in strofa.Parti)
                {
                    Inline tb;
                    if (p is not null)
                    {
                        if (p.Accordo is null)
                        {
                            tb = new Run(p.Testo);
                        }
                        else
                        {
                            tb = new Hyperlink(new Run(p.Testo));
                            ((Hyperlink)tb).NavigateUri = new Uri(string.Format(LinkFormat, p.Accordo.ToString()));
                        }
                    }
                    else
                    {
                        tb = new LineBreak();
                    }
                    b.Inlines.Add(tb);
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
