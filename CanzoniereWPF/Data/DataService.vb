Imports System.Collections.ObjectModel
Imports System.IO
Imports Enzo.Music

Public Class DataService
    Implements IDataService

    Private Const INDEX_FILE_NAME As String = "index.kantoj"

    Public Async Function CreateIndexAsync(folderPath As String) As Task Implements IDataService.CreateIndexAsync
        Dim fileName = Path.Combine(folderPath, INDEX_FILE_NAME)
        Dim canzoni As New Dictionary(Of String, Canzone)
        For Each f In Directory.GetFiles(folderPath, "*.kanto")
            canzoni.Add(f, Await GetCanzoneFromFilePathAsync(f))
        Next
        If canzoni.Count = 0 AndAlso File.Exists(fileName) Then
            Await Task.Run(Sub() File.Delete(fileName))
        Else
            Dim canti = New Canzoni
            For Each dicElem In canzoni
                canti.Add(dicElem.Key, If(dicElem.Value?.Titolo, String.Empty))
            Next
            If canti.Items.Count > 0 Then
                Dim x = canti.ToXDocument
                Await Task.Run(Sub()
                                   Using writer = Xml.XmlWriter.Create(fileName)
                                       x.WriteTo(writer)
                                   End Using
                               End Sub)
            End If
        End If
    End Function

    Public Async Function GetFileElementsFromFolderAsync(folderPath As String) As Task(Of List(Of FileElement)) Implements IDataService.GetFileElementsFromFolderAsync
        Dim ret As New List(Of FileElement)
        If folderPath Is Nothing Then Return ret
        If Path.GetPathRoot(folderPath) <> folderPath Then
            ret.Add(New FileElement() With {.FileName = "..", .IsFolder = True, .IsPreviousFolder = True, .FilePath = folderPath, .Title = ".."})
        End If
        If Path.GetExtension(folderPath) = ".kantoj" Then
            ret.AddRange(Await GetFileElementsFromKantojAsync(folderPath))
            Return ret
        Else
            Try
                If Directory.Exists(folderPath) Then
                    For Each f In IO.Directory.GetDirectories(folderPath)
                        Dim fn = Path.GetFileName(f)
                        Dim e As New FileElement With {.FileName = fn, .Title = fn, .FilePath = f, .IsFolder = True, .Exists = True}
                        ret.Add(e)
                    Next
                    Dim trovatoIndexFile = False
                    For Each f In IO.Directory.GetFiles(folderPath, "*.kantoj")
                        If Path.GetFileName(f) = INDEX_FILE_NAME Then
                            trovatoIndexFile = True
                            Continue For
                        End If
                        Dim fn = Path.GetFileNameWithoutExtension(f)
                        Dim e As New FileElement With {.FileName = fn, .Title = fn, .FilePath = f, .IsListOfFiles = True, .Exists = True}
                        ret.Add(e)
                    Next
                    Dim indexFilePath = Path.Combine(folderPath, INDEX_FILE_NAME)
                    Dim kantoFiles = Directory.GetFiles(folderPath, "*.kanto")
                    If trovatoIndexFile AndAlso kantoFiles.Count > 0 Then
                        Dim kantoFileElements = Await GetFileElementsFromKantojAsync(indexFilePath)
                        Dim lastChange = kantoFiles.Max(Function(f) File.GetLastWriteTimeUtc(f))
                        If kantoFileElements.Count <> kantoFiles.Count OrElse lastChange > File.GetLastWriteTimeUtc(indexFilePath) Then
                            Await CreateIndexAsync(folderPath)
                            kantoFileElements = Await GetFileElementsFromKantojAsync(indexFilePath)
                        End If
                        ret.AddRange(kantoFileElements.OrderBy(Function(e) e.Title))
                    Else
                        ret.AddRange(GetFileElementsFromKantoFiles(kantoFiles).OrderBy(Function(e) e.Title))
                        Await CreateIndexAsync(folderPath)
                    End If
                End If
            Catch ex As Exception
            End Try
            Return ret
        End If
    End Function

    Private Async Function GetFileElementsFromKantojAsync(kantojFilePath As String) As Task(Of IEnumerable(Of FileElement))
        Dim ret As IEnumerable(Of FileElement) = Nothing
        Await Task.Run(Sub()
                           Dim xDoc = XDocument.Load(kantojFilePath)
                           Dim dirPath = Path.GetDirectoryName(kantojFilePath)
                           Dim kantoFiles = From k In xDoc...<kanto>
                                            Select Path.Combine(dirPath, k.Value)
                           ret = GetFileElementsFromKantoFiles(kantoFiles)
                       End Sub)
        If ret Is Nothing Then Return New List(Of FileElement)
        Return New List(Of FileElement)(ret)
    End Function

    Private Function GetFileElementsFromKantoFiles(kantoFiles As IEnumerable(Of String)) As IEnumerable(Of FileElement)
        Dim ret As New List(Of FileElement)
        Dim canti = From fn In kantoFiles
                    Select New FileElement() With {.FilePath = fn, .FileName = Path.GetFileNameWithoutExtension(fn),
                            .Exists = File.Exists(fn)}
        Parallel.ForEach(canti, Sub(c)
                                    If c.Exists Then
                                        Try
                                            Dim xSong = XDocument.Load(c.FilePath)
                                            Dim sb As New Text.StringBuilder()
                                            sb.Append(xSong.<canzone>.@title)
                                            If xSong.<canzone>.@autore IsNot Nothing Then
                                                sb.Append(" (").Append(xSong.<canzone>.@autore).Append(")")
                                            End If
                                            c.Title = sb.ToString()
                                        Catch ex As Exception
                                            c.Title = c.FileName
                                            c.HasErrors = True
                                        End Try
                                    Else
                                        c.Title = c.FileName
                                    End If
                                    ret.Add(c)
                                End Sub)
        Parallel.ForEach(ret, Sub(c) Task.Run(Sub()
                                                  Try
                                                      If c IsNot Nothing AndAlso Canzone.FromXDocument(XDocument.Load(c.FilePath)) Is Nothing Then
                                                          c.HasErrors = True
                                                      End If
                                                  Catch ex As Exception
                                                      c.HasErrors = True
                                                  End Try
                                              End Sub))
        Return ret
    End Function

    Public Async Function GetCurrentFolderAsync() As Task(Of Uri) Implements IDataService.GetCurrentFolderAsync
        Return New Uri(Await Task.FromResult(Directory.GetCurrentDirectory()))
    End Function

    Public Async Function GetCanzoneFromFilePathAsync(filePath As String) As Task(Of Canzone) Implements IDataService.GetCanzoneFromFilePathAsync
        Try
            Return Await Canzone.FromStreamAsync(New IO.StreamReader(filePath))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
