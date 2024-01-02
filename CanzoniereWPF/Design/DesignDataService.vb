Imports System.Collections.ObjectModel
Imports Enzo.Music

Public Class DesignDataService
    Implements IDataService

    Public Async Function CreateIndexAsync(folderPath As String) As Task Implements IDataService.CreateIndexAsync
        Await Task.Run(Sub()
                           If 1 = 0 Then
                               ' DoNothing
                           End If
                       End Sub)
        Return
    End Function

    Public Function GetFileElementsFromFolderAsync(folderPath As String) As Task(Of List(Of FileElement)) Implements IDataService.GetFileElementsFromFolderAsync
        Dim ret As New List(Of FileElement) From {
            New FileElement() With {.FileName = "..", .FilePath = "c:\", .IsFolder = True, .IsPreviousFolder = True, .Title = ".."},
            New FileElement() With {.FileName = "Cartella d'esempio", .FilePath = "c:\temp\folder1", .IsFolder = True, .Title = "folder1"},
            New FileElement() With {.FileName = "kantoj1", .FilePath = "c:\temp\file1.kantoj", .IsListOfFiles = True, .Title = "file1.kantoj"},
            New FileElement() With {.FileName = "kanto1", .FilePath = "c:\temp\file1.kanto", .Title = "Alleluia (Toronto 2002)"},
            New FileElement() With {.FileName = "kanto2", .FilePath = "c:\temp\file2.kanto", .Title = "Altra canzone della lista con nome molto lungo"},
            New FileElement() With {.FileName = "kanto3", .FilePath = "c:\temp\file3.kanto", .Title = "Canzone con errori", .HasErrors = True},
            New FileElement() With {.FileName = "kanto4", .FilePath = "c:\temp\file4.kanto", .Title = "File mancante", .Exists = False}
        }
        Return Task.FromResult(ret)
    End Function

    Public Async Function GetCurrentFolderAsync() As Task(Of Uri) Implements IDataService.GetCurrentFolderAsync
        Return Await Task.FromResult(New Uri(IO.Directory.GetCurrentDirectory()))
    End Function

    Public Async Function GetCanzoneFromFilePathAsync(filePath As String) As Task(Of Canzone) Implements IDataService.GetCanzoneFromFilePathAsync
        Return Await Task.FromResult(New Canzone() With {.Titolo = "Canzone 1", .Autore = "Autore 1"})
    End Function
End Class
