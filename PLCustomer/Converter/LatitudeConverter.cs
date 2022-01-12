using System;
using System.Globalization;
using System.Windows.Data;

namespace PLCustomer
{
    internal class LatitudeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double num;
            if (value is not null && Double.TryParse(value.ToString(), out num)) {
                char dir = 'S';
                if (num < 0)
                    num = -num;
                else
                    dir = 'N';
                int degrees = (int)num;
                num = num - degrees;
                int minutes = (int)(num * 60);
                double rest = num - ((double)minutes / 60);
                double seconds = rest * 3600;
                return $"{degrees}° {minutes}' {Math.Round(seconds, 3)}\" {dir}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            double num;
            if (value is not null && Double.TryParse(value.ToString(), out num)) {
                return num;
            }
            return null;
        }
    }
}