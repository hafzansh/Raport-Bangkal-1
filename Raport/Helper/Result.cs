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
            double sum2 = 0;
            int val = Constants.current1;
            string result;
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            for (int i = 0; i < val; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            for (int i = 5; i < (5 + val); i++)
            {
                sum2 += System.Convert.ToDouble(values[i]);
            }
            //sum /= values.Length;            
            double resultval = (sum + sum2) / (val * 2);
            if (resultval <= Constants.current2)
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
