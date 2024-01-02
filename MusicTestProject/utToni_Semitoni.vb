Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class utToni_Semitoni

    <TestMethod()>
    Public Sub TestToni()
        Dim t1 = Tono.Value
        Dim t2 = Tono.Value
        Assert.AreEqual(t1, t2)
        Assert.AreSame(t1, t2)
        Dim d = t1 + t2
        Assert.IsTrue(d.Valore = 2)
    End Sub

    <TestMethod()>
    Public Sub TestSemitoni()
        Dim v1 = Semitono.Value
        Dim v2 = Semitono.Value
        Assert.AreEqual(v1, v2)
        Assert.AreSame(v1, v2)
        Assert.AreEqual(v1 + v2, Tono.Value)
        Dim d = v1 + v2
        Assert.AreEqual(1.0!, d.Valore)
        d = v1 * 2 + v2
        Assert.AreEqual(1.5!, d.Valore)
        d = 2 * v1 + v2
        Assert.AreEqual(1.5!, d.Valore)
    End Sub

End Class
