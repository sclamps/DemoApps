using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace SquareRt.Core.ValueConverters
{
    public class DoubleToStringValueConverter: IMvxValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString (value);
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return System.Convert.ToDouble (value);
        }
    }
}