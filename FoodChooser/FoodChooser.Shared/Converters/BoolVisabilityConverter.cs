using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FoodChooser.Converters
{
    public class BoolVisabilityConverter : IValueConverter
    {
        private object GetVisibility(object value)
        {
            //if (value is bool)
            //{
            //    return ((bool)value == true) ? Visibility.Collapsed : Visibility.Visible;
            //}
            

            if (!(value is bool))
            {
                return Visibility.Collapsed;
            }

            bool objValue = (bool)value;
            
            if (objValue)
            {
                return Visibility.Visible;
            }
            
            return Visibility.Collapsed;
        }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return GetVisibility(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
