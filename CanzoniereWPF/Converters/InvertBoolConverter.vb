Imports System.Globalization

<ValueConversion(GetType(Boolean), GetType(Boolean))>
Public Class InvertBoolConverter
    Implements IValueConverter

    Public Sub New()

    End Sub

    Private Function IValueConverter_Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        If value IsNot Nothing AndAlso TypeOf value Is Boolean Then
            Return Not CBool(value)
        End If

        Return True
    End Function

    Private Function IValueConverter_ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return IValueConverter_Convert(value, targetType, parameter, culture)
    End Function
End Class