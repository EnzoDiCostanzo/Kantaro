Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTestDistanza

    <TestMethod()> Public Sub TestMethod1()
        Dim d1 As New Distanza()
        d1.Valore = 1.5
        Dim d2 = 3 * Semitono.Value
        Assert.AreEqual(d1, d2, "(1t + 1st) <> 3 st")
    End Sub
    <TestMethod()> Public Sub TestMethod2()
        Dim d1 As New Distanza()
        d1.Valore = 1.5!
        Dim d2 = d1 + 6
        Assert.AreEqual(15, d2.Semitoni, "(1t + 1st) + 6 t <> 15st")
    End Sub

    <TestMethod()> Public Sub TestMethod3()
        Dim d1 As New Distanza()
        d1.Valore = 1.5!
        Dim d2 As New Distanza() With {.Valore = 3}
        Dim r = d1 + d2
        Diagnostics.Debug.Assert(r.Valore = 4.5, "(1t + 1st) + 3t")
    End Sub

    <TestMethod> Public Sub TestValore1()
        Dim d As New Distanza
        Dim valore = 1.0!
        d.Valore = valore
        Assert.AreEqual(d.Toni, 1.0!, "Valore di Distanza 1 non ha portato il risultato d.Toni=1")
        Assert.AreEqual(d.Semitoni, 2, "Valore di Distanza 1 non ha portato il risultato d.NumSemitoni=2")
        valore = 3.5!
        d.Valore = valore
        Assert.AreEqual(d.Toni, 3.5!, "Valore di Distanza 3.5 non ha portato il risultato d.Toni=3,5")
        Assert.AreEqual(d.Semitoni, 7, "Valore di Distanza 3.5 non ha portato il risultato d.NumSemitoni=7")
    End Sub

    <TestMethod> Public Sub TestValore2()
        Dim d As New Distanza
        Dim valore = 1.0!
        d.Valore = valore
        Assert.AreEqual(valore, d.Valore, "Valore di Distanza 1 diverso dopo l'assegnazione")
        valore = 3.5!
        d.Valore = valore
        Assert.AreEqual(valore, d.Valore, "Valore di Distanza 3.5 diverso dopo l'assegnazione")
    End Sub

    <TestMethod> Public Sub CastFromNumber1()
        Dim d1 = Tono.Value * 2 + Semitono.Value
        Dim d2 = CType(2.5, Distanza)
        Assert.IsTrue(d1.Semitoni = 5 AndAlso d1.Valore = 2.5, "Valore e NumSemitoni non corretto dopo conversione da 2.5")
        Assert.AreEqual(Of Distanza)(d1, d2, "Valore di Distanza diverso da quello previsto per 2.5")
    End Sub

    <TestMethod> Public Sub CastWithExceptionFromNumber()
        Try
            Dim d = CType(2.7, Distanza)
            Assert.Fail("Non ha dato errore la conversione da 2.7 a Distanza")
        Catch ex As InvalidCastException
            Dim d2 = CType(3, Distanza)
        End Try
    End Sub

    <TestMethod> Public Sub CastToNumber2()
        Dim d = CType(2.5, Distanza)
        Assert.IsTrue(CType(d, Single) = 2.5, "Valore di ritorno 'CType(d, Distanza) = 2.5' non riscontrato")
        d = CType(2, Distanza)
        Assert.IsTrue(CType(d, Single) = 2, "Valore di ritorno 'CType(d, Distanza) = 2.0' non riscontrato")
    End Sub

    <TestMethod> Public Sub CastFromString()
        Dim d1 = CType("2", Distanza)
        Assert.IsTrue(d1.Valore = 2.0!, "Valore non corretto dopo conversione da ""2""")
        Dim d2 = CType("2,5", Distanza)
        Assert.IsTrue(d2.Valore = 2.5!, "Valore non corretto dopo conversione da ""2,5""")
    End Sub

    <TestMethod> Public Sub CastToString()
        Try
            Dim d As New Distanza
            Dim expected As Single
            d.Valore = 3.5!
            expected = 3.5!
            Assert.AreEqual(expected.ToString(), d.ToString(), "Valore di Distanza 3.5 diverso da ""3,5""")
            d.Valore = 1
            expected = 1.0!
            Assert.AreEqual(expected.ToString(), d.ToString(), "Valore di Distanza 1 diverso da ""1""")
        Catch ex As InvalidCastException
            Dim d2 = CType(3, Distanza)
        End Try
    End Sub

End Class