Imports Enzo.Music

Public Class Strofa
    Implements ICloneable

    Public Property Nome As String
    Public Property Parti As New Generic.List(Of Parte)

    Public Overridable Function Clone() As Object Implements ICloneable.Clone
        Dim n As New Strofa
        n.CopyFrom(Me)
        Return n
    End Function

    Protected Sub CopyFrom(strofa As Strofa)
        Nome = strofa.Nome
        For Each p In strofa.Parti
            If p IsNot Nothing Then
                Parti.Add(p.Clone)
            Else
                Parti.Add(Nothing)
            End If
        Next
    End Sub

    Public Overrides Function Equals(other As Object) As Boolean
        If other Is Nothing Then Return False
        If Not TypeOf other Is Strofa Then Return False
        Dim b = DirectCast(other, Strofa)
        Dim uguali = String.Equals(Me.Nome, b.Nome) AndAlso
                     Me.Parti.Count = b.Parti.Count
        If uguali Then
            For i = 0 To Parti.Count - 1
                uguali = If(Parti(i)?.Equals(b.Parti(i)), False)
                If Not uguali Then Exit For
            Next
        End If
        Return uguali
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If obj1 Is Nothing AndAlso obj2 Is Nothing Then Return True
        If Not TypeOf obj1 Is Strofa Then Return False
        Return DirectCast(obj1, Strofa).Equals(obj2)
    End Function

    Public Shared Operator =(a As Strofa, b As Strofa) As Boolean
        Return Strofa.Equals(a, b)
    End Operator

    Public Shared Operator <>(a As Strofa, b As Strofa) As Boolean
        Return Not Strofa.Equals(a, b)
    End Operator
End Class

Public Class StrofaRipetuta
    Inherits Strofa

    ''' <summary>
    ''' Nome della strofa da ripetere
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Riferimento As String

    Public Overrides Function Clone() As Object
        Dim c As New StrofaRipetuta
        c.CopyFrom(Me)
        c.Riferimento = Me.Riferimento
        Return c
    End Function
End Class
