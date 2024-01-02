using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music.Kantaro.Test;

internal class UtToni_Semitoni
{
    [Test]
    public void TestToni() {
        var t1 = Tono.Value;
        var t2 = Tono.Value;
        Assert.That(t2, Is.EqualTo(t1));
        Assert.That(t2, Is.SameAs(t1));
        var d = t1 + t2;
        Assert.IsTrue(d.Valore == 2);
    }

    [Test]
    public void TestSemitoni()
    {
        var v1 = Semitono.Value;
        var v2 = Semitono.Value;
        Assert.That(v2, Is.EqualTo(v1));
        Assert.That(v2, Is.SameAs(v1));
        Assert.That(Tono.Value, Is.EqualTo(v1 + v2));
        var d = v1 + v2;
        Assert.That(d.Valore, Is.EqualTo(1.0F));
        d = v1 * 2 + v2;
        Assert.That(d.Valore, Is.EqualTo(1.5F));
        d = 2 * v1 + v2;
        Assert.That(d.Valore, Is.EqualTo(1.5F));
    }
}
