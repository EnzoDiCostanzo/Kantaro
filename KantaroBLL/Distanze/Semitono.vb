Public Class Semitono
    Inherits Distanza

    Private Sub New()
        MyBase.New()
        MyBase.Valore = 0.5
    End Sub

    Private Shared _semitono As Semitono
    Public Shared ReadOnly Property Value As Semitono
        Get
            If _semitono Is Nothing Then _semitono = New Semitono
            Return _semitono
        End Get
    End Property
End Class
