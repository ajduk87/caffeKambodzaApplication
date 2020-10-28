using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace caffeKambodzaApplication
{
     [ValueConversion(typeof(String), typeof(String))]
    public class StringConverterHistory : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(Constants.PRODUCT)) return "proizvod kafića";
            else return "stavka magacina";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "ConvertBack";
        }


    }
}
