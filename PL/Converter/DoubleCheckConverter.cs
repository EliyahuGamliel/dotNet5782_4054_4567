using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class DoubleCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not null) {
                double num;
                bool error = Double.TryParse(value.ToString(), out num);
                if (!error)
                    return false;
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}