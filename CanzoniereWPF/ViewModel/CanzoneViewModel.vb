Imports System.ComponentModel

Public Class CanzoneViewModel
    Inherits ViewModelBase

    Private _Canzone As Canzone
    Private _dataService As IDataService

    Public Sub New(dataService As IDataService)
        _dataService = dataService
    End Sub

    Private _filePath As String
    Public Property FilePath As String
        Get
            Return _filePath
        End Get
        Set(value As String)
            _filePath = value
            OnPropertyChanged()
            Dim t = Task.Run(Async Function()
                                 Await OnFilePathChangedAync()
                                 Return True
                             End Function)
            t.Wait()
        End Set
    End Property

    Private Async Function OnFilePathChangedAync() As Task
        If FilePath IsNot Nothing Then
            Canzone = Await _dataService.GetCanzoneFromFilePathAsync(FilePath)
        Else
            Canzone = Nothing
        End If
    End Function

    Public Property Canzone As Canzone
        Get
            Return _Canzone
        End Get
        Set(value As Canzone)
            _Canzone = value
            OnPropertyChanged()
            OnPropertyChanged("VariazioneInSemitoni")
        End Set
    End Property

    Public Property VariazioneInSemitoni As Integer
        Get
            Return If(_Canzone Is Nothing, 0, _Canzone.VariazioneInSemitoni)
        End Get
        Set(value As Integer)
            _Canzone.VariazioneInSemitoni = value
            OnPropertyChanged()
            OnPropertyChanged("Canzone")
        End Set
    End Property

    Private _preferredZoom As Double
    Public Property PreferredZoom As Double
        Get
            Return _preferredZoom
        End Get
        Set(value As Double)
            _preferredZoom = value
            OnPropertyChanged()
        End Set
    End Property
End Class
