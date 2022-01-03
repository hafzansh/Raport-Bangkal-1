using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Raport.Helper
{
    public class ValidationHelper : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int myInt;
            if (!int.TryParse(System.Convert.ToString(value), out myInt))
                return new ValidationResult(false, "Illegal characters");

            if (myInt < 0 || myInt > 20)
            {
                return new ValidationResult(false, "Please enter a number in the range: 0 - 20");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
