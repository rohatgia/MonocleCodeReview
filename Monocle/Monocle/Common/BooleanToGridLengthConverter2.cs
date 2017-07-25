using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Monocle.Common
{
    class BooleanToGridLengthConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new GridLength(4, GridUnitType.Star);
            }
            else
            {
                return new GridLength(1, GridUnitType.Star);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new GridLength(4, GridUnitType.Star);
            }
            else
            {
                return new GridLength(1, GridUnitType.Star);
            }
        }
    }
}
