Public Class Canzoni
    Inherits DependencyObject

    Public Class KantoItem
        Public Property FileName As String
        Public Property Canzone As Canzone

        Public ReadOnly Property Description As String
            Get
                Return If(Canzone IsNot Nothing, Canzone.Description, IO.Path.GetFileNameWithoutExtension(FileName))
            End Get
        End Property
    End Class

    Private _canzoni As ObjectModel.ObservableCollection(Of KantoItem)

    ''' <summary>
    ''' Crea una istanza con una lista vuota di canzoni
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _canzoni = New ObjectModel.ObservableCollection(Of KantoItem)
    End Sub

    ''' <summary>
    ''' Crea una istanza con la lista di canzoni
    ''' </summary>
    ''' <param name="fileKantoj">File con estenzione .kantoj con l'elenco di canzoni</param>
    ''' <remarks></remarks>
    Public Sub New(fileKantoj As String)
        Me.New()
        Dim xDoc = XDocument.Load(fileKantoj)
        Dim canti = From k In xDoc...<kanto>
                    Let filePath = IO.Path.Combine(IO.Path.GetDirectoryName(fileKantoj), k.Value)
                    Let EsiteFile = IO.File.Exists(filePath)
                    Select New KantoItem() With {.FileName = filePath,
                                                 .Canzone = If(EsiteFile, Canzone.FromStream(New IO.StreamReader(filePath)), Nothing)}
        SetFrom(canti)
        Me.FilePath = fileKantoj
    End Sub

    ''' <summary>
    ''' Crea una istanza con la lista di canzoni
    ''' </summary>
    ''' <param name="fileKanto">Singolo file .kanto</param>
    ''' <param name="canzone">Canzone istanziata dal file</param>
    ''' <remarks></remarks>
    Public Sub New(fileKanto As String, canzone As Canzone)
        Me.New()
        SetFrom({New KantoItem() With {.FileName = fileKanto, .Canzone = canzone}})
    End Sub

    ''' <summary>
    ''' Percorso del file .kantoj associato alla lista di canzoni
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilePath As String

    Private Sub SetFrom(canzoni As IEnumerable(Of KantoItem))
        For Each c In canzoni
            _canzoni.Add(c)
        Next
        If _canzoni.Count > 0 Then
            CanzoneAttiva = _canzoni(0).Canzone
        End If
        Me.Index = 0
    End Sub

    Private Property Index As Integer

    Public Property CanzoneAttiva As Canzone
        Get
            Return GetValue(CanzoneAttivaProperty)
        End Get
        Set(ByVal value As Canzone)
            SetValue(CanzoneAttivaProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CanzoneAttivaProperty As DependencyProperty = _
                           DependencyProperty.Register("CanzoneAttiva", _
                           GetType(Canzone), GetType(Canzoni), _
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property IsTheFirst As Boolean
        Get
            Return Index <= 0
        End Get
    End Property

    Public ReadOnly Property IsTheLast As Boolean
        Get
            Return Index >= All.Count - 1
        End Get
    End Property

    Public ReadOnly Property CanGoPrevious As Boolean
        Get
            Return _canzoni.Count > 0 AndAlso Not IsTheFirst
        End Get
    End Property
    Public ReadOnly Property CanGoNext As Boolean
        Get
            Return _canzoni.Count > 0 AndAlso Not IsTheLast
        End Get
    End Property

    Public Sub GoToPrevious()
        If IsTheFirst Then Return
        Index -= 1
        CanzoneAttiva = All(Index).Canzone
    End Sub

    Public Sub GoToNext()
        If IsTheLast Then Return
        Index += 1
        CanzoneAttiva = All(Index).Canzone
    End Sub

    Public ReadOnly Property All() As ObjectModel.ObservableCollection(Of KantoItem)
        Get
            If _canzoni Is Nothing Then
                _canzoni = New ObjectModel.ObservableCollection(Of KantoItem)
            End If
            If CanzoneAttiva Is Nothing AndAlso _canzoni.Count > 0 Then
                CanzoneAttiva = _canzoni(0).Canzone
            End If
            Return _canzoni
        End Get
    End Property
End Class
