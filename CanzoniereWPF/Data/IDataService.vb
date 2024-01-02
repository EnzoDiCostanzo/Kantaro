Imports System.Collections.ObjectModel

Public Interface IDataService

    Function GetCurrentFolderAsync() As Task(Of Uri)
    Function GetCanzoneFromFilePathAsync(filePath As String) As Task(Of Canzone)
    Function GetFileElementsFromFolderAsync(folderPath As String) As Task(Of List(Of FileElement))

    Function CreateIndexAsync(folderPath As String) As Task
End Interface
