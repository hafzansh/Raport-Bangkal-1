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
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;

            //foreach (var item in values)
            //{
            //    sum += System.Convert.ToDouble(item);
            //}
            for (int i = 0; i < Constants.current1; i++)
            {
                sum += System.Convert.ToDouble(values[i]);
            }
            //sum /= values.Length;            
            sum /= Constants.current1;
            return sum.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
