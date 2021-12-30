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
    class PhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not null) {
                string num = value.ToString();
                int check;
                bool error = true;
                if (num.Length != 14 || num[0] != '+' || num[1] != '9' || num[2] != '7' || num[3] != '2' || num[4] != '-' || num[5] != '5')
                    error = false;
                else {
                    string output = num.Substring(num.IndexOf("+") + 6, 4);
                    error = Int32.TryParse(output, out check);
                    if (error) {
                        output = num.Substring(num.IndexOf("+") + 10, 4);
                        error = Int32.TryParse(output, out check);
                    }
                }
                if (!error)
                    return "+972-5????????";
                return num;
            }
            return "+972-5????????";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string num = value.ToString();
            int check;
            bool error = true;
            if (num.Length != 14 || num[0] != '+' || num[1] != '9' || num[2] != '7' || num[3] != '2' || num[4] != '-' || num[5] != '5')
                error = false;
            else {
                string output = num.Substring(num.IndexOf("+") + 6, 4);
                error = Int32.TryParse(output, out check);
                if (error) {
                    output = num.Substring(num.IndexOf("+") + 10, 4);
                    error = Int32.TryParse(output, out check);
                }
            }
            if (!error)
                return "+972-5????????";
            return num;
        }
    }
}