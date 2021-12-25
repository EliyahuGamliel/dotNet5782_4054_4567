using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace PLCustomer
{
    class LongitudeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double num = (double)value;
            char dir = 'W';
            if (num < 0)
                num = -num;
            else
                dir = 'E';
            int degrees = (int)num;
            num = num - degrees;
            int minutes = (int)(num * 60);
            double rest = num - ((double)minutes / 60);
            double seconds = rest * 3600;
            return $"{degrees}° {minutes}' {Math.Round(seconds, 3)}\" {dir}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}