using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class BatteryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return "";
            return Math.Round((double)value, 0).ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}