using Enzo.Music.Kantaro;

namespace Enzo.Music.Kantaro.Test;

internal class UtCanzone
{
    [Test]
    public void TestEqualsSuCanzone()
    {
        Canzone a = GetCanzone(4, 4, true);
        Canzone b = GetCanzone(4, 4, true);
        Assert.That(a.Equals(a), "La stessa canzone non risulta uguale a se stessa");
        Assert.That(a.Equals(b), "Canzoni identiche non risultano uguali");
        // Controllo autore
        a.Autore = "Modificato";
        Assert.That(a, Is.Not.EqualTo(b), "Canzoni con Autore diverso risultano uguali");
        // Controllo titolo
        a = GetCanzone(4, 4, true);
        a.Titolo = "Modificato";
        // Controllo titolo
        a = GetCanzone(4, 4, true);
        a.VariazioneInSemitoni = 3;
        Assert.That(a, Is.Not.EqualTo(b), "Canzoni con VariazioneInSemitoni diversi risultano uguali");
        // Controllo numero Strofe
        a = GetCanzone(4, 4, true);
        a.Strofe.Remove(a.Strofe[3]);
        Assert.That(a, Is.Not.EqualTo(b), "Canzoni con strofe diverse risultano uguali");
    }

    private Canzone GetCanzone(int numeroStrofe, int numeroParti, bool conRit)
    {
        var c = new Canzone();
        c.Titolo = "Titolo della canzone";
        c.Autore = "Autore della canzone";
        if (conRit)
        {
            Strofa rit = new() { Nome = "rit", Parti = GetParti(numeroParti) };
            c.Strofe.Add(rit);
        }
        int num;
        if (conRit)
            num = numeroStrofe / 2;
        else
            num = numeroStrofe;
        for (int i = 1; i <= num; i++)
        {
            c.Strofe.Add(new Strofa { Parti = GetParti(numeroParti) });
            if (conRit) c.Strofe.Add(new StrofaRipetuta("rit"));
        }
        return c;
    }

    private List<Parte?> GetParti(int numero)
    {
        var r = new List<Parte?>();
        for (int i = 1; i <= numero; i++)
        {
            Parte p = new Parte { Testo = $"Parte n°{numero}" };
            r.Add(p);
        }
        return r;
    }
}
