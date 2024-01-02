Public MustInherit Class ViewModelBase
    Inherits ObservableObject

    Public Shared ReadOnly Property IsInDesignModeStatic As Boolean
        Get
#If DEBUG Then
            Return True
#Else
            Return False
#End If
        End Get
    End Property
End Class
