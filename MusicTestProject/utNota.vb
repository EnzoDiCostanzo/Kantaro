Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class utNota

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
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
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

    <TestMethod()>
    Public Sub VerificaNoteStatiche()
        Assert.AreEqual(Nota.[DO], New Nota(NotaEnum.[DO]), "Nota statica DO non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.DOdiesis, New Nota(NotaEnum.DOdiesis), "Nota statica DO# non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.RE, New Nota(NotaEnum.RE), "Nota statica RE non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.REdiesis, New Nota(NotaEnum.REdiesis), "Nota statica RE# non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.MI, New Nota(NotaEnum.MI), "Nota statica MI non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.FA, New Nota(NotaEnum.FA), "Nota statica FA non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.FAdiesis, New Nota(NotaEnum.FAdiesis), "Nota statica FA# non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.SOL, New Nota(NotaEnum.SOL), "Nota statica SOL non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.SOLdiesis, New Nota(NotaEnum.SOLdiesis), "Nota statica SOL# non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.LA, New Nota(NotaEnum.LA), "Nota statica LA non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.LAdiesis, New Nota(NotaEnum.LAdiesis), "Nota statica LA# non corrisponde a quella del costruttore")
        Assert.AreEqual(Nota.SI, New Nota(NotaEnum.SI), "Nota statica SI non corrisponde a quella del costruttore")
    End Sub

    <TestMethod()>
    Public Sub VerificaNoteStatiche_Bemolle()
        Assert.AreEqual(Nota.REb, Nota.DOdiesis, "Nota statica REb non corrisponde a quella del DO#")
        Assert.AreEqual(Nota.MIb, Nota.REdiesis, "Nota statica MIb non corrisponde a quella del RE#")
        Assert.AreEqual(Nota.SOLb, Nota.FAdiesis, "Nota statica SOLb non corrisponde a quella del FA#")
        Assert.AreEqual(Nota.LAb, Nota.SOLdiesis, "Nota statica LAb non corrisponde a quella del SOL#")
        Assert.AreEqual(Nota.SIb, Nota.LAdiesis, "Nota statica SIb non corrisponde a quella del LA#")
    End Sub

    <TestMethod()>
    Public Sub NoteStringParseTest()
        Dim n As New Nota(NotaEnum.LA)
        Assert.IsTrue(Nota.TryParse("DO", n), "Conversione di ""DO"" non riuscita")
        Assert.AreEqual(n, Nota.DO, "Conversione di ""DO"" non corrisponde alla nota DO")
        Assert.IsTrue(Nota.TryParse("DO#", n), "Conversione di ""DO#"" non riuscita")
        Assert.AreEqual(n, Nota.DOdiesis, "Conversione di ""DO#"" non corrisponde alla nota DO#")
        Assert.IsTrue(Nota.TryParse("REb", n), "Conversione di ""REb"" non riuscita")
        Assert.AreEqual(n, Nota.DOdiesis, "Conversione di ""REb"" non corrisponde alla nota DO#")
    End Sub

    <TestMethod()>
    Public Sub VerificaToString_Note()
        Assert.AreEqual("DO", Nota.DO.ToString, True, "ToString della nota DO non corrisponde")
        Assert.AreEqual("DO#", Nota.DOdiesis.ToString, True, "ToString della nota DO# non corrisponde")
        Assert.AreEqual("RE", Nota.RE.ToString, True, "ToString della nota RE non corrisponde")
        Assert.AreEqual("RE#", Nota.REdiesis.ToString, True, "ToString della nota RE# non corrisponde")
        Assert.AreEqual("MI", Nota.MI.ToString, True, "ToString della nota MI non corrisponde")
        Assert.AreEqual("FA", Nota.FA.ToString, True, "ToString della nota FA non corrisponde")
        Assert.AreEqual("FA#", Nota.FAdiesis.ToString, True, "ToString della nota FA# non corrisponde")
        Assert.AreEqual("SOL", Nota.SOL.ToString, True, "ToString della nota SOL non corrisponde")
        Assert.AreEqual("SOL#", Nota.SOLdiesis.ToString, True, "ToString della nota SOL# non corrisponde")
        Assert.AreEqual("LA", Nota.LA.ToString, True, "ToString della nota LA non corrisponde")
        Assert.AreEqual("LA#", Nota.LAdiesis.ToString, True, "ToString della nota LA# non corrisponde")
        Assert.AreEqual("SI", Nota.SI.ToString, True, "ToString della nota SI non corrisponde")
    End Sub

    <TestMethod()>
    Public Sub VerificaToString_NoteBemolle()
        Assert.AreEqual("REb", Nota.DOdiesis.ToString(True), True, "ToString della nota DO# non corrisponde al REb")
        Assert.AreEqual("MIb", Nota.REdiesis.ToString(True), True, "ToString della nota RE# non corrisponde al MIb")
        Assert.AreEqual("SOLb", Nota.FAdiesis.ToString(True), True, "ToString della nota FA# non corrisponde al SOLb")
        Assert.AreEqual("LAb", Nota.SOLdiesis.ToString(True), True, "ToString della nota SOL# non corrisponde al LAb")
        Assert.AreEqual("SIb", Nota.LAdiesis.ToString(True), True, "ToString della nota LA# non corrisponde al SIb")
    End Sub

    <TestMethod()>
    Public Sub VerificaSaltiNote_dalDO()
        Dim C = Nota.DO
        Dim nuova = C + Semitono.Value
        Assert.AreEqual(Nota.DOdiesis, nuova, "Verifica DO + 1 semitono")
        nuova = C + 2 * Semitono.Value
        Assert.AreEqual(Nota.RE, nuova, "Verifica DO + 2 semitoni")
        nuova = C + 3 * Semitono.Value
        Assert.AreEqual(Nota.REdiesis, nuova, "Verifica DO + 3 semitoni")
        For i = NotaEnum.DO To NotaEnum.SI
            nuova = New Nota(i)
            Assert.AreEqual(nuova, C + CInt(i) / 2, String.Format("Verifica costruzione con salti nota DO + {0}", CInt(i)))
        Next
    End Sub

    <TestMethod()>
    Public Sub VerificaSaltiNote_dalDOdiesis()
        Dim C_ = Nota.DOdiesis
        Dim nuova = C_ + Semitono.Value
        Assert.AreEqual(Nota.[RE], nuova, "Verifica DO# + 1 semitono")
        nuova = C_ + 2 * Semitono.Value
        Assert.AreEqual(Nota.REdiesis, nuova, "Verifica DO# + 2 semitoni")
        nuova = C_ + 3 * Semitono.Value
        Assert.AreEqual(Nota.MI, nuova, "Verifica DO# + 3 semitoni")
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((1 + i) Mod 12)
            Assert.AreEqual(nuova, C_ + CInt(i) / 2, String.Format("Verifica costruzione con salti nota DO# + {0}", CInt(i)))
        Next
    End Sub

    <TestMethod()>
    Public Sub VerificaSaltiNote_dalRE()
        Dim D = Nota.[RE]
        Dim nuova = D + Semitono.Value
        Assert.AreEqual(Nota.REdiesis, nuova, "Verifica RE + 1 semitono")
        nuova = D + 2 * Semitono.Value
        Assert.AreEqual(Nota.MI, nuova, "Verifica RE + 2 semitoni")
        nuova = D + 3 * Semitono.Value
        Assert.AreEqual(Nota.FA, nuova, "Verifica RE + 3 semitoni")
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((D.Valore + i) Mod 12)
            Assert.AreEqual(nuova, D + CInt(i) / 2, String.Format("Verifica costruzione con salti nota RE + {0}", CInt(i)))
        Next
    End Sub

    <TestMethod()>
    Public Sub VerificaSaltiNote_dalREdiesis()
        Dim D_ = Nota.REdiesis
        Dim nuova = D_ + Semitono.Value
        Assert.AreEqual(Nota.MI, nuova, "Verifica RE# + 1 semitono")
        nuova = D_ + 2 * Semitono.Value
        Assert.AreEqual(Nota.FA, nuova, "Verifica RE# + 2 semitoni")
        nuova = D_ + 3 * Semitono.Value
        Assert.AreEqual(Nota.FAdiesis, nuova, "Verifica RE# + 3 semitoni")
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((D_.Valore + i) Mod 12)
            Assert.AreEqual(nuova, D_ + CInt(i) / 2, String.Format("Verifica costruzione con salti nota RE + {0}", CInt(i)))
        Next
    End Sub

    <TestMethod()> Public Sub VerificaSaltiNote_dalMI()
        Dim E = Nota.MI
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((E.Valore + i) Mod 12)
            Assert.AreEqual(nuova, E + CInt(i) / 2, String.Format("Verifica costruzione con salti nota MI + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalFA()
        Dim F = Nota.FA
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((F.Valore + i) Mod 12)
            Assert.AreEqual(nuova, F + CInt(i) / 2, String.Format("Verifica costruzione con salti nota FA + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalFAdiesis()
        Dim F_ = Nota.FAdiesis
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((F_.Valore + i) Mod 12)
            Assert.AreEqual(nuova, F_ + CInt(i) / 2, String.Format("Verifica costruzione con salti nota FA# + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalSOL()
        Dim G = Nota.SOL
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((G.Valore + i) Mod 12)
            Assert.AreEqual(nuova, G + CInt(i) / 2, String.Format("Verifica costruzione con salti nota SOL + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalSOLdiesis()
        Dim G_ = Nota.SOLdiesis
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((G_.Valore + i) Mod 12)
            Assert.AreEqual(nuova, G_ + CInt(i) / 2, String.Format("Verifica costruzione con salti nota SOL# + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalLA()
        Dim A = Nota.LA
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((A.Valore + i) Mod 12)
            Assert.AreEqual(nuova, A + CInt(i) / 2, String.Format("Verifica costruzione con salti nota LA + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalLAdiesis()
        Dim A_ = Nota.LAdiesis
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((A_.Valore + i) Mod 12)
            Assert.AreEqual(nuova, A_ + CInt(i) / 2, String.Format("Verifica costruzione con salti nota LA# + {0}", CInt(i)))
        Next
    End Sub
    <TestMethod()> Public Sub VerificaSaltiNote_dalSI()
        Dim B = Nota.SI
        Dim nuova As Nota
        For i = NotaEnum.[DO] To NotaEnum.SI
            nuova = New Nota((B.Valore + i) Mod 12)
            Assert.AreEqual(nuova, B + CInt(i) / 2, String.Format("Verifica costruzione con salti nota SI + {0}", CInt(i)))
        Next
    End Sub


End Class
