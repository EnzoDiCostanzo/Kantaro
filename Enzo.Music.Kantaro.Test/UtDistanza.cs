using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music.Kantaro.Test;

internal class UtDistanza
{
    [Test]
    public void TestInit()
    {
        var d = new Distanza();
        Assert.That(d.Valore == 0, "Valore iniziale di Distanza diverso da 0");
        Assert.That(d.Semitoni == 0, "NumSemitoni iniziale di Distanza diverso da 0");
        Assert.That(d.Toni == 0, "Toni iniziale di Distanza diverso da 0");
    }

    [Test]
    public void TestSemitono()
    {
        var d = Semitono.Value;
        Assert.That(d.Valore == 0.5F, "Valore di Semitono diverso da 0.5");
        Assert.That(d.Semitoni == 1, "NumSemitoni di Semitono diverso da 1");
        Assert.That(d.Toni == 0.5F, "Toni di Semitono diverso da 0.5");
    }

    [Test]
    public void TestTono()
    {
        var d = Tono.Value;
        Assert.That(d.Valore == 1.0F, "Valore di Tono diverso da 1.0");
        Assert.That(d.Semitoni == 2, "NumSemitoni di Tono diverso da 2");
        Assert.That(d.Toni == 1.0F, "Toni di Tono diverso da 1.0");
    }

    [Test]
    public void TestMethod1()
    {
        var d1 = new Distanza();
        d1.Valore = 1.5F;
        var d2 = 3 * Semitono.Value;
        Assert.That(d2, Is.EqualTo(d1), "(1t + 1st) <> 3 st");
    }
    [Test]
    public void TestMethod2()
    {
        var d1 = new Distanza();
        d1.Valore = 1.5F;
        var d2 = d1 + 6;
        Assert.That(d2.Semitoni, Is.EqualTo(15), "(1t + 1st) + 6 t <> 15st");
    }

    [Test]
    public void TestMethod3()
    {
        var d1 = new Distanza();
        d1.Valore = 1.5F;
        var d2 = new Distanza { Valore = 3 };
        var r = d1 + d2;
        Assert.That(r.Valore == 4.5, "(1t + 1st) + 3t");
    }

    [Test]
    public void TestValore1()
    {
        var d = new Distanza();
        var valore = 1.0F;
        d.Valore = valore;
        Assert.That(d.Toni == 1.0, "Valore di Distanza 1 non ha portato il risultato d.Toni=1");
        Assert.That(d.Semitoni == 2, "Valore di Distanza 1 non ha portato il risultato d.NumSemitoni=2");
        valore = 3.5F;
        d.Valore = valore;
        Assert.That(d.Toni == 3.5F, "Valore di Distanza 3.5 non ha portato il risultato d.Toni=3,5");
        Assert.That(d.Semitoni == 7, "Valore di Distanza 3.5 non ha portato il risultato d.NumSemitoni=7");
    }

    [Test]
    public void TestValore2()
    {
        var d = new Distanza();
        var valore = 1.0F;
        d.Valore = valore;
        Assert.That(d.Valore, Is.EqualTo(valore), "Valore di Distanza 1 diverso dopo l'assegnazione");
        valore = 3.5F;
        d.Valore = valore;
        Assert.That(d.Valore, Is.EqualTo(valore), "Valore di Distanza 3.5 diverso dopo l'assegnazione");
    }

    [Test]
    public void CastFromNumber1()
    {
        var d1 = Tono.Value * 2 + Semitono.Value;
        var d2 = (Distanza)2.5;
        Assert.That(d1.Semitoni == 5 && d1.Valore == 2.5, "Valore e NumSemitoni non corretto dopo conversione da 2.5");
        Assert.That(d2, Is.EqualTo(d1), "Valore di Distanza diverso da quello previsto per 2.5");
    }

    [Test]
    public void CastWithExceptionFromNumber()
    {
        try
        {
            var d = (Distanza)2.7;
            Assert.Fail("Non ha dato errore la conversione da 2.7 a Distanza");
        }
        catch //(InvalidCastException ex)
        {
            var d2 = (Distanza)3;
        }

    }

    [Test]
    public void CastToNumber2()
    {
        var d = (Distanza)2.5;
        Assert.That((Single)d == 2.5, "Valore di ritorno '(Distanza)d == 2.5' non riscontrato");
        d = (Distanza)2;
        Assert.That((Single)d == 2, "Valore di ritorno '(Distanza)d == 2.0' non riscontrato");
    }

    [Test]
    public void CastFromString()
    {
        var d1 = (Distanza)"2";
        Assert.That(d1.Valore == 2.0!, "Valore non corretto dopo conversione da \"2\"");
        var d2 = (Distanza)"2,5";
        Assert.That(d2.Valore == 2.5!, "Valore non corretto dopo conversione da \"2,5\"");
    }

    [Test]
    public void CastToString()
    {
        try
        {
            var d = new Distanza();
            Single expected;
            d.Valore = 3.5F;
            expected = 3.5F;
            Assert.That(d.ToString(), Is.EqualTo(expected.ToString()), "Valore di Distanza 3.5 diverso da \"3,5\"");
            d.Valore = 1;
            expected = 1.0F;
            Assert.That(d.ToString(), Is.EqualTo(expected.ToString()), "Valore di Distanza 1 diverso da \"1\"");
        }
        catch //(InvalidCastException ex)
        {
            var d2 = (Distanza)3;
        }
    }
}
