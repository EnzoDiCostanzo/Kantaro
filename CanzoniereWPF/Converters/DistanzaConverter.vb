Public Class DistanzaConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim d = DirectCast(value, Distanza)
        If targetType = GetType(Single) Then
            Return CType(d, Single)
        ElseIf targetType = GetType(Double) Then
            Return CType(d, Double)
        Else
            Throw New InvalidOperationException("Il tipo di destinazione deve essere un numerico")
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim v As Single
        If IsNumeric(value) Then
            Return CType(value, Distanza)
        ElseIf Single.TryParse(value.ToString, v) Then
            Return CType(v, Distanza)
        Else
            Return DependencyProperty.UnsetValue
        End If
    End Function
End Class
