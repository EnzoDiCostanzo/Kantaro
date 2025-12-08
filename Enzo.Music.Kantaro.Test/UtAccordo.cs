using Enzo.Music;

namespace Enzo.Music.Kantaro.Test;

public class UtAccordo
{

    private static Accordo DOmag, DO_mag, REmag, RE_mag, MImag, FAmag, FA_mag, SOLmag, SOL_mag, LAmag, LA_mag, SImag;
    private static Accordo DOmin, DO_min, REmin, RE_min, MImin, FAmin, FA_min, SOLmin, SOL_min, LAmin, LA_min, SImin;

    [SetUp]
    public void Setup()
    {
        // Creazione degli accordi maggiori
        DOmag = new Accordo(new Scala(Nota.DO, ModoMaggiore.GetInstance()));
        DO_mag = new Accordo(new Scala(Nota.DOdiesis, ModoMaggiore.GetInstance()));
        REmag = new Accordo(new Scala(Nota.RE, ModoMaggiore.GetInstance()));
        RE_mag = new Accordo(new Scala(Nota.REdiesis, ModoMaggiore.GetInstance()));
        MImag = new Accordo(new Scala(Nota.MI, ModoMaggiore.GetInstance()));
        FAmag = new Accordo(new Scala(Nota.FA, ModoMaggiore.GetInstance()));
        FA_mag = new Accordo(new Scala(Nota.FAdiesis, ModoMaggiore.GetInstance()));
        SOLmag = new Accordo(new Scala(Nota.SOL, ModoMaggiore.GetInstance()));
        SOL_mag = new Accordo(new Scala(Nota.SOLdiesis, ModoMaggiore.GetInstance()));
        LAmag = new Accordo(new Scala(Nota.LA, ModoMaggiore.GetInstance()));
        LA_mag = new Accordo(new Scala(Nota.LAdiesis, ModoMaggiore.GetInstance()));
        SImag = new Accordo(new Scala(Nota.SI, ModoMaggiore.GetInstance()));
        // Creazione degli accordi minori
        DOmin = new Accordo(new Scala(Nota.DO, ModoMinoreArmonica.GetInstance()));
        DO_min = new Accordo(new Scala(Nota.DOdiesis, ModoMinoreArmonica.GetInstance()));
        REmin = new Accordo(new Scala(Nota.RE, ModoMinoreArmonica.GetInstance()));
        RE_min = new Accordo(new Scala(Nota.REdiesis, ModoMinoreArmonica.GetInstance()));
        MImin = new Accordo(new Scala(Nota.MI, ModoMinoreArmonica.GetInstance()));
        FAmin = new Accordo(new Scala(Nota.FA, ModoMinoreArmonica.GetInstance()));
        FA_min = new Accordo(new Scala(Nota.FAdiesis, ModoMinoreArmonica.GetInstance()));
        SOLmin = new Accordo(new Scala(Nota.SOL, ModoMinoreArmonica.GetInstance()));
        SOL_min = new Accordo(new Scala(Nota.SOLdiesis, ModoMinoreArmonica.GetInstance()));
        LAmin = new Accordo(new Scala(Nota.LA, ModoMinoreArmonica.GetInstance()));
        LA_min = new Accordo(new Scala(Nota.LAdiesis, ModoMinoreArmonica.GetInstance()));
        SImin = new Accordo(new Scala(Nota.SI, ModoMinoreArmonica.GetInstance()));
    }

    [Test]
    public void ClonazioneAccordiMaggiori()
    {
        object n1 = DOmag.Clone();
        Assert.That(DOmag.GetType(), Is.EqualTo(n1.GetType()));
        Assert.That(n1, Is.Not.SameAs(DOmag));
        Accordo acc = (Accordo)n1;
        Assert.That(acc.Scala, Is.EqualTo(DOmag.Scala));
        Assert.That(acc.Estensione.IsEmpty);
        Assert.IsNull(acc.Basso);
    }

    [Test]
    public void ClonazioneAccordiMinori()
    {
        object n1 = DOmin.Clone();
        Assert.That(DOmin.GetType(), Is.EqualTo(n1.GetType()));
        Assert.That(n1, Is.Not.SameAs(DOmin));
        Accordo acc = (Accordo)n1;
        Assert.That(acc.Scala, Is.EqualTo(DOmin.Scala));
        Assert.That(acc.Estensione.IsEmpty);
        Assert.IsNull(acc.Basso);
    }

    [Test]
    public void TestConversioneAccordiDaStringhe()
    {
        var c = (Accordo?)"Do";
        Assert.That(c, Is.EqualTo(DOmag));
        var c7 = (Accordo?)"Do7";
        Assert.That(c7?.ToString(), Is.EqualTo("Do7"), "Fallito DO7");
        var c7_ = (Accordo?)"Do7+";
        Assert.That(c7_?.ToString(), Is.EqualTo("Do7+"), "Fallito DO7+");
        var Sib = (Accordo?)"SIb";
        Assert.That(Sib, Is.EqualTo(LA_mag));
        var Sib_2 = (Accordo?)"Sib";
        Assert.That(Sib_2, Is.EqualTo(Sib)); // Controllo maiuscole e minuscole sul SIb
        var Mim = (Accordo?)"Mi-";
        Assert.That(Mim, Is.EqualTo(MImin));
        var Do_dim = (Accordo?)"Do#dim";
        Accordo expected_Do_dim = new Accordo(DO_mag.Scala, new Accordo.EstensioneT(0, Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.Diminuito));
        Assert.That(Do_dim, Is.EqualTo(expected_Do_dim));
    }

    private void ConfrontaAccordiA1Tono(string text, Accordo? acc, Accordo? accTonoPrec)
    {
        if(acc is null) throw new ArgumentNullException(nameof(acc));
        if (accTonoPrec is null) throw new ArgumentNullException(nameof(accTonoPrec));
        acc -= (Distanza)1;
        Assert.That(acc, Is.EqualTo(accTonoPrec), $"{text}: acc - 1 tono != accTonoPrec");
        Assert.That(acc.ToString(false), Is.EqualTo(accTonoPrec?.ToString(false)), $"{text}: Accordo.ToString(false)");
        string expected = accTonoPrec?.ToString(true)??string.Empty;
        string actual = acc.ToString(true);
        Assert.That(actual, Is.EqualTo(expected), $"{text}: Accordo.ToString(true)");
    }

    [Test]
    public void TestConversioneAccordiInStringhe()
    {
        var DO7 = (Accordo?)"DO7";
        Assert.That(DO7?.ToString().ToUpper(), Is.EqualTo("DO7"));
        var SIb7 = (Accordo?)"SIb7";
        ConfrontaAccordiA1Tono("DO7,SIb7", DO7, SIb7);
        var a2 = SOL_mag.Clone() as Accordo;
        if(a2 is null) throw new NullReferenceException("Trovato valore null al Clone dell'accordo di SOL");
        a2.Estensione = 7;
        ConfrontaAccordiA1Tono("SIb7,SOL#7", SIb7, a2);
    }

    [Test]
    public void TestMethod_DOmaggiore()
    {
        var nuovoAccordo = DOmag + Tono.Value;
        Assert.That(nuovoAccordo, Is.EqualTo(REmag), "DO + 1 tono <> RE");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO maggiore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("RE"));
        nuovoAccordo -= Semitono.Value;
        Assert.That(nuovoAccordo, Is.EqualTo(DO_mag), "RE - 1 semitono <> DO#");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE maggiore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("DO#"));
    }

    [Test]
    public void TestMethod_DOmaggiore_ConDistanza()
    {
        Distanza dist = new Distanza { Valore = 1 };
        var nuovoAccordo = DOmag + dist;
        Assert.That(nuovoAccordo, Is.EqualTo(REmag), "DO + 1 tono <> RE");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO maggiore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("RE"));
        nuovoAccordo -= new Distanza {Semitoni = 1};
        Assert.That(nuovoAccordo, Is.EqualTo(DO_mag), "RE - 1 semitono <> DO#");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE maggiore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("DO#"));
    }

    [Test]
    public void TestMethod_DOmaggiore7()
    {
        var nuovoAccordo = (Accordo?)"Do7";
        Assert.IsNotNull(nuovoAccordo, "La conversione dell'accordo Do7 ha restituito il valore 'null'");
        Assert.That(!nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione non valorizzata dopo conversione dalla stringa \"Do7\"");
        nuovoAccordo.Estensione = 4;
        Assert.That(!nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione non valorizzata dopo valorizzazione della proprietà sull'accordo di DO maggiore");
        Assert.That(nuovoAccordo.Estensione.Valore, Is.EqualTo(4));
        Assert.That(nuovoAccordo.Estensione.VariazioneSemitono, Is.EqualTo(Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.None));
        Assert.That(nuovoAccordo, Is.Not.SameAs(DOmag));
        Assert.That(nuovoAccordo, Is.Not.EqualTo(DOmag));
        nuovoAccordo.Estensione = 7;
        nuovoAccordo += Tono.Value;
        var RE7 = (Accordo)REmag.Clone();
        RE7.Estensione = 7;
        Assert.That(nuovoAccordo, Is.EqualTo(RE7), "DO7 + 1 tono <> RE7");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO7 maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("RE7"));
        nuovoAccordo = (Accordo?)"Do7";
        Assert.IsNotNull(nuovoAccordo, "La conversione dell'accordo Do7 ha restituito il valore 'null'");
        nuovoAccordo += new Distanza {Valore = 1};
        Assert.That(nuovoAccordo, Is.EqualTo(RE7), "DO7 + 1 tono <> RE7");
        nuovoAccordo -= Semitono.Value;
        Accordo DO_7 = (Accordo)DO_mag.Clone();
        DO_7.Estensione = 7;
        Assert.That(nuovoAccordo, Is.EqualTo(DO_7), "RE7 - 1 semitono <> DO#7");
        Assert.That(!nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione nulla dopo decremento di un semitono dell'accordo di RE7 maggiore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE7 maggiore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("DO#7"));
    }

    [Test]
    public void TestMethod_VariazioneAccordi()
    {
        Distanza dist = new Distanza();
        dist.Valore = -1; // Toglie 1 tono
        // Definizione accordo di DO
        var v = DOmag.Clone() as Accordo;
        Assert.IsNotNull(v, "Il Clone dell'accordo di DO ha restituito il valore 'null'");
        // Accordo atteso
        var att = (Accordo?)"SIb";
        Assert.IsNotNull(att, "La conversione dell'accordo SIb ha restituito il valore 'null'");
        Assert.That(v + dist, Is.EqualTo(att)); // Testa l'accordo SIb
        // Test con estensione
        v.Estensione = 4;
        att.Estensione = 4;
        Assert.That(v + dist, Is.EqualTo(att)); // Testa l'accordo SIb4
        // Test con Basso
        v.Basso = Nota.LA;
        att.Basso = Nota.SOL;
        Assert.That(v + dist, Is.EqualTo(att)); // Testa l'accordo SIb4/ SOL
        // Test con estensione
        v.Estensione = Accordo.EstensioneT.Empty;
        att.Estensione = Accordo.EstensioneT.Empty;
        Assert.That(v + dist, Is.EqualTo(att)); // Testa l'accordo SIb/ SOL
    }

    [Test]
    public void TestMethod_DOminore()
    {
        var nuovoAccordo = DOmin + Tono.Value;
        Assert.That(nuovoAccordo, Is.EqualTo(REmin), "DO- + 1 tono <> RE-");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO minore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO minore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("RE-"));
        nuovoAccordo -= Semitono.Value;
        Assert.That(nuovoAccordo, Is.EqualTo(DO_min), "REmin - 1 semitono <> DO#min");
        Assert.That(nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE minore");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE minore");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("DO#-"));
    }

    [Test]
    public void TestMethod_DOminore7() {
        Accordo nuovoAccordo = (Accordo)DOmin.Clone();
        nuovoAccordo.Estensione = 7;
        Assert.That(!nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione non valorizzata dopo valorizzazione della proprietà sull'accordo di DO minore");
        Assert.That(nuovoAccordo.Estensione.Valore, Is.EqualTo(7));
        Assert.That(nuovoAccordo.Estensione.VariazioneSemitono, Is.EqualTo(Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.None));
        Assert.That(nuovoAccordo, Is.Not.SameAs(DOmin));
        nuovoAccordo += Tono.Value;
        Accordo RE7 = (Accordo)REmin.Clone();
        RE7.Estensione = 7;
        Assert.That(nuovoAccordo, Is.EqualTo(RE7), "DO-7 + 1 tono <> RE-7");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO-7");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("RE-7"));
        nuovoAccordo -= Semitono.Value;
        Accordo DO_7 = (Accordo)DO_min.Clone();
        DO_7.Estensione = 7;
        Assert.That(nuovoAccordo, Is.EqualTo(DO_7), "RE-7 - 1 semitono <> DO#-7");
        Assert.That(!nuovoAccordo.Estensione.IsEmpty, "Accordo.Estensione nulla dopo decremento di un semitono dell'accordo di RE-7");
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE-7");
        Assert.That(nuovoAccordo.ToString().ToUpper(), Is.EqualTo("DO#-7"));
    }
}