using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace PL
{
    class DoubleCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            /*double num;
            bool error = Double.TryParse(value.ToString(), out num);
            if (!error)
                return false;
            return true;*/
            string num = (string)value;
            if (num.Contains("°") && num.Contains("'") && num.Contains("\"")) {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}