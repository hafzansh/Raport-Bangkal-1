using Raport.Helper;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Raport.Helper
{
    public class Result2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double sum = 0;
            int val = Constants.current3;
            string result;
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            for (int i = 0; i < val; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            //sum /= values.Length;            
            double resultval = sum / val;
            if (resultval < Constants.current2)
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
