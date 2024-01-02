<DebuggerDisplay("{GetText}")> Public Class Nota
    Public Valore As NotaEnum

    Public Sub New(valore As NotaEnum)
        If valore < NotaEnum.[DO] OrElse valore > NotaEnum.SI Then Throw New IndexOutOfRangeException()
        Me.Valore = valore
    End Sub

    Public ReadOnly Property IsNaturale As Boolean
        Get
            Return {NotaEnum.DO, NotaEnum.RE, NotaEnum.MI, NotaEnum.FA, NotaEnum.SOL, NotaEnum.LA, NotaEnum.SI}.Contains(Me.Valore)
        End Get
    End Property

#Region " Istanze statiche di tutte le note "
    Public Shared ReadOnly Property [DO] As Nota
        Get
            Return New Nota(NotaEnum.[DO])
        End Get
    End Property
    Public Shared ReadOnly Property DOdiesis As Nota
        Get
            Return New Nota(NotaEnum.DOdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property REb As Nota
        Get
            Return New Nota(NotaEnum.DOdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property RE As Nota
        Get
            Return New Nota(NotaEnum.RE)
        End Get
    End Property
    Public Shared ReadOnly Property REdiesis As Nota
        Get
            Return New Nota(NotaEnum.REdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property MIb As Nota
        Get
            Return New Nota(NotaEnum.REdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property MI As Nota
        Get
            Return New Nota(NotaEnum.MI)
        End Get
    End Property
    Public Shared ReadOnly Property FA As Nota
        Get
            Return New Nota(NotaEnum.FA)
        End Get
    End Property
    Public Shared ReadOnly Property FAdiesis As Nota
        Get
            Return New Nota(NotaEnum.FAdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property SOLb As Nota
        Get
            Return New Nota(NotaEnum.FAdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property SOL As Nota
        Get
            Return New Nota(NotaEnum.SOL)
        End Get
    End Property
    Public Shared ReadOnly Property SOLdiesis As Nota
        Get
            Return New Nota(NotaEnum.SOLdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property LAb As Nota
        Get
            Return New Nota(NotaEnum.SOLdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property LA As Nota
        Get
            Return New Nota(NotaEnum.LA)
        End Get
    End Property
    Public Shared ReadOnly Property LAdiesis As Nota
        Get
            Return New Nota(NotaEnum.LAdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property SIb As Nota
        Get
            Return New Nota(NotaEnum.LAdiesis)
        End Get
    End Property
    Public Shared ReadOnly Property SI As Nota
        Get
            Return New Nota(NotaEnum.SI)
        End Get
    End Property
#End Region

#Region " Definizione degli Operatori "
    Public Shared Operator +(ByVal nota As Nota, ByVal distanza As Distanza) As Nota
        If distanza.Valore < 0 Then Return nota - New Distanza() With {.Valore = -1 * distanza.Valore}
        Dim n = (nota.Valore + distanza.Semitoni) Mod 12 ' le note vanno da 0 ad 11
        Return New Nota(n)
    End Operator
    Public Shared Operator -(ByVal nota As Nota, ByVal distanza As Distanza) As Nota
        Dim n = (nota.Valore + 12 - distanza.Semitoni) Mod 12 ' le note vanno da 0 ad 11
        Return New Nota(n)
    End Operator

    Public Shared Operator =(ByVal class1 As Nota, ByVal class2 As Nota) As Boolean
        If class1 Is Nothing Then Return False
        Return class1.Equals(class2)
    End Operator

    Public Shared Operator <>(ByVal class1 As Nota, ByVal class2 As Nota) As Boolean
        Return Not class1.Equals(class2)
    End Operator
#End Region

    Private ReadOnly Property GetText() As String
        Get
            Return Me.Valore.ToString()
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj IsNot Nota Then Return False
        Dim n = DirectCast(obj, Nota)
        Return n.Valore.Equals(Valore)
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If TypeOf obj1 IsNot Nota Then Return False
        Return DirectCast(obj1, Nota).Equals(obj2)
    End Function

    Public Overrides Function ToString() As String
        Return Me.ToString(False)
    End Function

    Public Overloads Function ToString(bemollePrefer As Boolean) As String
        Dim sb As New Text.StringBuilder
        If IsNaturale Then
            Dim txtNota = Me.Valore.ToString

            sb.Append(txtNota.Substring(0, 1).ToUpper)
            sb.Append(txtNota.Substring(1).ToLower)
        Else
            If bemollePrefer Then
                Dim succ = Me + Semitono.Value
                sb.Append(succ.ToString)
                sb.Append("b")
            Else
                Dim prec = Me - Semitono.Value
                sb.Append(prec.ToString)
                sb.Append("#")
            End If
        End If
        Return sb.ToString()
    End Function

    Public Shared Function TryParse(value As String, ByRef result As Nota) As Boolean
        Dim n As NotaEnum
        Dim testo As String
        If value.EndsWith("b") Then
            If Not [Enum].TryParse(value.Substring(0, value.Length - 1).ToUpper, n) Then Return False
            testo = String.Format("{0}", New Nota(n) - 1 / 2).ToUpper
        Else
            testo = value.ToUpper
        End If
        If Not [Enum].TryParse(testo.Replace("#", "diesis"), n) Then Return False
        result = New Nota(n)
        Return True
    End Function

End Class
