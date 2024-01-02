Public Class Canzone

    Public Property VariazioneInSemitoni As Integer
    Public Property Tag As Object

    Public Shared Async Function FromStreamAsync(stream As System.IO.StreamReader) As Task(Of Canzone)
        If stream Is Nothing Then Throw New ArgumentNullException("stream")
        Return FromXDocument(Await LoadXDocumentAsync(stream).ConfigureAwait(False))
    End Function

    Private Shared Async Function LoadXDocumentAsync(stream As System.IO.StreamReader) As Task(Of XDocument)
        If stream Is Nothing Then Throw New ArgumentNullException("stream")
        Dim t As New Task(Of XDocument)(Function()
                                            Try
                                                Return XDocument.Load(stream)
                                            Catch ex As Exception
                                                Return Nothing
                                            End Try
                                        End Function)
        t.Start()
        Return Await t.ConfigureAwait(False)
    End Function

    Public Shared Function FromXDocument(document As XDocument) As Canzone
        If document Is Nothing Then Return Nothing
        Try
            Dim xSong = document
            Dim song As New Canzone
            With song
                .Titolo = xSong.<canzone>.@title
                .Autore = xSong.<canzone>.@autore
                .VariazioneInSemitoni = xSong.<canzone>.@variazione
                For Each st In xSong.<canzone>.<strofa>
                    If Not String.IsNullOrWhiteSpace(st.@ref) Then
                        .Strofe.Add(New StrofaRipetuta With {.Riferimento = st.@ref})
                        'If .NamedParagraphs.ContainsKey(st.@ref) Then
                        '    .Strofe.Add(.NamedParagraphs(st.@ref).Clone())
                        'End If
                        Continue For
                    End If
                    Dim strofa As New Strofa
                    For Each elem In st.Elements
                        Dim p As Parte = Nothing
                        If elem.Name = "parte" Then
                            p = New Parte()
                            If Not String.IsNullOrEmpty(elem.@accordo) Then p.Accordo = CType(elem.@accordo, Accordo)
                            If Not elem.IsEmpty Then p.Testo = elem.Value
                        End If
                        strofa.Parti.Add(p)
                    Next
                    If Not String.IsNullOrWhiteSpace(st.@name) Then
                        strofa.Nome = st.@name
                        '.NamedParagraphs.Add(st.@name, strofa)
                    End If
                    .Strofe.Add(strofa)
                Next
            End With
            Return song
        Catch ex As Exception
            Throw New Exception(ex.Message, ex) With {.Source = document.ToString}
        End Try
    End Function

    Public Function ToXml() As XDocument
        Dim d As New XDocument()
        Dim root = <canzone/>
        If Me.Titolo IsNot Nothing Then root.Add(New XAttribute("title", Me.Titolo))
        If Me.Autore IsNot Nothing Then root.Add(New XAttribute("autore", Me.Autore))
        If Me.VariazioneInSemitoni <> 0 Then root.Add(New XAttribute("variazione", Me.VariazioneInSemitoni))
        For Each st In Strofe
            Dim strofa = <strofa/>
            If TypeOf st Is StrofaRipetuta Then
                strofa.Add(New XAttribute("ref", DirectCast(st, StrofaRipetuta).Riferimento))
            Else
                For Each p In st.Parti
                    If p Is Nothing Then
                        strofa.Add(<br/>)
                    Else
                        Dim parte = <parte/>
                        If p.Accordo IsNot Nothing Then
                            parte.Add(New XAttribute("accordo", p.Accordo.ToString))
                        End If
                        If p.Testo IsNot Nothing Then
                            parte.Value = p.Testo
                        End If
                        strofa.Add(parte)
                    End If
                Next
            End If
            root.Add(strofa)
        Next
        d.Add(root)
        Return d
    End Function

    Public Property Titolo As String
    Public Property Autore As String

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

    Private _strofe As IList(Of Strofa)
    Public ReadOnly Property Strofe As IList(Of Strofa)
        Get
            If _strofe Is Nothing Then
                _strofe = New List(Of Strofa)
            End If
            Return _strofe
        End Get
    End Property

    Public Function GetStrofe() As IList(Of Strofa)
        Dim ret As New List(Of Strofa)
        For Each s In Strofe
            Dim n As Strofa = DirectCast(s, ICloneable).Clone
            If VariazioneInSemitoni <> 0 Then
                Dim dist As New Distanza With {.Semitoni = VariazioneInSemitoni}
                For Each p In n.Parti
                    If p Is Nothing Then Continue For
                    If p.Accordo IsNot Nothing Then
                        p.Accordo = p.Accordo + dist
                    End If
                Next
            End If
            ret.Add(n)
        Next
        Return ret
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If obj1 Is Nothing AndAlso obj2 Is Nothing Then Return True
        If Not TypeOf obj1 Is Canzone Then Return False
        Return DirectCast(obj1, Canzone).Equals(obj2)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then Return False
        If Not TypeOf obj Is Canzone Then Return False
        Dim b = DirectCast(obj, Canzone)
        Dim uguali = String.Equals(Me.Autore, b.Autore) AndAlso
                     String.Equals(Me.Titolo, b.Titolo) AndAlso
                     Integer.Equals(Me.VariazioneInSemitoni, b.VariazioneInSemitoni)
        If uguali Then uguali = (Me.Strofe.Count = b.Strofe.Count)
        If uguali Then
            For i = 0 To Strofe.Count - 1
                uguali = Strofe(i).Equals(b.Strofe(i))
                If Not uguali Then Exit For
            Next
        End If
        Return uguali
    End Function

    Public Shared Operator <>(ByVal left As Canzone, ByVal right As Canzone) As Boolean
        Return Not Canzone.Equals(left, right)
    End Operator

    Public Shared Operator =(ByVal left As Canzone, ByVal right As Canzone) As Boolean
        Return Canzone.Equals(left, right)
    End Operator
End Class
