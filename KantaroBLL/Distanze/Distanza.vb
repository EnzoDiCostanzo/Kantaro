<DebuggerDisplay("{GetText}")>
Public Class Distanza

    Private _valore As Single ' in semitoni

    ''' <summary>
    '''  Valore che indica la distanza in Toni
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Toni As Single
        Get
            Return _valore / 2.0!
        End Get
    End Property

    ''' <summary>
    ''' Valore che indica la distanza in semitoni
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Semitoni As Integer
        Get
            Return _valore
        End Get
        Set(value As Integer)
            _valore = value
        End Set
    End Property

    Private ReadOnly Property GetText() As String
        Get
            Return String.Format("{0}", Me.Toni)
        End Get
    End Property

    ''' <summary>
    ''' Rappresenta il valore della distanza espresso in Toni
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Valore As Single
        Get
            Return _valore / 2.0!
        End Get
        Set(value As Single)
            If (2 * value) Mod 1 <> 0 Then
                Throw New ArgumentOutOfRangeException("Impossibile convertire il valore in Toni e Semitoni")
            End If
            _valore = value * 2.0!
        End Set
    End Property

    Public Shared Operator +(ByVal class1 As Distanza, ByVal class2 As Distanza) As Distanza
        Dim r = New Distanza()
        r.Valore = class1.Valore + class2.Valore
        Return r
    End Operator

    Public Shared Operator +(ByVal class1 As Distanza, ByVal valore As Integer) As Distanza
        Dim r = New Distanza()
        r.Valore = class1.Valore + valore
        Return r
    End Operator

    Public Shared Operator +(ByVal class1 As Distanza, ByVal valore As Double) As Distanza
        Dim r = New Distanza()
        r.Valore = class1.Valore + CSng(valore)
        Return r
    End Operator

    Public Shared Operator *(ByVal class1 As Distanza, ByVal moltiplicatore As Integer) As Distanza
        Dim d As New Distanza
        d.Valore = class1.Valore * moltiplicatore
        Return d
    End Operator

    Public Shared Operator *(ByVal moltiplicatore As Integer, ByVal class1 As Distanza) As Distanza
        Dim d As New Distanza
        d.Valore = moltiplicatore * class1.Valore
        Return d
    End Operator

    Public Shared Operator =(ByVal class1 As Distanza, ByVal class2 As Distanza) As Boolean
        Return class1.Equals(class2)
    End Operator

    Public Shared Operator <>(ByVal class1 As Distanza, ByVal class2 As Distanza) As Boolean
        Return Not class1.Equals(class2)
    End Operator

    Public Overrides Function Equals(obj As Object) As Boolean
        If Not TypeOf obj Is Distanza Then Return False
        Return DirectCast(obj, Distanza).Valore.Equals(Valore)
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If Not TypeOf obj1 Is Distanza Then Return False
        Return DirectCast(obj1, Distanza).Equals(obj2)
    End Function

    Public Overrides Function ToString() As String
        Return Me.Valore.ToString()
    End Function

    Public Shared Narrowing Operator CType(ByVal initialData As Double) As Distanza
        Return CType(CSng(initialData), Distanza)
    End Operator

    Public Shared Narrowing Operator CType(ByVal initialData As Integer) As Distanza
        Return CType(CSng(initialData), Distanza)
    End Operator

    Public Shared Narrowing Operator CType(ByVal initialData As Single) As Distanza
        Dim r = New Distanza()
        Try
            r.Valore = initialData
        Catch ex As ArgumentOutOfRangeException
            Throw New InvalidCastException("Impossibile convertire il valore in un oggetto di tipo 'Distanza'")
        End Try
        Return r
    End Operator

    Public Shared Widening Operator CType(ByVal initialData As Distanza) As Single
        Return initialData.Valore
    End Operator

    Public Shared Narrowing Operator CType(ByVal initialData As String) As Distanza
        Return CType(Single.Parse(initialData), Distanza)
    End Operator

    Public Shared Widening Operator CType(ByVal initialData As Distanza) As String
        Return initialData.Valore.ToString()
    End Operator

End Class
