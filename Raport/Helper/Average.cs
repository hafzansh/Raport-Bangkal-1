using Raport.Helper;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Raport.Helper
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>    
    public class Average : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            double sum = 0;
            double sum2 = 0;
            int val = Constants.current1;
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            for (int i = 0; i < val ; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            for (int i=5;i< (5 + val); i++)
            {
                sum2 += System.Convert.ToDouble(values[i]);
            }
            //sum /= values.Length;            
            double result = (sum + sum2) / (val*2);
            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
