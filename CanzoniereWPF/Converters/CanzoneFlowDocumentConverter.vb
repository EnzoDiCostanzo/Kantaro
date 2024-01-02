Public Class CanzoneFlowDocumentConverter
    Implements IValueConverter

#Region " Proprietà per la rappresentazione grafica del documento "
    Public Property TitoloFontSize As Integer = 14
    Public Property TitoloFontStyle As FontStyle = FontStyles.Normal
    Public Property TitoloFontWeight As FontWeight = FontWeights.Bold
    Public Property AutoreFontSize As Integer = 12
    Public Property AutoreFontStyle As FontStyle = FontStyles.Italic
    Public Property AutoreFontWeight As FontWeight = FontWeights.Regular
#End Region

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing OrElse Not (TypeOf value Is Canzone) Then Return DependencyProperty.UnsetValue
        If targetType <> GetType(FlowDocument) Then
            Throw New InvalidOperationException("Il tipo di destinazione deve essere un FlowDocument")
        End If
        Dim strofeDict As New Generic.Dictionary(Of String, Strofa)

        Dim doc As New FlowDocument
        Dim song = DirectCast(value, Canzone)
        If Not String.IsNullOrWhiteSpace(song.Titolo) Then
            Dim par As New Paragraph() With {.FontSize = TitoloFontSize, .FontStyle = TitoloFontStyle, .FontWeight = TitoloFontWeight}
            par.Inlines.Add(song.Titolo)
            doc.Blocks.Add(par)
        End If
        If Not String.IsNullOrWhiteSpace(song.Autore) Then
            Dim par As New Paragraph() With {.FontSize = AutoreFontSize, .FontStyle = AutoreFontStyle, .FontWeight = AutoreFontWeight}
            par.Inlines.Add(song.Autore)
            doc.Blocks.Add(par)
        End If
        Dim noteTrovate = (From strofa In song.GetStrofe()
                           From p In strofa.Parti
                           Where p IsNot Nothing AndAlso p.Accordo IsNot Nothing
                           Select p.Accordo.Scala.NotaFondamentale).ToArray
        Dim noteNaturaliTrovate = (From n In noteTrovate Where n.IsNaturale Select n.Valore).Distinct.ToArray
        Dim altreNoteTrovate = (From n In noteTrovate Where Not n.IsNaturale Select n.Valore).Distinct.ToArray
        Dim noteBemolli As New Generic.Dictionary(Of NotaEnum, Boolean)
        For Each nota In altreNoteTrovate
            noteBemolli.Add(nota, False)
        Next
        Dim trovatoBemolle As Boolean
        Do
            trovatoBemolle = False
            For Each nota In altreNoteTrovate
                Dim nuovaNota = New Nota(nota)
                Dim existItsNat = noteNaturaliTrovate.Contains((nuovaNota - Semitono.Value).Valore) OrElse
                                  noteBemolli.ContainsKey((nuovaNota - Tono.Value).Valore) AndAlso noteBemolli((nuovaNota - Tono.Value).Valore)
                If Not nuovaNota.IsNaturale AndAlso existItsNat AndAlso Not noteBemolli(nota) Then
                    noteBemolli(nota) = True
                    trovatoBemolle = True
                End If
            Next
        Loop While trovatoBemolle
        For Each strofa In song.GetStrofe()
            Dim b As New Paragraph()
            If Not String.IsNullOrWhiteSpace(strofa.Nome) Then
                If Not strofeDict.ContainsKey(strofa.Nome) Then strofeDict.Add(strofa.Nome, strofa)
            End If
            Dim strofaDaConvertire As Strofa
            If TypeOf strofa Is StrofaRipetuta Then
                strofaDaConvertire = strofeDict(DirectCast(strofa, StrofaRipetuta).Riferimento)
            Else
                strofaDaConvertire = strofa
            End If
            For Each p In strofaDaConvertire.Parti
                If p IsNot Nothing Then
                    Dim testoAccordo As String
                    If p.Accordo Is Nothing Then
                        testoAccordo = String.Empty
                    Else
                        Dim nuovaNota = p.Accordo.Scala.NotaFondamentale
                        Dim bemolle = noteBemolli.ContainsKey(nuovaNota.Valore) AndAlso noteBemolli(nuovaNota.Valore)
                        Dim partiConAccordiSimili = From parte In strofaDaConvertire.Parti
                                                    Where parte IsNot Nothing AndAlso
                                                    parte.Accordo IsNot Nothing AndAlso
                                                    parte.Accordo.Scala IsNot Nothing AndAlso
                                                    parte.Accordo.Scala.NotaFondamentale = nuovaNota
                                                    Select parte
                        If bemolle AndAlso partiConAccordiSimili.FirstOrDefault IsNot Nothing Then
                            bemolle = False
                        End If
                        testoAccordo = p.Accordo.ToString(bemolle)
                    End If
                    Dim tb As New TextBlock()
                    tb.Inlines.Add(testoAccordo & vbCrLf & p.Testo)
                    b.Inlines.Add(tb)
                Else
                    b.Inlines.Add(vbCrLf)
                End If
            Next
            doc.Blocks.Add(b)
        Next
        Return doc
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DependencyProperty.UnsetValue
    End Function
End Class
