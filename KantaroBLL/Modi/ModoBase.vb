Public MustInherit Class ModoBase

    Protected Sub CreaSuccessioneDistanze(lista As Generic.IList(Of Distanza))
        _successioni = New System.Collections.ObjectModel.ReadOnlyCollection(Of Distanza)(lista)
    End Sub

    Protected _successioni As System.Collections.ObjectModel.ReadOnlyCollection(Of Distanza)
    Public ReadOnly Property Successioni As System.Collections.ObjectModel.ReadOnlyCollection(Of Distanza)
        Get
            Return _successioni
        End Get
    End Property

    Public ReadOnly Property NumeroSuccessioni As Integer
        Get
            Return Successioni.Count
        End Get
    End Property
End Class
