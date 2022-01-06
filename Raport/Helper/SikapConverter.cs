using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Raport.Helper
{
    class SikapConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = System.Convert.ToInt32(value);
            string result;
            if (val == 4)
            {
                result = "Sangat Baik";
            }
            else if (val == 3)
            {
                result = "Baik";
            }
            else if (val == 2)
            {
                result = "Cukup";
            }
            else if (val ==1)
            {
                result = "Perlu Bimbingan";
            }
            else
            {
                result = "Belum Diisi";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
