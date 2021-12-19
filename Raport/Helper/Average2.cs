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
    public class Average2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            double sum = 0;
            int val = Constants.current3;
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;

            //foreach (var item in values)
            //{
            //    sum += System.Convert.ToDouble(item);
            //}
            for (int i = 0; i < val ; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            
            //sum /= values.Length;            
            double result = (sum /val);
            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
