Public Class ModoMinoreNaturale
    Inherits ModoBase

    Private Sub New()
        CreaSuccessioneDistanze(New Generic.List(Of Distanza)({Tono.Value,
                                                                Semitono.Value,
                                                                Tono.Value,
                                                                Tono.Value,
                                                                Semitono.Value,
                                                                Tono.Value,
                                                                Tono.Value}))
    End Sub

    Public Shared Function GetInstance() As ModoMinoreNaturale
        Return New ModoMinoreNaturale
    End Function
End Class

Public Class ModoMinoreMelodica
    Inherits ModoBase

    Private Sub New()
        CreaSuccessioneDistanze(New Generic.List(Of Distanza)({Tono.Value,
                                                              Semitono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Semitono.Value}))
    End Sub

    Public Shared Function GetInstance() As ModoMinoreMelodica
        Return New ModoMinoreMelodica
    End Function
End Class

Public Class ModoMinoreArmonica
    Inherits ModoBase

    Private Sub New()
        CreaSuccessioneDistanze(New Generic.List(Of Distanza)({Tono.Value,
                                                              Semitono.Value,
                                                              Tono.Value,
                                                              Tono.Value,
                                                              Semitono.Value,
                                                              Tono.Value + Semitono.Value,
                                                              Semitono.Value}))
    End Sub

    Public Shared Function GetInstance() As ModoMinoreArmonica
        Return New ModoMinoreArmonica
    End Function
End Class
