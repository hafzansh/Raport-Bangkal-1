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
            string dt;
            DateTime k; 
            if (value.ToString() == "")
            {
                dt = "01/01/2000 12:00:00 AM";
                k = DateTime.Parse(dt.ToString(), CultureInfo.CreateSpecificCulture("en-US"));
                return k.ToString("dd/MM/yyyy");
            }
            else {
                k = DateTime.Parse(value.ToString(), CultureInfo.CreateSpecificCulture("en-US"));
                return k.ToString("dd/MM/yyyy");
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
