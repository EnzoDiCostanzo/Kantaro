using System;
using System.Globalization;
using System.Windows.Data;

namespace Enzo.Music.KantaroWpf.Converters;


[ValueConversion(typeof(bool), typeof(bool))]
public partial class InvertBoolConverter : IValueConverter
{

    public InvertBoolConverter()
    {

    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null && value is bool)
        {
            return !(bool)value;
        }

        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}
