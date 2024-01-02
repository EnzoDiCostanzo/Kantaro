Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class utCanzone

    <TestMethod()> Public Sub TestEqualsSuCanzone()
        Dim a = GetCanzone(4, 4, True)
        Dim b = GetCanzone(4, 4, True)
        Assert.AreEqual(a, a, "La stessa canzone non risulta uguale a se stessa")
        Assert.AreEqual(b, a, "Canzoni identiche non risultano uguali")
        ' Controllo autore
        a.Autore = "Modificato"
        Assert.AreNotEqual(b, a, "Canzoni con Autore diverso risultano uguali")
        ' Controllo titolo
        a = GetCanzone(4, 4, True)
        a.Titolo = "Modificato"
        ' Controllo titolo
        a = GetCanzone(4, 4, True)
        a.VariazioneInSemitoni = 3
        Assert.AreNotEqual(b, a, "Canzoni con VariazioneInSemitoni diversi risultano uguali")
        ' Controllo numero Strofe
        a = GetCanzone(4, 4, True)
        a.Strofe.Remove(a.Strofe(3))
        Assert.AreNotEqual(b, a, "Canzoni con strofe diverse risultano uguali")
    End Sub

    Private Function GetCanzone(numeroStrofe As Integer, numeroParti As Integer, conRit As Boolean) As Canzone
        Dim c As New Canzone
        c.Titolo = "Titolo della canzone"
        c.Autore = "Autore della canzone"
        Dim rit As Strofa = Nothing
        If conRit Then
            rit = New Strofa() With {.Nome = "rit", .Parti = GetParti(numeroParti)}
            c.Strofe.Add(rit)
        End If
        Dim num As Integer
        If conRit Then
            num = numeroStrofe \ 2
        Else
            num = numeroStrofe
        End If
        For i = 1 To num
            c.Strofe.Add(New Strofa() With {.Parti = GetParti(numeroParti)})
            If conRit Then c.Strofe.Add(New StrofaRipetuta() With {.Riferimento = "rit"})
        Next
        Return c
    End Function

    Private Function GetParti(numero As Integer) As IList(Of Parte)
        Dim r As New List(Of Parte)
        For i = 1 To numero
            Dim p As New Parte
            p.Testo = String.Format("Parte n°{0}", numero)
            r.Add(p)
        Next
        Return r
    End Function

End Class