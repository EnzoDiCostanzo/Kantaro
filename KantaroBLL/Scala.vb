Public Class Scala
    Private _NotaFondamentale As Nota
    Private _Modo As ModoBase

    Public Sub New(notaFondamentale As Nota, modo As ModoBase)
        If notaFondamentale Is Nothing Then Throw New ArgumentNullException("notaFondamentale")
        If modo Is Nothing Then Throw New ArgumentNullException("modo")

        _NotaFondamentale = notaFondamentale
        _Modo = modo
    End Sub

    Public ReadOnly Property NotaFondamentale As Nota
        Get
            Return _NotaFondamentale
        End Get
    End Property
    Public ReadOnly Property Modo As ModoBase
        Get
            Return _Modo
        End Get
    End Property

    Default Public ReadOnly Property Item(index As Integer) As Nota
        Get
            Dim incrementi = index Mod Modo.NumeroSuccessioni
            Dim r As Nota = Me.NotaFondamentale
            For i = 0 To incrementi - 1
                r += Modo.Successioni(i)
            Next
            Return r
        End Get
    End Property

#Region " Verifica uguaglianze "
    Public Shared Operator =(ByVal class1 As Scala, ByVal class2 As Scala) As Boolean
        Return class1.Equals(class2)
    End Operator

    Public Shared Operator <>(ByVal class1 As Scala, ByVal class2 As Scala) As Boolean
        Return Not class1.Equals(class2)
    End Operator

    Public Overrides Function Equals(obj As Object) As Boolean
        If Not TypeOf obj Is Scala Then Return False
        Dim s = DirectCast(obj, Scala)
        Dim bNota = s.NotaFondamentale.Equals(Me.NotaFondamentale)
        Dim bModo = s.Modo.Equals(s.Modo)
        Return bNota AndAlso bModo
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If Not TypeOf obj1 Is Scala Then Return False
        Return DirectCast(obj1, Scala).Equals(obj2)
    End Function
#End Region

    Public Overrides Function ToString() As String
        Return Me.ToString(False)
    End Function

    Public Overloads Function ToString(bemollePrefer As Boolean) As String
        Return Me.NotaFondamentale.ToString(bemollePrefer)
    End Function

    Public Shared Function TryParse(value As String, ByRef result As Scala) As Boolean
        Dim nb As Nota = Nothing
        Dim modo As ModoBase = Nothing
        If value.Length > 3 AndAlso value.EndsWith("dim", StringComparison.CurrentCultureIgnoreCase) Then
            value = value.Substring(0, value.Length - 3)
        End If
        If Not Nota.TryParse(value.TrimEnd("m-+0123456789".ToCharArray), nb) Then Return False
        Dim regExNum As New System.Text.RegularExpressions.Regex("[0-9]+\+*")
        Dim accordoBase = regExNum.Replace(value, String.Empty)
        Dim na As Nota = Nothing
        Nota.TryParse(accordoBase, na)
        If nb.Equals(na) Then
            modo = ModoMaggiore.GetInstance
        Else
            modo = ModoMinoreArmonica.GetInstance
        End If
        result = New Scala(nb, modo)
        Return True
    End Function

End Class
