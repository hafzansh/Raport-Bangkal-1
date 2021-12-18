using Raport.Helper;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Raport.Helper
{
    public class Result : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double sum = 0;            
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;

            //foreach (var item in values)
            //{
            //    sum += System.Convert.ToDouble(item);
            //}
            //sum /= values.Length;
            for (int i = 0; i < Constants.current1; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            sum /= Constants.current1;
            string result;
            if (sum <=5)
            {
                result = "Tidak Tuntas";
            }
            else
            {
                result = "Tuntas";
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
