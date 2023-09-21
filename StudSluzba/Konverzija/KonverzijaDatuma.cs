using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace StudSluzba.Konverzija

{
    
    public class KonverzijaDatuma : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateOnly datum = (DateOnly)value;
            return datum.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateOnly datum;
            if (DateOnly.TryParse(strValue, out datum))
            {
                return datum;
            }
            return null;
        }



    }
}
