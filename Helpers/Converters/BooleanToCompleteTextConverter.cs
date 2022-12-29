using System;
using System.Globalization;
using System.Windows.Data;

namespace FFXIVRelicTracker.Helpers.Converters
{
    class BooleanToCompleteTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                {
                    return "Completed";
                }
            }
            return "NA";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
