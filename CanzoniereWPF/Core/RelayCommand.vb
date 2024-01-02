Public Class RelayCommand
    Implements ICommand

    Private ReadOnly _canExecute As Predicate(Of Object)
    Private ReadOnly _execute As Action(Of Object)

    Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object))
        _canExecute = canExecute
        _execute = execute
    End Sub
    Public Sub New(execute As Action(Of Object))
        Me.New(execute, Function() True)
    End Sub

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _execute(parameter)
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return _canExecute(parameter)
    End Function
End Class

Public Class RelayCommand(Of T)
    Implements ICommand

    Private ReadOnly _canExecute As Predicate(Of T)
    Private ReadOnly _execute As Action(Of T)

    Public Sub New(execute As Action(Of T), canExecute As Predicate(Of T))
        _canExecute = canExecute
        _execute = execute
    End Sub
    Public Sub New(execute As Action(Of T))
        Me.New(execute, Function() True)
    End Sub

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _execute(parameter)
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return _canExecute(parameter)
    End Function
End Class
