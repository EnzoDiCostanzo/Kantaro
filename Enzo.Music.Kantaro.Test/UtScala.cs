using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music.Kantaro.Test;

internal class UtScala
{
    [Test]
    public void ScalaMaggioreDO()
    {
        Scala scala = new Scala(Nota.DO, ModoMaggiore.GetInstance());
        Assert.That(scala.NotaFondamentale, Is.EqualTo(Nota.DO));
        Assert.That(scala[0], Is.EqualTo(Nota.DO));
        Assert.That(scala[1], Is.EqualTo(Nota.RE));
        Assert.That(scala[2], Is.EqualTo(Nota.MI));
        Assert.That(scala[3], Is.EqualTo(Nota.FA));
        Assert.That(scala[4], Is.EqualTo(Nota.SOL));
        Assert.That(scala[5], Is.EqualTo(Nota.LA));
        Assert.That(scala[6], Is.EqualTo(Nota.SI));
    }
    [Test]
    public void ScalaMaggioreDOdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.DOdiesis), ModoMaggiore.GetInstance());
        Assert.That(scala.NotaFondamentale, Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.DO)));
    }
    [Test]
    public void ScalaMaggioreRE()
    {
        Scala scala = new Scala(new Nota(NotaEnum.RE), ModoMaggiore.GetInstance());
        Assert.That(scala.NotaFondamentale, Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
    }
    [Test]
    public void ScalaMaggioreREdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.REdiesis), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.RE)));
    }
    [Test]
    public void ScalaMaggioreMI()
    {
        Scala scala = new Scala(new Nota(NotaEnum.MI), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
    }
    [Test]
    public void ScalaMaggioreFA()
    {
        Scala scala = new Scala(new Nota(NotaEnum.FA), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.MI)));
    }
    [Test]
    public void ScalaMaggioreFAdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.FAdiesis), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.FA)));
    }
    [Test]
    public void ScalaMaggioreSOL()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SOL), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
    }
    [Test]
    public void ScalaMaggioreSOLdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SOLdiesis), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.SOL)));
    }
    [Test]
    public void ScalaMaggioreLA()
    {
        Scala scala = new Scala(new Nota(NotaEnum.LA), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
    }
    [Test]
    public void ScalaMaggioreLAdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.LAdiesis), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.LA)));
    }
    [Test]
    public void ScalaMaggioreSI()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SI), ModoMaggiore.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
    }

    [Test]
    public void ScalaMinoreNaturaleDO()
    {
        Scala scala = new Scala(new Nota(NotaEnum.DO), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
    }
    [Test]
    public void ScalaMinoreNaturaleDOdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.DOdiesis), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.SI)));
    }
    [Test]
    public void ScalaMinoreNaturaleRE()
    {
        Scala scala = new Scala(new Nota(NotaEnum.RE), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.DO)));
    }
    [Test]
    public void ScalaMinoreNaturaleREdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.REdiesis), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
    }
    [Test]
    public void ScalaMinoreNaturaleMI()
    {
        Scala scala = new Scala(new Nota(NotaEnum.MI), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.RE)));
    }
    [Test]
    public void ScalaMinoreNaturaleFA()
    {
        Scala scala = new Scala(new Nota(NotaEnum.FA), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
    }
    [Test]
    public void ScalaMinoreNaturaleFAdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.FAdiesis), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.MI)));
    }
    [Test]
    public void ScalaMinoreNaturaleSOL()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SOL), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.FA)));
    }
    [Test]
    public void ScalaMinoreNaturaleSOLdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SOLdiesis), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
    }
    [Test]
    public void ScalaMinoreNaturaleLA()
    {
        Scala scala = new Scala(new Nota(NotaEnum.LA), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.LA)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.SOL)));
    }
    [Test]
    public void ScalaMinoreNaturaleLAdiesis()
    {
        Scala scala = new Scala(new Nota(NotaEnum.LAdiesis), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.LAdiesis)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.DO)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.REdiesis)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.FA)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.SOLdiesis)));
    }
    [Test]
    public void ScalaMinoreNaturaleSI()
    {
        Scala scala = new Scala(new Nota(NotaEnum.SI), ModoMinoreNaturale.GetInstance());
        Assert.That(scala[0], Is.EqualTo(new Nota(NotaEnum.SI)));
        Assert.That(scala[1], Is.EqualTo(new Nota(NotaEnum.DOdiesis)));
        Assert.That(scala[2], Is.EqualTo(new Nota(NotaEnum.RE)));
        Assert.That(scala[3], Is.EqualTo(new Nota(NotaEnum.MI)));
        Assert.That(scala[4], Is.EqualTo(new Nota(NotaEnum.FAdiesis)));
        Assert.That(scala[5], Is.EqualTo(new Nota(NotaEnum.SOL)));
        Assert.That(scala[6], Is.EqualTo(new Nota(NotaEnum.LA)));
    }
}
