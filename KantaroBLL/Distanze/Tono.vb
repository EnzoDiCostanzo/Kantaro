Public Class Tono
    Inherits Distanza

    Private Sub New()
        MyBase.New()
        MyBase.Valore = 1
    End Sub

    Private Shared _tono As Tono
    Public Shared ReadOnly Property Value As Tono
        Get
            If _tono Is Nothing Then _tono = New Tono
            Return _tono
        End Get
    End Property
End Class
