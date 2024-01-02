using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Enzo.Music.KantaroWpf.Converters;

public partial class DistanzaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Distanza d = (Distanza)value;
        if (targetType == typeof(float))
        {
            return (object)(float)d;
        }
        else if (targetType == typeof(double))
        {
            return (object)(double)d;
        }
        else
        {
            throw new InvalidOperationException("Il tipo di destinazione deve essere un numerico");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (Microsoft.VisualBasic.Information.IsNumeric(value))
        {
            return (Distanza)value;
        }
        else if (float.TryParse(value.ToString(), out float v))
        {
            return (Distanza)v;
        }
        else
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
