Public Class StrofeFlowDocumentConverter
    Implements IValueConverter

    Private Const LinkPrefix As String = "kanto://accordo#"

    Private ReadOnly Property LinkFormat As String
        Get
            Dim sb As New Text.StringBuilder
            sb.Append(StrofeFlowDocumentConverter.LinkPrefix)
            sb.Append("{0}")
            Return sb.ToString
        End Get
    End Property

    Public Shared Function GetAccordoFromUri(navigateUri As Uri) As String
        If navigateUri Is Nothing Then Return Nothing
        ' Verifica la corretta valorizzazione dell'uri passato come parametro
        If Not navigateUri.AbsolutePath.StartsWith(StrofeFlowDocumentConverter.LinkPrefix) Then
            Throw New InvalidCastException("Unrecognized uri format")
        End If
        Return navigateUri.AbsolutePath.Remove(0, LinkPrefix.Length)
    End Function

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If value Is Nothing OrElse Not (TypeOf value Is IList(Of Strofa)) Then Return DependencyProperty.UnsetValue
        If targetType <> GetType(FlowDocument) Then
            Throw New InvalidOperationException("Il tipo di destinazione deve essere un FlowDocument")
        End If
        Dim doc As New FlowDocument
        Dim strofe = DirectCast(value, IList(Of Strofa))
        For Each strofa In strofe
            Dim b As New Paragraph()
            If strofa.Nome IsNot Nothing Then
                b.Tag = strofa.Nome
                b.FontStyle = Windows.FontStyles.Italic
            End If
            If TypeOf strofa Is StrofaRipetuta Then
                b.Padding = New Thickness(10, 5, 0, 5)
                b.FontWeight = FontWeights.Bold
                b.FontStyle = FontStyles.Italic
                Dim lnk As New Hyperlink(New Run(DirectCast(strofa, StrofaRipetuta).Riferimento))
                b.Inlines.Add(lnk)
            Else
                For Each p In strofa.Parti
                    Dim tb As TextElement
                    If p IsNot Nothing Then
                        If p.Accordo Is Nothing Then
                            tb = New Run(p.Testo)
                        Else
                            tb = New Hyperlink(New Run(p.Testo))
                            DirectCast(tb, Hyperlink).NavigateUri = New Uri(String.Format(LinkFormat, p.Accordo.ToString))
                        End If
                    Else
                        tb = New LineBreak
                    End If
                    b.Inlines.Add(tb)
                Next
            End If
            doc.Blocks.Add(b)
        Next
        Return doc
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return DependencyProperty.UnsetValue
    End Function
End Class
