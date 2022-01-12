using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return "Gray";
            else if ((double)value > 60)
                return "Green";
            else if ((double)value > 20)
                return "Yellow";
            return "Red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}