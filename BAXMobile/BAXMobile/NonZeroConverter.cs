using System;
using System.Globalization;
using Xamarin.Forms;

namespace BAXMobile
{
    public class NonZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            double number;
            if (double.TryParse(value.ToString(), out number))
            {
                return number >= 0;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
