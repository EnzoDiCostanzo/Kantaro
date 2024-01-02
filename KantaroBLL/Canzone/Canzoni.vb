Public Class Canzoni

    Public Class CanzoniItem
        Public Property FilePath As String
        Public Property Title As String
    End Class

    Private _canzoni As New List(Of CanzoniItem)

    Public Sub New()

    End Sub

    Public Sub New(fileKantoj As String)
        Dim xDoc = XDocument.Load(fileKantoj)
        _canzoni.AddRange(FromXDocument(xDoc, IO.Path.GetDirectoryName(fileKantoj)).Items)
    End Sub

    ''' <summary>
    ''' Restituisce la classe Canzoni relativa all'xml del file kantoj
    ''' </summary>
    ''' <param name="document">Oggetto XDocument della lista di canzoni</param>
    ''' <param name="basePath">Percorso del file system dove è contenuto il file d'origine</param>
    ''' <returns></returns>
    Public Shared Function FromXDocument(document As XDocument, basePath As String) As Canzoni
        Dim kj As New Canzoni()
        Dim canti = From k In document...<kanto>
                    Let filePath = IO.Path.Combine(basePath, k.Value)
                    Select New CanzoniItem() With {.FilePath = filePath, .Title = k.@title}
        kj._canzoni.AddRange(canti)
        Return kj
    End Function

    Public Function ToXDocument() As XDocument
        Dim doc = <?xml version="1.0" encoding="utf-8"?><kantoj/>
        For Each c In Items
            Dim k = <kanto><%= c.FilePath %></kanto>
            If Not String.IsNullOrEmpty(c.Title) Then
                k.@title = c.Title
            End If
            If doc.Root.LastNode Is Nothing Then
                doc.Root.AddFirst(k)
            Else
                doc.Root.LastNode.AddAfterSelf(k)
            End If
        Next
        Return doc
    End Function

    Public ReadOnly Property Items As IEnumerable(Of CanzoniItem)
        Get
            Return _canzoni
        End Get
    End Property

    Public Sub Add(pathCanzone As String, title As String)
        _canzoni.Add(New CanzoniItem() With {.FilePath = pathCanzone, .Title = title})
    End Sub

    Public Sub Add(item As CanzoniItem)
        _canzoni.Add(item)
    End Sub
End Class
