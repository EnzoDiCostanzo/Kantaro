Public Class CanzoneDTO
    Property Autore As String
    Property Titolo As String


    ''' <summary>
    ''' Descrizione della canzone (costituita dal titolo + l'autore se presente)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Description As String
        Get
            Dim sb As New Text.StringBuilder
            If Titolo IsNot Nothing Then sb.Append(Titolo)
            If Not String.IsNullOrWhiteSpace(Autore) Then
                sb.Append(" (")
                sb.Append(Autore)
                sb.Append(")")
            End If
            Return sb.ToString
        End Get
    End Property

    Private _strofe As IList(Of StrofaDTO)
    Public ReadOnly Property Strofe As IList(Of StrofaDTO)
        Get
            If _strofe Is Nothing Then
                _strofe = New List(Of StrofaDTO)
            End If
            Return _strofe
        End Get
    End Property
End Class
