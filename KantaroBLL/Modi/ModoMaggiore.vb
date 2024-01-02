Public Class ModoMaggiore
    Inherits ModoBase

    Private Sub New()
        MyBase.New()
        CreaSuccessioneDistanze(New Generic.List(Of Distanza)({Tono.Value,
                                                              Tono.Value,
                                                              Semitono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Semitono.Value}))
    End Sub

    Public Shared Function GetInstance() As ModoMaggiore
        Return New ModoMaggiore
    End Function

End Class
