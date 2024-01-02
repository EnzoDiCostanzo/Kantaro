Imports GalaSoft.MvvmLight
Imports GalaSoft.MvvmLight.Ioc
Imports GalaSoft.MvvmLight.Messaging
Imports Microsoft.Practices.ServiceLocation

'  In App.xaml
'  <Application.Resources>
'      <vm:ViewModelLocator xmlns:vm="clr-namespace:Enzo.Canzoniere" x:Key="Locator" />
'  </Application.Resources>
'
'  In the View
'  DataContext = "{Binding Source={StaticResource Locator}, Path=ViewModelName}"
'
'  You can also use Blend To Do all this With the tool's support.
'  See http : //www.galasoft.ch/mvvm

Public Class ViewModelLocator


    Public Sub New()
        ServiceLocator.SetLocatorProvider(Function() SimpleIoc.Default)

        If ViewModelBase.IsInDesignModeStatic Then
            ' Create design time view services and models
            SimpleIoc.Default.Register(Of IDataService, DesignDataService)()
        Else
            ' Create run time view services and models
            SimpleIoc.Default.Register(Of IDataService, DataService)()
        End If
        SimpleIoc.Default.Register(Of CanzoniViewModel)()
        SimpleIoc.Default.Register(Of CanzoneViewModel)()
    End Sub

    Public ReadOnly Property Canzone() As CanzoneViewModel
        Get
            Return ServiceLocator.Current.GetInstance(Of CanzoneViewModel)()
        End Get
    End Property

    Public ReadOnly Property Canzoni() As CanzoniViewModel
        Get
            Return ServiceLocator.Current.GetInstance(Of CanzoniViewModel)()
        End Get
    End Property

    Public Shared Sub Cleanup()
        ' TODO Clear the ViewModels
    End Sub
End Class
