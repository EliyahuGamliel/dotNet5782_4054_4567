﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    internal class LongitudeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double num;
            if (value is not null && Double.TryParse(value.ToString(), out num)) {
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