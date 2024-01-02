Imports System.ComponentModel
Imports Enzo.Music

Public Interface INavigationService
    Event Navigating(s As Object, e As CancelEventArgs)
    Sub NavigateTo(pageUri As Uri)
    Sub NavigateToSong(viewModel As CanzoneViewModel)
    Sub GoBack()
End Interface

Public Class NavigationService
    Implements INavigationService

    Private _mainFrame As NavigationWindow

#Region " Implementation Of INavigationService "

    Public Event Navigating(s As Object, e As CancelEventArgs) Implements INavigationService.Navigating

    Public Sub NavigateTo(pageUri As Uri) Implements INavigationService.NavigateTo

        If EnsureMainFrame() Then
            _mainFrame.Navigate(pageUri)
        End If
    End Sub

    Public Sub NavigateToSong(viewModel As CanzoneViewModel) Implements INavigationService.NavigateToSong
        If EnsureMainFrame() Then
            Dim v As New CanzoneView(viewModel)
            _mainFrame.Navigate(v)
        End If
    End Sub

    Public Sub GoBack() Implements INavigationService.GoBack
        If (EnsureMainFrame() AndAlso _mainFrame.CanGoBack) Then
            _mainFrame.GoBack()
        End If
    End Sub

#End Region

    Private Function EnsureMainFrame() As Boolean
        If _mainFrame IsNot Nothing Then
            Return True
        End If

        _mainFrame = DirectCast(System.Windows.Application.Current.MainWindow, NavigationWindow)

        If _mainFrame IsNot Nothing Then
            ' Could be Nothing if the app runs inside a design tool
            AddHandler _mainFrame.Navigating, Sub(s As Object, e As NavigatingCancelEventArgs)
                                                  RaiseEvent Navigating(s, e)
                                              End Sub
            Return True
        End If

        Return False
    End Function
End Class
