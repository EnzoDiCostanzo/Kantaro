using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Enzo.Music;
public class Canzone : IEquatable<Canzone>, IEqualityOperators<Canzone, Canzone, bool>
{
    public int VariazioneInSemitoni { get; set; }
    public object? Tag { get; set; }
    public string? Titolo { get; set; }
    public string? Autore { get; set; }

    public readonly List<Strofa> Strofe = [];

    /// <summary>
    /// Descrizione della canzone (costituita dal titolo + l'autore se presente)
    /// </summary>
    public string Description
    {
        get
        {
            StringBuilder sb = new();
            if (Titolo != null) sb.Append(Titolo);
            if (!string.IsNullOrWhiteSpace(Autore))
                sb.Append($" ({Autore})");
            return sb.ToString();
        }
    }

    public static async Task<Canzone?> FromStreamAsync(StreamReader stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        return FromXDocument(await LoadXDocumentAsync(stream).ConfigureAwait(false));
    }

    public static async Task<XDocument?> LoadXDocumentAsync(StreamReader stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        var t = new Task<XDocument?>(() =>
        {
            try
            {
                return XDocument.Load(stream);
            }
            catch// (Exception ex)
            {
                return null;
            }
        });
        t.Start();
        return await t.ConfigureAwait(false);
    }

    public static Canzone? FromXDocument(XDocument? document)
    {
        if (document is null) return null;
        try
        {
            Canzone song = new()
            {
                Titolo = document.Element("Canzone")?.Attribute("Titolo")?.Value ?? string.Empty,
                Autore = document.Element("Canzone")?.Attribute("Autore")?.Value ?? string.Empty
            };
            if (int.TryParse(document.Element("Canzone")?.Attribute("variazione")?.Value, out int variazione))
                song.VariazioneInSemitoni = variazione;
            foreach (XElement st in document.Element("canzone")?.Elements("strofa") ?? [])
            {
                if (!string.IsNullOrWhiteSpace(st.Attribute("ref")?.Value))
                {
                    song.Strofe.Add(new StrofaRipetuta(st.Attribute("ref")?.Value ?? string.Empty));
                    continue;
                }
                Strofa strofa = new();
                foreach (XElement elem in st.Elements())
                {
                    Parte? p = null;
                    if (elem.Name == "parte")
                    {
                        p = new Parte();
                        if (!string.IsNullOrWhiteSpace(elem.Attribute("accordo")?.Value))
                            p.Accordo = (Accordo?)(elem.Attribute("accordo")?.Value ?? string.Empty);
                        if (!elem.IsEmpty) p.Testo = elem.Value;
                    }
                    strofa.Parti.Add(p);
                }
                if (!string.IsNullOrWhiteSpace(st.Attribute("name")?.Value))
                    strofa.Nome = st.Attribute("name")?.Value;
                song.Strofe.Add(strofa);
            }
            return song;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex) { Source = document.ToString() };
        }
    }

    public XDocument ToXml()
    {
        XDocument d = new();
        XElement root = new("canzone");
        if (Titolo != null) root.Add(new XAttribute("title", Titolo));
        if (Autore != null) root.Add(new XAttribute("autore", Autore));
        if (VariazioneInSemitoni != 0) root.Add(new XAttribute("variazione", VariazioneInSemitoni));
        foreach (var st in Strofe)
        {
            XElement strofa = new("strofa");
            if (st is StrofaRipetuta stRip)
                strofa.Add(new XAttribute("ref", stRip.Riferimento));
            else
            {
                foreach (var p in st.Parti)
                {
                    if (p is null)
                        strofa.Add(new XElement("br"));
                    else
                    {
                        XElement parte = new("parte");
                        if (p.Accordo != null)
                            parte.Add(new XAttribute("accordo", p.Accordo.ToString()));
                        if (p.Testo != null)
                            parte.Value = p.Testo;
                        strofa.Add(parte);
                    }
                }
            }
            root.Add(strofa);
        }
        d.Add(root);
        return d;
    }

    public IList<Strofa> GetStrofe()
    {
        List<Strofa> ret = [];
        foreach (var s in Strofe)
        {
            Strofa n = (Strofa)s.Clone();
            if (VariazioneInSemitoni != 0)
            {
                Distanza dist = new() { Semitoni = VariazioneInSemitoni };
                foreach (var p in n.Parti)
                {
                    if (p is null) continue;
                    if (p.Accordo != null)
                        p.Accordo += dist;
                }
            }
            ret.Add(n);
        }
        return ret;
    }

    public bool Equals(Canzone? other)
    {
        if (other is null) return false;
        var uguali = string.Equals(Autore, other.Autore) &&
                     string.Equals(Titolo, other.Titolo) &&
                     VariazioneInSemitoni == other.VariazioneInSemitoni;
        if (uguali) uguali = Strofe.Count == other.Strofe.Count;
        if (uguali)
        {
            for (var i = 0; i < Strofe.Count; i++)
            {
                uguali = Strofe[i].Equals(other.Strofe[i]);
                if (!uguali) break;
            }
        }
        return uguali;
    }

    public static bool Equals(Canzone? obj1, Canzone? obj2)
    {
        if (obj1 is null || obj2 is null) return false;
        return obj1.Equals(obj2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Canzone) return false;
        return Equals((Canzone?)obj);
    }

    public static bool operator ==(Canzone? left, Canzone? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Canzone? left, Canzone? right)
    {
        if (left is null || right is null) return false;
        return !left.Equals(right);
    }

    public override int GetHashCode()
    {
        var hc = (Titolo??string.Empty).GetHashCode() 
            ^ (Autore??string.Empty).GetHashCode() 
            ^ VariazioneInSemitoni.GetHashCode();
        hc ^= Strofe.GetHashCode();
        return hc;
    }
}
