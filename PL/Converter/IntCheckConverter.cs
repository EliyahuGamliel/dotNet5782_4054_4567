using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class IntCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int num;
            bool error = Int32.TryParse(value.ToString(), out num);
            if (!error)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}