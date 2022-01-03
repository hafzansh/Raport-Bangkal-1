using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Raport.Helper
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dt = System.Convert.ToString(value);
            if (dt[1].ToString() == "/")
            {
                dt = "0" + dt;
            }
            string first = dt.Substring(0, 2);
            string mid = dt.Substring(3, 2);
            string last = dt.Substring(6, 4);
            string res = mid + "/" + first + "/" + last;
            //DateTime k = System.Convert.ToDateTime(dt.Substring(0, 10));
            DateTime k = DateTime.Parse(value.ToString(), CultureInfo.CreateSpecificCulture("en-US"));
            //DateTime k = System.Convert.ToDateTime(value);
            //DateTime dt = DateTime.ParseExact(value.ToString(), "dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
            return k.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
