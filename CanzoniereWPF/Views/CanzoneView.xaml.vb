Class CanzoneView
    Public Sub New(myViewModel As CanzoneViewModel)
        Me.InitializeComponent()
        GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance(Of CanzoneViewModel).Canzone = myViewModel.Canzone
    End Sub
End Class
