using System;
using System.Windows;
using System.Windows.Data;

namespace QuinaNadal.Converters
{
    public class VisibilityHiddenConverter : IValueConverter
    {
        public bool ShowOnFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility rv = Visibility.Visible;
            try
            {
                bool x = value != null && bool.Parse(value.ToString());
                if (ShowOnFalse) x = !x;
                rv = x ? Visibility.Visible : Visibility.Hidden;
            }
            catch (Exception)
            {
                // ignored
            }
            return rv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }
}