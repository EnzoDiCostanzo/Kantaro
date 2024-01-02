Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class utAccordo

    Private Shared DOmag, DO_mag, REmag, RE_mag, MImag, FAmag, FA_mag, SOLmag, SOL_mag, LAmag, LA_mag, SImag As Accordo
    Private Shared DOmin, DO_min, REmin, RE_min, MImin, FAmin, FA_min, SOLmin, SOL_min, LAmin, LA_min, SImin As Accordo

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        ' Creazione degli accordi maggiori
        DOmag = New Accordo(New Scala(Nota.DO, ModoMaggiore.GetInstance))
        DO_mag = New Accordo(New Scala(Nota.DOdiesis, ModoMaggiore.GetInstance))
        REmag = New Accordo(New Scala(Nota.RE, ModoMaggiore.GetInstance))
        RE_mag = New Accordo(New Scala(Nota.REdiesis, ModoMaggiore.GetInstance))
        MImag = New Accordo(New Scala(Nota.MI, ModoMaggiore.GetInstance))
        FAmag = New Accordo(New Scala(Nota.FA, ModoMaggiore.GetInstance))
        FA_mag = New Accordo(New Scala(Nota.FAdiesis, ModoMaggiore.GetInstance))
        SOLmag = New Accordo(New Scala(Nota.SOL, ModoMaggiore.GetInstance))
        SOL_mag = New Accordo(New Scala(Nota.SOLdiesis, ModoMaggiore.GetInstance))
        LAmag = New Accordo(New Scala(Nota.LA, ModoMaggiore.GetInstance))
        LA_mag = New Accordo(New Scala(Nota.LAdiesis, ModoMaggiore.GetInstance))
        SImag = New Accordo(New Scala(Nota.SI, ModoMaggiore.GetInstance))
        ' Creazione degli accordi minori
        DOmin = New Accordo(New Scala(Nota.DO, ModoMinoreArmonica.GetInstance))
        DO_min = New Accordo(New Scala(Nota.DOdiesis, ModoMinoreArmonica.GetInstance))
        REmin = New Accordo(New Scala(Nota.RE, ModoMinoreArmonica.GetInstance))
        RE_min = New Accordo(New Scala(Nota.REdiesis, ModoMinoreArmonica.GetInstance))
        MImin = New Accordo(New Scala(Nota.MI, ModoMinoreArmonica.GetInstance))
        FAmin = New Accordo(New Scala(Nota.FA, ModoMinoreArmonica.GetInstance))
        FA_min = New Accordo(New Scala(Nota.FAdiesis, ModoMinoreArmonica.GetInstance))
        SOLmin = New Accordo(New Scala(Nota.SOL, ModoMinoreArmonica.GetInstance))
        SOL_min = New Accordo(New Scala(Nota.SOLdiesis, ModoMinoreArmonica.GetInstance))
        LAmin = New Accordo(New Scala(Nota.LA, ModoMinoreArmonica.GetInstance))
        LA_min = New Accordo(New Scala(Nota.LAdiesis, ModoMinoreArmonica.GetInstance))
        SImin = New Accordo(New Scala(Nota.SI, ModoMinoreArmonica.GetInstance))
    End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()> Public Sub ClonazioneAccordiMaggiori()
        Dim n1 = DOmag.Clone
        Assert.IsInstanceOfType(n1, DOmag.GetType)
        Assert.AreNotSame(DOmag, n1)
        Dim acc = DirectCast(n1, Accordo)
        Assert.AreEqual(DOmag.Scala, acc.Scala)
        Assert.IsNull(acc.Estensione)
        Assert.IsNull(acc.Basso)
    End Sub

    <TestMethod()> Public Sub ClonazioneAccordiMinori()
        Dim n1 = DOmin.Clone
        Assert.IsInstanceOfType(n1, DOmin.GetType)
        Assert.AreNotSame(DOmin, n1)
        Dim acc = DirectCast(n1, Accordo)
        Assert.AreEqual(DOmin.Scala, acc.Scala)
        Assert.IsNull(acc.Estensione)
        Assert.IsNull(acc.Basso)
    End Sub

    <TestMethod> Public Sub TestConversioneAccordiDaStringhe()
        Dim c = CType("Do", Accordo)
        Assert.AreEqual(DOmag, c)
        Dim c7 = CType("Do7", Accordo)
        Assert.AreEqual("Do7", c7.ToString, "Fallito DO7")
        Dim c7_ = CType("Do7+", Accordo)
        Assert.AreEqual("Do7+", c7_.ToString, "Fallito DO7+")
        Dim Sib = CType("SIb", Accordo)
        Assert.AreEqual(LA_mag, Sib)
        Dim Sib_2 = CType("Sib", Accordo)
        Assert.AreEqual(Sib, Sib_2) ' Controllo maiuscole e minuscole sul SIb
        Dim Mim = CType("Mi-", Accordo)
        Assert.AreEqual(MImin, Mim)
        Dim Do_dim = CType("Do#dim", Accordo)
        Dim expected_Do_dim As New Accordo(DO_mag.Scala, New Accordo.EstensioneT(0, Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.Diminuito))
        Assert.AreEqual(expected_Do_dim, Do_dim)
    End Sub

    Private Sub ConfrontaAccordiA1Tono(text As String, acc As Accordo, accTonoPrec As Accordo)
        acc -= 1
        Assert.AreEqual(accTonoPrec, acc, text & ": acc - 1 tono != accTonoPrec")
        Assert.AreEqual(accTonoPrec.ToString(False), acc.ToString(False), text & ": Accordo.ToString(False)")
        Dim expected = accTonoPrec.ToString(True)
        Dim actual = acc.ToString(True)
        Assert.AreEqual(expected, actual, text & ": Accordo.ToString(True)")
    End Sub

    <TestMethod> Public Sub TestConversioneAccordiInStringhe()
        Dim DO7 = CType("DO7", Accordo)
        Assert.AreEqual("DO7", DO7.ToString.ToUpper())
        Dim SIb7 = CType("SIb7", Accordo)
        ConfrontaAccordiA1Tono("DO7,SIb7", DO7, SIb7)
        Dim a2 = DirectCast(SOL_mag.Clone, Accordo)
        a2.Estensione = 7
        ConfrontaAccordiA1Tono("SIb7,SOL#7", SIb7, a2)
    End Sub

    <TestMethod()> Public Sub TestMethod_DOmaggiore()
        Dim nuovoAccordo = DOmag + Tono.Value
        Assert.AreEqual(REmag, nuovoAccordo, "DO + 1 tono <> RE")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO maggiore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO maggiore")
        Assert.AreEqual("RE", nuovoAccordo.ToString, True)
        nuovoAccordo -= Semitono.Value
        Assert.AreEqual(DO_mag, nuovoAccordo, "RE - 1 semitono <> DO#")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE maggiore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE maggiore")
        Assert.AreEqual("DO#", nuovoAccordo.ToString, True)
    End Sub

    <TestMethod()> Public Sub TestMethod_DOmaggiore_ConDistanza()
        Dim dist As New Distanza() With {.Valore = 1}
        Dim nuovoAccordo = DOmag + dist
        Assert.AreEqual(REmag, nuovoAccordo, "DO + 1 tono <> RE")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO maggiore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO maggiore")
        Assert.AreEqual("RE", nuovoAccordo.ToString, True)
        nuovoAccordo -= New Distanza() With {.Semitoni = 1}
        Assert.AreEqual(DO_mag, nuovoAccordo, "RE - 1 semitono <> DO#")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE maggiore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE maggiore")
        Assert.AreEqual("DO#", nuovoAccordo.ToString, True)
    End Sub

    <TestMethod()> Public Sub TestMethod_DOmaggiore7()
        Dim nuovoAccordo As Accordo = CType("Do7", Accordo)
        Assert.IsNotNull(nuovoAccordo.Estensione, "Accordo.Estensione non valorizzata dopo conversione dalla stringa ""Do7""")
        nuovoAccordo.Estensione = 4
        Assert.IsNotNull(nuovoAccordo.Estensione, "Accordo.Estensione non valorizzata dopo valorizzazione della proprietà sull'accordo di DO maggiore")
        Assert.AreEqual(4, nuovoAccordo.Estensione.Valore)
        Assert.AreEqual(Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.None, nuovoAccordo.Estensione.VariazioneSemitono)
        Assert.AreNotSame(DOmag, nuovoAccordo)
        Assert.AreNotEqual(DOmag, nuovoAccordo)
        nuovoAccordo.Estensione = 7
        nuovoAccordo += Tono.Value
        Dim RE7 As Accordo = REmag.Clone
        RE7.Estensione = 7
        Assert.AreEqual(RE7, nuovoAccordo, "DO7 + 1 tono <> RE7")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO7 maggiore")
        Assert.AreEqual("RE7", nuovoAccordo.ToString, True)
        nuovoAccordo = CType("Do7", Accordo)
        nuovoAccordo += New Distanza() With {.Valore = 1}
        Assert.AreEqual(RE7, nuovoAccordo, "DO7 + 1 tono <> RE7")
        nuovoAccordo -= Semitono.Value
        Dim DO_7 As Accordo = DO_mag.Clone
        DO_7.Estensione = 7
        Assert.AreEqual(DO_7, nuovoAccordo, "RE7 - 1 semitono <> DO#7")
        Assert.IsNotNull(nuovoAccordo.Estensione, "Accordo.Estensione nulla dopo decremento di un semitono dell'accordo di RE7 maggiore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE7 maggiore")
        Assert.AreEqual("DO#7", nuovoAccordo.ToString, True)
    End Sub

    <TestMethod> Public Sub TestMethod_VariazioneAccordi()
        Dim dist As New Distanza()
        dist.Valore = -1 ' Toglie 1 tono
        ' Definizione accordo di DO
        Dim v = DirectCast(DOmag.Clone, Accordo)
        ' Accordo atteso
        Dim att = CType("SIb", Accordo)
        Assert.AreEqual(att, v + dist) ' Testa l'accordo SIb
        ' Test con estensione
        v.Estensione = 4
        att.Estensione = 4
        Assert.AreEqual(att, v + dist) ' Testa l'accordo SIb4
        ' Test con Basso
        v.Basso = Nota.LA
        att.Basso = Nota.SOL
        Assert.AreEqual(att, v + dist) ' Testa l'accordo SIb4/SOL
        ' Test con estensione
        v.Estensione = Nothing
        att.Estensione = Nothing
        Assert.AreEqual(att, v + dist) ' Testa l'accordo SIb/SOL
    End Sub

    <TestMethod()> Public Sub TestMethod_DOminore()
        Dim nuovoAccordo = DOmin + Tono.Value
        Assert.AreEqual(REmin, nuovoAccordo, "DO- + 1 tono <> RE-")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo incremento di un tono dell'accordo di DO minore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO minore")
        Assert.AreEqual("RE-", nuovoAccordo.ToString, True)
        nuovoAccordo -= Semitono.Value
        Assert.AreEqual(DO_min, nuovoAccordo, "REmin - 1 semitono <> DO#min")
        Assert.IsNull(nuovoAccordo.Estensione, "Accordo.Estensione valorizzata dopo decremento di un semitono dell'accordo di RE minore")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE minore")
        Assert.AreEqual("DO#-", nuovoAccordo.ToString, True)
    End Sub

    <TestMethod()> Public Sub TestMethod_DOminore7()
        Dim nuovoAccordo As Accordo = DOmin.Clone
        nuovoAccordo.Estensione = 7
        Assert.IsNotNull(nuovoAccordo.Estensione, "Accordo.Estensione non valorizzata dopo valorizzazione della proprietà sull'accordo di DO minore")
        Assert.AreEqual(7, nuovoAccordo.Estensione.Valore)
        Assert.AreEqual(Accordo.EstensioneT.EstensioneVariazioneSemitonoEnum.None, nuovoAccordo.Estensione.VariazioneSemitono)
        Assert.AreNotSame(DOmin, nuovoAccordo)
        nuovoAccordo += Tono.Value
        Dim RE7 As Accordo = REmin.Clone
        RE7.Estensione = 7
        Assert.AreEqual(RE7, nuovoAccordo, "DO-7 + 1 tono <> RE-7")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo incremento di un tono dell'accordo di DO-7")
        Assert.AreEqual("RE-7", nuovoAccordo.ToString, True)
        nuovoAccordo -= Semitono.Value
        Dim DO_7 As Accordo = DO_min.Clone
        DO_7.Estensione = 7
        Assert.AreEqual(DO_7, nuovoAccordo, "RE-7 - 1 semitono <> DO#-7")
        Assert.IsNotNull(nuovoAccordo.Estensione, "Accordo.Estensione nulla dopo decremento di un semitono dell'accordo di RE-7")
        Assert.IsNull(nuovoAccordo.Basso, "Accordo.Basso valorizzato dopo decremento di un semitono dell'accordo di RE-7")
        Assert.AreEqual("DO#-7", nuovoAccordo.ToString, True)
    End Sub

End Class