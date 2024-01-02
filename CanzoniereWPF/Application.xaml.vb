Imports Microsoft.Extensions.DependencyInjection

Class Application

    Sub New()
        Dim services As IServiceCollection
        services = New ServiceCollection()
        services.AddSingleton(Of CanzoniViewModel)
        services.AddSingleton(Of CanzoneViewModel)
        services.AddSingleton(Of INavigationService, NavigationService)
        Me.Services = services.BuildServiceProvider()
    End Sub

    Public ReadOnly Property Services As IServiceProvider

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        Try
            If e.Args.Count > 0 Then
                If IO.File.Exists(e.Args(0)) Then
                    Me.Properties("fileToOpen") = e.Args(0)
                End If
            End If
            AddJumpList()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore in avvio programma", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Public ReadOnly Property NavigationWindowView() As NavigationWindow
        Get
            Return DirectCast(Me.MainWindow, NavigationWindow)
        End Get
    End Property

    Private Sub AddJumpList()
        Dim jl = Shell.JumpList.GetJumpList(My.Application)
        Try
            For Each f In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory, FileIO.SearchOption.SearchAllSubDirectories, "*.kantoj")
                Dim itm As New Shell.JumpPath()
                itm.CustomCategory = "Liste di canzoni"
                itm.Path = f
                jl.JumpItems.Add(itm)
            Next
        Finally
            jl.Apply()
        End Try
    End Sub
End Class
