
Imports Microsoft.Extensions.DependencyInjection

Public Class CanzoniViewModel
    Inherits ViewModelBase

    Private _dataService As IDataService

    Public Sub New(dataService As IDataService)
        _dataService = dataService
        FolderPath = _dataService.GetCurrentFolderAsync.Result.LocalPath
    End Sub

#Region " Proprietà di input e output "
    Private _isWorking As Boolean
    Public Property IsWorking As Boolean
        Get
            Return _isWorking
        End Get
        Set(value As Boolean)
            _isWorking = value
            OnPropertyChanged()
        End Set
    End Property

    Private _folderPath As String
    Public Property FolderPath As String
        Get
            Return _folderPath
        End Get
        Set(value As String)
            If Not String.Equals(value, _folderPath) Then
                _folderPath = value
                OnPropertyChanged()
                OnFolderPathChanged()
            End If
        End Set
    End Property

    Private Async Sub OnFolderPathChanged()
        Await LoadFromFolderAsync()
    End Sub

    Private _selectedFileElement As FileElement
    Public Property SelectedFileElement As FileElement
        Get
            Return _selectedFileElement
        End Get
        Set(value As FileElement)
            _selectedFileElement = value
            OnPropertyChanged()
            OnSelectedFileElementChanged()
        End Set
    End Property

    Private Sub OnSelectedFileElementChanged()
        If SelectedFileElement Is Nothing Then Return
        Dim fp = SelectedFileElement.FilePath
        If SelectedFileElement.IsPreviousFolder Then
            FolderPath = IO.Path.GetDirectoryName(FolderPath)
        ElseIf SelectedFileElement.IsContainer Then
            FolderPath = fp
        ElseIf SelectedFileElement.Exists AndAlso Not SelectedFileElement.HasErrors Then
            ' File .kanto da aprire
            Dim ns = My.Application.Services.GetRequiredService(Of INavigationService)
            Dim cvm As New CanzoneViewModel(_dataService) With {
                .FilePath = fp
            }
            If cvm.Canzone IsNot Nothing Then ns.NavigateToSong(cvm)
        End If
    End Sub

    Private Property AllFolderElements As List(Of FileElement)

    Private _folderElements As ComponentModel.ICollectionView
    Public Property FolderElements() As ComponentModel.ICollectionView
        Get
            If _folderElements Is Nothing Then
                _folderElements = CollectionViewSource.GetDefaultView(AllFolderElements)
            End If
            Return _folderElements
        End Get
        Set(value As ComponentModel.ICollectionView)
            _folderElements = value
            OnPropertyChanged()
        End Set
    End Property

    Private _filter As String
    Public Property Filter As String
        Get
            Return If(_filter, String.Empty)
        End Get
        Set(value As String)
            _filter = value
            OnPropertyChanged()
            OnFilterChanged()
        End Set
    End Property
    Private Sub OnFilterChanged()
        FolderElements.Filter = Function(e As FileElement) e.Title.ToLower.Contains(Filter.ToLower) 'IsVisible
        FolderElements.Refresh()
    End Sub
#End Region

#Region " Comandi "
    Private _selectFileElementCommand As RelayCommand(Of FileElement)
    Public Property SelectFileElementCommand As RelayCommand(Of FileElement)
        Get
            If _selectFileElementCommand Is Nothing Then
                _selectFileElementCommand = New RelayCommand(Of FileElement)(Sub(fileElement As FileElement)
                                                                                 SelectedFileElement = fileElement
                                                                             End Sub)
            End If
            Return _selectFileElementCommand
        End Get
        Set(value As RelayCommand(Of FileElement))
            _selectFileElementCommand = value
            OnPropertyChanged()
        End Set
    End Property

    Private _openCommand As RelayCommand
    Public Property OpenCommand As RelayCommand
        Get
            If _openCommand Is Nothing Then
                _openCommand = New RelayCommand(Sub()
                                                    If SelectedFileElement Is Nothing Then Return
                                                    Dim fp = SelectedFileElement.FilePath
                                                    If SelectedFileElement.IsPreviousFolder Then
                                                        FolderPath = IO.Path.GetDirectoryName(FolderPath)
                                                    ElseIf SelectedFileElement.IsContainer Then
                                                        FolderPath = fp
                                                    Else ' File .kanto da aprire
                                                        Dim ns = My.Application.Services.GetRequiredService(Of INavigationService)
                                                        Dim cvm As New CanzoneViewModel(_dataService) With {
                                                            .FilePath = fp
                                                        }
                                                        If cvm.Canzone IsNot Nothing Then ns.NavigateToSong(cvm)
                                                    End If
                                                End Sub,
                                                Function() SelectedFileElement IsNot Nothing)
            End If
            Return _openCommand
        End Get
        Set(value As RelayCommand)
            _openCommand = value
            OnPropertyChanged()
        End Set
    End Property

    Private _refreshCommand As RelayCommand
    Public Property RefreshCommand As RelayCommand
        Get
            If _refreshCommand Is Nothing Then
                _refreshCommand = New RelayCommand(Async Sub()
                                                       Await _dataService.CreateIndexAsync(FolderPath)
                                                       Await LoadFromFolderAsync()
                                                   End Sub)
            End If
            Return _refreshCommand
        End Get
        Set(value As RelayCommand)
            _refreshCommand = value
            OnPropertyChanged()
        End Set
    End Property
#End Region

    Public Async Function LoadFromFolderAsync() As Task
        Dim saveIsWorking = IsWorking
        Try
            IsWorking = True
            If AllFolderElements Is Nothing Then AllFolderElements = New List(Of FileElement)
            AllFolderElements.Clear()
            Dim elements = Await _dataService.GetFileElementsFromFolderAsync(FolderPath)
            AllFolderElements.AddRange(elements)
            ' Aggiorna la vista in caso di filtri attivi
            Filter = Filter
        Finally
            Task.WaitAll()
            IsWorking = saveIsWorking
        End Try
    End Function
End Class
