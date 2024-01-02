Public Class Accordo
    Implements ICloneable

    Private _scala As Scala
    Public Property Basso As Nota
    Private _estensione As EstensioneT
    'Private _formaPreferita As FormaPreferita

    Public Sub New(scala As Scala)
        Me.New(scala, EstensioneT.Empty, Nothing)
    End Sub

    Private Sub New(scala As Scala, testoPreferito As String)
        Me.New(scala)
        '_formaPreferita = New FormaPreferita With {.Scala = scala, .Testo = testoPreferito}
    End Sub

    Public Sub New(scala As Scala, estensione As EstensioneT)
        Me.New(scala, estensione, Nothing)
    End Sub

    Public Sub New(scala As Scala, num As Integer)
        Me.New(scala, num, EstensioneT.EstensioneVariazioneSemitonoEnum.None, Nothing)
    End Sub

    Public Sub New(scala As Scala, num As Integer, varEstensione As EstensioneT.EstensioneVariazioneSemitonoEnum)
        Me.New(scala, num, varEstensione, Nothing)
    End Sub

    Public Sub New(scala As Scala, num As Integer, varEstensione As EstensioneT.EstensioneVariazioneSemitonoEnum, basso As Nota)
        _scala = scala
        If num > 0 Then
            Dim ext As New EstensioneT
            ext.Valore = num
            ext.VariazioneSemitono = varEstensione
            Me.Estensione = ext
        End If
        Me.Basso = basso
    End Sub

    Public Sub New(scala As Scala, estensione As EstensioneT, basso As Nota)
        _scala = scala
        Me.Estensione = estensione
        Me.Basso = basso
    End Sub

    Public ReadOnly Property Scala As Scala
        Get
            Return _scala
        End Get
    End Property

    Private Structure FormaPreferita
        Public Property Scala As Scala
        Public Property Testo As String
    End Structure

    Public Class EstensioneT
        Public Enum EstensioneVariazioneSemitonoEnum
            None = 0
            Diminuito = 1
            Aumentato = 2
        End Enum

        Public Property Valore As Integer
        Public Property VariazioneSemitono As EstensioneVariazioneSemitonoEnum

        Public Shared ReadOnly Property Empty As EstensioneT
            Get
                Return New EstensioneT()
            End Get
        End Property

        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return Me.Valore = 0
            End Get
        End Property

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If Not TypeOf (obj) Is EstensioneT Then Return False
            Dim o = DirectCast(obj, EstensioneT)
            Return o.Valore = Me.Valore AndAlso o.VariazioneSemitono = Me.VariazioneSemitono
        End Function

        Public Overrides Function ToString() As String
            Dim sb As New System.Text.StringBuilder
            If Valore > 0 Then sb.Append(Valore)
            If Me.VariazioneSemitono = EstensioneVariazioneSemitonoEnum.Aumentato Then sb.Append("+")
            If Me.VariazioneSemitono = EstensioneVariazioneSemitonoEnum.Diminuito Then sb.Append("dim")
            Return sb.ToString()
        End Function

        Public Shared Widening Operator CType(value As Integer) As EstensioneT
            Dim e As New EstensioneT
            e.Valore = value
            Return e
        End Operator

        Public Shared Function TryParse(value As String, ByRef result As EstensioneT) As Boolean
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Dim r = New EstensioneT
            If Not String.IsNullOrWhiteSpace(value) Then
                Dim regEst As New System.Text.RegularExpressions.Regex("^[0-9]+")
                If Not regEst.IsMatch(value) Then Return False
                r.Valore = CInt(regEst.Match(value).Value)
                Select Case value.Remove(0, regEst.Match(value).Value.Length)
                    Case "+"
                        r.VariazioneSemitono = EstensioneVariazioneSemitonoEnum.Aumentato
                    Case "dim"
                        r.VariazioneSemitono = EstensioneVariazioneSemitonoEnum.Diminuito
                    Case Else
                        r.VariazioneSemitono = EstensioneVariazioneSemitonoEnum.None
                End Select
            End If
            result = r
            Return True
        End Function

        Friend Sub New()

        End Sub

        Public Sub New(valore As Integer, varSemitono As EstensioneVariazioneSemitonoEnum)
            Me.Valore = valore
            Me.VariazioneSemitono = varSemitono
        End Sub
    End Class

    Public Property Estensione As EstensioneT
        Get
            Return If(_estensione Is Nothing OrElse _estensione.IsEmpty, Nothing, _estensione)
        End Get
        Set(value As EstensioneT)
            _estensione = value
        End Set
    End Property

#Region " Definizione degli Operatori "
    Public Shared Operator +(ByVal accordo1 As Accordo, ByVal distanza As Distanza) As Accordo
        If distanza.Semitoni = 0 Then Return accordo1
        Dim n = New Accordo(New Scala(accordo1.Scala.NotaFondamentale + distanza, accordo1.Scala.Modo), accordo1.Estensione)
        If accordo1.Basso IsNot Nothing Then n.Basso = accordo1.Basso + distanza

        Return n
    End Operator
    Public Shared Operator -(ByVal accordo1 As Accordo, ByVal distanza As Distanza) As Accordo
        If distanza.Semitoni = 0 Then Return accordo1
        Dim n = New Accordo(New Scala(accordo1.Scala.NotaFondamentale - distanza, accordo1.Scala.Modo), accordo1.Estensione)
        If accordo1.Basso IsNot Nothing Then n.Basso = accordo1.Basso - distanza

        Return n
    End Operator

    Public Shared Operator =(ByVal class1 As Accordo, ByVal class2 As Accordo) As Boolean
        If class1 Is Nothing Then Return False
        Return class1.Equals(class2)
    End Operator

    Public Shared Operator <>(ByVal class1 As Accordo, ByVal class2 As Accordo) As Boolean
        If class1 Is Nothing Then Return False
        Return Not class1.Equals(class2)
    End Operator
#End Region

    Public Overrides Function Equals(obj As Object) As Boolean
        If Not TypeOf obj Is Accordo Then Return False
        Dim a = DirectCast(obj, Accordo)
        Dim bScala = a.Scala.Equals(Scala)
        Dim bBasso = ((Basso Is Nothing AndAlso a.Basso Is Nothing) OrElse
                      (Basso IsNot Nothing AndAlso a.Basso IsNot Nothing AndAlso Basso.Equals(a.Basso)))
        Dim bAlter = ((Estensione Is Nothing AndAlso a.Estensione Is Nothing) OrElse
                      (Estensione IsNot Nothing AndAlso a.Estensione IsNot Nothing AndAlso Estensione.Equals(a.Estensione)))
        Return bScala AndAlso bAlter AndAlso bBasso AndAlso bAlter
    End Function

    Public Overloads Shared Function Equals(obj1 As Object, obj2 As Object) As Boolean
        If obj1 Is Nothing AndAlso obj2 Is Nothing Then Return True
        If Not TypeOf obj1 Is Accordo Then Return False
        Return DirectCast(obj1, Accordo).Equals(obj2)
    End Function

    Public Shared Widening Operator CType(value As String) As Accordo
        Dim a As Accordo = Nothing
        TryParse(value, a)
        Return a
    End Operator

    Public Shared Function TryParse(value As String, ByRef result As Accordo) As Boolean
        If value Is Nothing Then Throw New ArgumentNullException("text")
        Dim parti = value.Split("/")
        Dim sc As Scala = Nothing
        If Not Scala.TryParse(parti(0), sc) Then Throw New InvalidCastException(String.Format("Testo non convertibile in Accordo: {0}", parti(0)))

        Dim acc As New Accordo(sc, value)
        If parti(0).Remove(0, sc.ToString.Length).Length > 0 Then
            Dim temp = parti(0).Remove(0, sc.ToString.Length)
            Dim ext As New EstensioneT
            Dim num = New String(temp.ToCharArray.TakeWhile(Function(c As Char) Char.IsDigit(c)).ToArray)
            Dim valNumb As Integer
            If Integer.TryParse(num, valNumb) Then ext.Valore = valNumb
            If num <> temp Then
                Select Case temp.Remove(0, num.Length)
                    Case "+" : ext.VariazioneSemitono = EstensioneT.EstensioneVariazioneSemitonoEnum.Aumentato
                    Case "dim" : ext.VariazioneSemitono = EstensioneT.EstensioneVariazioneSemitonoEnum.Diminuito
                    Case Else : ext.VariazioneSemitono = EstensioneT.EstensioneVariazioneSemitonoEnum.None
                End Select
            End If
            acc.Estensione = ext
        End If
        If parti.Length > 1 Then
            Dim basso As Nota = Nothing
            If parti.Length <> 2 OrElse Not Nota.TryParse(parti(1), basso) Then Throw New InvalidCastException(String.Format("Testo non convertibile in Accordo: {0}", parti(1)))
            acc.Basso = basso
        End If
        result = acc
        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Me.ToString(False)
    End Function

    Public Overloads Function ToString(bemollePrefer As Boolean) As String
        Dim sb As New System.Text.StringBuilder
        'If _formaPreferita.Scala IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(_formaPreferita.Testo) AndAlso Scala.Equals(_formaPreferita.Scala) Then
        '    sb.Append(_formaPreferita.Testo)
        'Else
        sb.Append(Me.Scala.ToString(bemollePrefer))
        If TypeOf Scala.Modo Is ModoMinoreArmonica Then
            sb.Append("-")
        End If
        If Estensione IsNot Nothing Then
            sb.Append(Estensione.ToString)
        End If
        If Me.Basso IsNot Nothing Then
            sb.Append("/")
            sb.Append(Me.Basso.ToString)
        End If
        'End If
        Return sb.ToString()
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim n As New Accordo(Scala, Me.Estensione, Me.Basso)
        'n._formaPreferita = _formaPreferita
        Return n
    End Function
End Class
