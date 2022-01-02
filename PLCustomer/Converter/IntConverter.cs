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
    class IntConverter : IValueConverter
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