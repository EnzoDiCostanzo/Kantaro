using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Enzo.Music.Kantaro.Test;

internal class UtNota
{
    [Test]
    public void VerificaNoteStatiche()
    {
        Assert.That(new Nota(NotaEnum.DO), Is.EqualTo(Nota.DO), "Nota statica DO non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.DOdiesis), Is.EqualTo(Nota.DOdiesis), "Nota statica DO# non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.RE), Is.EqualTo(Nota.RE), "Nota statica RE non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.REdiesis), Is.EqualTo(Nota.REdiesis), "Nota statica RE# non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.MI), Is.EqualTo(Nota.MI), "Nota statica MI non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.FA), Is.EqualTo(Nota.FA), "Nota statica FA non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.FAdiesis), Is.EqualTo(Nota.FAdiesis), "Nota statica FA# non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.SOL), Is.EqualTo(Nota.SOL), "Nota statica SOL non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.SOLdiesis), Is.EqualTo(Nota.SOLdiesis), "Nota statica SOL# non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.LA), Is.EqualTo(Nota.LA), "Nota statica LA non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.LAdiesis), Is.EqualTo(Nota.LAdiesis), "Nota statica LA# non corrisponde a quella del costruttore");
        Assert.That(new Nota(NotaEnum.SI), Is.EqualTo(Nota.SI), "Nota statica SI non corrisponde a quella del costruttore");
    }

    [Test]
    public void VerificaNoteStatiche_Bemolle()
    {
        Assert.That(Nota.DOdiesis, Is.EqualTo(Nota.REb), "Nota statica REb non corrisponde a quella del DO#");
        Assert.That(Nota.REdiesis, Is.EqualTo(Nota.MIb), "Nota statica MIb non corrisponde a quella del RE#");
        Assert.That(Nota.FAdiesis, Is.EqualTo(Nota.SOLb), "Nota statica SOLb non corrisponde a quella del FA#");
        Assert.That(Nota.SOLdiesis, Is.EqualTo(Nota.LAb), "Nota statica LAb non corrisponde a quella del SOL#");
        Assert.That(Nota.LAdiesis, Is.EqualTo(Nota.SIb), "Nota statica SIb non corrisponde a quella del LA#");
    }

    [Test]
    public void NoteStringParseTest()
    {
        Assert.That(Nota.TryParse("DO", out Nota? n), "Conversione di \"DO\" non riuscita");
        Assert.That(Nota.DO, Is.EqualTo(n), "Conversione di \"DO\" non corrisponde alla nota DO");
        Assert.That(Nota.TryParse("DO#", out n), "Conversione di \"DO#\" non riuscita");
        Assert.That(Nota.DOdiesis, Is.EqualTo(n), "Conversione di \"DO#\" non corrisponde alla nota DO#");
        Assert.That(Nota.TryParse("REb", out n), "Conversione di \"REb\" non riuscita");
        Assert.That(Nota.DOdiesis, Is.EqualTo(n), "Conversione di \"REb\" non corrisponde alla nota DO#");
    }

    [Test]
    public void VerificaToString_Note()
    {
        Assert.That(Nota.DO.ToString().ToUpper(), Is.EqualTo("DO"), "ToString della nota DO non corrisponde");
        Assert.That(Nota.DOdiesis.ToString().ToUpper(), Is.EqualTo("DO#"), "ToString della nota DO# non corrisponde");
        Assert.That(Nota.RE.ToString().ToUpper(), Is.EqualTo("RE"), "ToString della nota RE non corrisponde");
        Assert.That(Nota.REdiesis.ToString().ToUpper(), Is.EqualTo("RE#"), "ToString della nota RE# non corrisponde");
        Assert.That(Nota.MI.ToString().ToUpper(), Is.EqualTo("MI"), "ToString della nota MI non corrisponde");
        Assert.That(Nota.FA.ToString().ToUpper(), Is.EqualTo("FA"), "ToString della nota FA non corrisponde");
        Assert.That(Nota.FAdiesis.ToString().ToUpper(), Is.EqualTo("FA#"), "ToString della nota FA# non corrisponde");
        Assert.That(Nota.SOL.ToString().ToUpper(), Is.EqualTo("SOL"), "ToString della nota SOL non corrisponde");
        Assert.That(Nota.SOLdiesis.ToString().ToUpper(), Is.EqualTo("SOL#"), "ToString della nota SOL# non corrisponde");
        Assert.That(Nota.LA.ToString().ToUpper(), Is.EqualTo("LA"), "ToString della nota LA non corrisponde");
        Assert.That(Nota.LAdiesis.ToString().ToUpper(), Is.EqualTo("LA#"), "ToString della nota LA# non corrisponde");
        Assert.That(Nota.SI.ToString().ToUpper(), Is.EqualTo("SI"), "ToString della nota SI non corrisponde");
    }

    [Test]
    public void VerificaToString_NoteBemolle()
    {
        Assert.IsTrue("REb".Equals(Nota.DOdiesis.ToString(true), StringComparison.InvariantCultureIgnoreCase), "ToString della nota DO# non corrisponde al REb");
        Assert.IsTrue("MIb".Equals(Nota.REdiesis.ToString(true), StringComparison.InvariantCultureIgnoreCase), "ToString della nota RE# non corrisponde al MIb");
        Assert.IsTrue("SOLb".Equals(Nota.FAdiesis.ToString(true), StringComparison.InvariantCultureIgnoreCase), "ToString della nota FA# non corrisponde al SOLb");
        Assert.IsTrue("LAb".Equals(Nota.SOLdiesis.ToString(true), StringComparison.InvariantCultureIgnoreCase), "ToString della nota SOL# non corrisponde al LAb");
        Assert.IsTrue("SIb".Equals(Nota.LAdiesis.ToString(true), StringComparison.InvariantCultureIgnoreCase), "ToString della nota LA# non corrisponde al SIb");
    }

    [Test]
    public void VerificaSaltiNote_dalDO()
    {
        var C = Nota.DO;
        var nuova = C + Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.DOdiesis), "Verifica DO + 1 semitono");
        nuova = C + 2 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.RE), "Verifica DO + 2 semitoni");
        nuova = C + 3 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.REdiesis), "Verifica DO + 3 semitoni");
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota(i);
            Assert.That(C + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota DO + {(int)i}");
        }
    }

    [Test]
    public void VerificaSaltiNote_dalDOdiesis()
    {
        var C_ = Nota.DOdiesis;
        var nuova = C_ + Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.RE), "Verifica DO# + 1 semitono");
        nuova = C_ + 2 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.REdiesis), "Verifica DO# + 2 semitoni");
        nuova = C_ + 3 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.MI), "Verifica DO# + 3 semitoni");
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)((1 + (int)i) % 12));
            Assert.That(C_ + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota DO# + {(int)i}");
        }
    }

    [Test]
    public void VerificaSaltiNote_dalRE()
    {
        var D = Nota.RE;
        var nuova = D + Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.REdiesis), "Verifica RE + 1 semitono");
        nuova = D + 2 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.MI), "Verifica RE + 2 semitoni");
        nuova = D + 3 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.FA), "Verifica RE + 3 semitoni");
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)D.Valore + (int)i) % 12));
            Assert.That(D + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota RE + {(int)i}");
        }
    }

    [Test]
    public void VerificaSaltiNote_dalREdiesis()
    {
        var D_ = Nota.REdiesis;
        var nuova = D_ + Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.MI), "Verifica RE# + 1 semitono");
        nuova = D_ + 2 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.FA), "Verifica RE# + 2 semitoni");
        nuova = D_ + 3 * Semitono.Value;
        Assert.That(nuova, Is.EqualTo(Nota.FAdiesis), "Verifica RE# + 3 semitoni");
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)D_.Valore + (int)i) % 12));
            Assert.That(D_ + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota RE + {(int)i}");
        }
    }

    [Test]
    public void VerificaSaltiNote_dalMI()
    {
        var E = Nota.MI;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)E.Valore + (int)i) % 12));
            Assert.That(E + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota MI + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalFA()
    {
        var F = Nota.FA;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)F.Valore + (int)i) % 12));
            Assert.That(F + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota FA + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalFAdiesis()
    {
        var F_ = Nota.FAdiesis;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)F_.Valore + (int)i) % 12));
            Assert.That(F_ + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota FA# + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalSOL()
    {
        var G = Nota.SOL;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)G.Valore + (int)i) % 12));
            Assert.That(G + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota SOL + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalSOLdiesis()
    {
        var G_ = Nota.SOLdiesis;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)G_.Valore + (int)i) % 12));
            Assert.That(G_ + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota SOL# + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalLA()
    {
        var A = Nota.LA;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)A.Valore + (int)i) % 12));
            Assert.That(A + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota LA + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalLAdiesis()
    {
        var A_ = Nota.LAdiesis;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)A_.Valore + (int)i) % 12));
            Assert.That(A_ + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota LA# + {(int)i}");
        }
    }
    [Test]
    public void VerificaSaltiNote_dalSI()
    {
        var B = Nota.SI;
        Nota nuova;
        for (NotaEnum i = NotaEnum.DO; i <= NotaEnum.SI; i++)
        {
            nuova = new Nota((NotaEnum)(((int)B.Valore + (int)i) % 12));
            Assert.That(B + (Distanza)((float)i / 2), Is.EqualTo(nuova), $"Verifica costruzione con salti nota SI + {(int)i}");
        }
    }
}
