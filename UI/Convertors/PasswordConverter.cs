using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Convertors
{
    public class PasswordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Here, you can modify the value that is being displayed in the data grid.
            // In this case, we'll just return an empty string to hide the password.
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // We don't need to do anything in this method, so we'll just return null.
            return null;
        }
    }
}
