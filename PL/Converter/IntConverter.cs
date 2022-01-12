using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not null) {
                int num;
                bool error = Int32.TryParse(value.ToString(), out num);
                if (!error)
                    return null;
                return num;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not null) {
                int num;
                bool error = Int32.TryParse(value.ToString(), out num);
                if (!error)
                    return null;
                return num;
            }
            return null;
        }
    }
}