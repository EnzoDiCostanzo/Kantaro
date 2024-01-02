Public Class Parte
    Implements ICloneable

    Public Property Accordo As Accordo
    Public Property Testo As String

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim p As New Parte
        If Accordo IsNot Nothing Then p.Accordo = Accordo.Clone
        If Testo IsNot Nothing Then p.Testo = Testo
        Return p
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then Return False
        If Not TypeOf obj Is Parte Then Return False
        Dim b = DirectCast(obj, Parte)
        Dim uguali = String.Equals(Testo, b.Testo) AndAlso Accordo.Equals(Accordo, b.Accordo)
        Return uguali
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If obj1 Is Nothing AndAlso obj2 Is Nothing Then Return True
        If Not TypeOf obj1 Is Parte Then Return False
        Return DirectCast(obj1, Parte).Equals(obj2)
    End Function

    Public Shared Operator =(a As Parte, b As Parte) As Boolean
        Return Parte.Equals(a, b)
    End Operator

    Public Shared Operator <>(a As Parte, b As Parte) As Boolean
        Return Not Parte.Equals(a, b)
    End Operator
End Class
